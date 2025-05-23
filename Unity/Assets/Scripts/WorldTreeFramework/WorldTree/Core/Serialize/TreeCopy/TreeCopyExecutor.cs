/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2025/5/20 20:14

* ������

*/


using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WorldTree
{
	public static partial class TreeCopyExecutorRule
	{
		class AddRule : AddRule<TreeCopyExecutor>
		{
			protected override void Execute(TreeCopyExecutor self)
			{
				// ��ȡ�ڵ�ķ���
				self.Core.RuleManager.TryGetRuleGroup<TreeCopyStruct>(out self.copyStructRuleDict);
				self.Core.PoolGetUnit(out self.ObjectToObjectDict);
			}
		}

		class RemoveRule : RemoveRule<TreeCopyExecutor>
		{
			protected override void Execute(TreeCopyExecutor self)
			{
				self.ObjectToObjectDict.Dispose();
			}
		}
	}

	/// <summary>
	/// �����ִ����
	/// </summary>
	public class TreeCopyExecutor : Node
		, TempOf<INode>
		, AsRule<ITreeCopy>
		, AsAwake
	{
		/// <summary>
		/// �����Ӧ�����ֵ�
		/// </summary>
		public UnitDictionary<object, object> ObjectToObjectDict;

		/// <summary>
		/// ��ͬ�������л������б���
		/// </summary>
		public RuleGroup copyStructRuleDict;

		/// <summary>
		/// ��������
		/// </summary>
		public T CopyTo<T>(T source, ref T target) => CloneObject(source, ref target, true);

		/// <summary>
		/// ��������
		/// </summary>
		public T Copy<T>(T source)
		{
			T target = default;
			return CloneObject(source, ref target);
		}

		/// <summary>
		/// ��������
		/// </summary>
		public T CloneObject<T>(T source, ref T target, bool isClear = false)
		{
			if (isClear) ObjectToObjectDict.Clear();

			if (EqualityComparer<T>.Default.Equals(source, default))
			{
				// �����Ĭ��ֵ��ֱ�Ӹ�ֵ������
				target = source;
				return target;
			}

			// ����Ǵ�ֵ���ͣ������������ֶΣ���ֱ�Ӹ�ֵ������
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				target = source;
				return target;
			}

			Type type = source.GetType();
			long typeCode = this.Core.TypeToCode(type);
			this.Core.RuleManager.SupportNodeRule(typeCode);

			// ǰ�� ��ֵ���͵�ס�ˣ��������ж� �������õĽṹ��
			if (type.IsValueType)
			{
				if (copyStructRuleDict.TryGetValue(typeCode, out RuleList ruleList))
				{
					for (int i = 0; i < ruleList.Count; i++)
					{
						Unsafe.As<TreeCopyStructRule<T>>(ruleList[i]).Invoke(this, ref source, ref target);
					}
				}
			}
			// ���ǽṹ�壬��������
			else if (this.Core.RuleManager.TryGetRuleList<TreeCopy>(typeCode, out RuleList ruleList) && ruleList.NodeType == typeCode)
			{
				object sourceObj = source;
				object targetObj = null;

				// ���Ի�ȡĿ�����
				if (!ObjectToObjectDict.TryGetValue(sourceObj, out targetObj))
				{
					// �������򿽱�
					targetObj = target;
					((IRuleList<TreeCopy>)ruleList).SendRef(this, ref sourceObj, ref targetObj);
				}

				target = (T)targetObj;

				// ��¼��������
				if (sourceObj != null && targetObj != null)
					ObjectToObjectDict.TryAdd(sourceObj, targetObj);
			}
			return target;
		}

		/// <summary>
		/// Σ��ָ�����Ϳ�������
		/// </summary>
		public void TypeCloneObject(Type type, object source, ref object target)
		{
			long typeCode = this.TypeToCode(type);
			if (Core.RuleManager.TryGetRuleList<TreeCopy>(typeCode, out RuleList ruleList) && ruleList.NodeType == typeCode)
				((IRuleList<TreeCopy>)ruleList).SendRef(this, ref source, ref target);
		}
	}
}