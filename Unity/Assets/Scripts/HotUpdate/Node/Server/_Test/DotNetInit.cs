using System;

namespace WorldTree
{
	/// <summary>
	/// ���Է���123,����ע��
	/// </summary>
	public interface TestRule : ISendRule<float, string>, IMethodRule { }



	/// <summary>
	/// ���Խڵ�
	/// </summary>
	public partial class DotNetInit : Node, ComponentOf<INode>
		, AsComponentBranch
		, AsAwake
		, AsTestRule
	{
		/// <summary>
		/// ����
		/// </summary>
		public Action Action;
	}
}