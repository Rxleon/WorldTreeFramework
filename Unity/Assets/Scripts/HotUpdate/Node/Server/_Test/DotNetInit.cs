/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2024/8/27 11:53

* ������

*/
using System;
using System.Collections.Generic;

namespace WorldTree.Server
{
	/// <summary>
	/// ���Խڵ�
	/// </summary>
	public partial class DotNetInit : Node, ComponentOf<INode>
		, AsComponentBranch
		, AsChildBranch
		, AsAwake
	{
		/// <summary>
		/// ����
		/// </summary>
		public int ConfigId;
		/// <summary>
		/// ����
		/// </summary>
		public Action Action;

		/// <summary>
		/// a
		/// </summary>
		public List<int> intsList;


	}



	/// <summary>
	/// ����
	/// </summary>
	public class Test<T> : Node
		, AsTestNodeEvent<DotNetInit>
		, AsRule<IRule>
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