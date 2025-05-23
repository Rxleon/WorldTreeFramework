using System;
namespace WorldTree
{

	/// <summary>
	/// ������
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public class TreeCopyableAttribute : Attribute { }

	/// <summary>
	/// ����ر�����
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public class TreeCopySpecialAttribute : Attribute { }

	/// <summary>
	/// �����Ա�������Ա��
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class TreeCopyIgnoreAttribute : Attribute { }

	/// <summary>
	/// ���������ӿڣ��������л�δ֪���ͣ����AsRule�ķ�������
	/// </summary>
	public interface ITreeCopy : IRule { }

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

	#region �ǳ��淨��

	/// <summary>
	/// �ṹ�忽���ӿڣ����ڷ����л�δ֪���ͣ����AsRule�ķ�������
	/// </summary>
	public interface TreeCopyStruct : ISendRefRule, ISourceGeneratorIgnore { }


	/// <summary>
	/// �ṹ�忽������
	/// </summary>
	/// <remarks>���Ƴ���д�����Բ�������Ϊ��</remarks>
	public abstract class TreeCopyStructRule<GT> : Rule<GT, TreeCopyStruct>, ISendRefRule<GT, GT>
	{
		/// <summary>
		/// ����
		/// </summary>
		public virtual void Invoke(INode self, ref GT arg1, ref GT arg2) => Execute(self as TreeCopyExecutor, ref arg1, ref arg2);
		/// <summary>
		/// ִ��
		/// </summary>
		protected abstract void Execute(TreeCopyExecutor self, ref GT arg1, ref GT arg2);
	}

	#endregion
}