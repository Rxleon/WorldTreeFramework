/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2025/5/20 20:14

* ������

*/


using System;
using System.Runtime.CompilerServices;

namespace WorldTree
{
	/// <summary>
	/// ������
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public class TreeCopyableAttribute : Attribute { }

	/// <summary>
	/// �����Ա�������Ա��
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class TreeCopyIgnoreAttribute : Attribute { }



	/// <summary>
	/// ���������ӿڣ��������л�δ֪���ͣ����AsRule�ķ�������
	/// </summary>
	public interface ITreeCopy : ISendRefRule { }

	///// <summary>
	///// ��������йܷ���
	///// </summary>
	//public interface TreeCopyUnmanaged<T> : ISendRefRule<T, T>, ITreeCopy, ISourceGeneratorIgnore { }
	///// <summary>
	///// ��������йܷ���
	///// </summary>
	//public abstract class TreeCopyUnmanagedRule<T> : SendRefRule<TreeCopyExecutor, TreeCopyUnmanaged<T>, T, T> { }


	/// <summary>
	/// ���������
	/// </summary>
	public interface TreeCopy : ISendRefRule<object, object>, ITreeCopy, ISourceGeneratorIgnore { }
	/// <summary>
	/// ���������
	/// </summary>
	public abstract class TreeCopyRule<GT> : Rule<GT, TreeCopy>, ISendRefRule<object, object>
	{
		/// <summary>
		/// ����
		/// </summary>
		public virtual void Invoke(INode self, ref object source, ref object target) => Execute(self as TreeCopyExecutor, ref source, ref target);
		/// <summary>
		/// ִ��
		/// </summary>
		protected abstract void Execute(TreeCopyExecutor self, ref object source, ref object target);
	}

	/// <summary>
	/// �����ִ����
	/// </summary>
	public class TreeCopyExecutor : Node, ICloneable
		, AsRule<ITreeCopy>
	{
		/// <summary>
		/// �����ӦId
		/// </summary>
		public UnitDictionary<object, int> ObjectToIdDict;

		/// <summary>
		/// Id��Ӧ����
		/// </summary>
		public UnitDictionary<int, object> IdToObjectDict;

		public object Clone()
		{
			return null;
		}

		/// <summary>
		/// ��������
		/// </summary>
		public void CopyTo<T>(in T source, ref T target)
		{
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				target = source;
				return;
			}


		}
	}


}