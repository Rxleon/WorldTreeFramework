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
		public Action action;
	}
}