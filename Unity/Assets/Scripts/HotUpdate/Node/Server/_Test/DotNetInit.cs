using System;

namespace WorldTree
{
	/// <summary>
	/// ���Խڵ�
	/// </summary>
	public partial class DotNetInit : Node, ComponentOf<INode>
		, AsComponentBranch
		, AsAwake
	{
		/// <summary>
		/// ����
		/// </summary>
		public Action Action;
	}
}