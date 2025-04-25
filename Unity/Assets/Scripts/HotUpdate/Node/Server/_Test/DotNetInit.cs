/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2024/8/27 11:53

* ������

*/
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





	/// <summary>
	/// ����
	/// </summary>
	public class Test : Node
		, AsTestNodeEvent<DotNetInit>
	{
		/// <summary>
		/// �ֶ�
		/// </summary>
		public int ConfigId;

		/// <summary>
		/// ����
		/// </summary>
		public long ConfigName => ConfigId;
	}


}