/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2025/5/21 15:45

* ������

*/

using System.Collections;
using System.Collections.Generic;

namespace WorldTree.Server
{
	/// <summary>
	/// ��������
	/// </summary>
	[TreeCopyable]
	public partial class CopyTest1
	{
		/// <summary>
		/// ����
		/// </summary>
		public int Value1 = 1;
		/// <summary>
		/// ����
		/// </summary>
		public CopyTestStruct1 Value2 = default;

		/// <summary>
		/// ����
		/// </summary>
		public IDictionary ValueDict = null;

		/// <summary>
		/// a
		/// </summary>
		public int Value11 { get => Value1; set => Value1 = value; }

		/// <summary>
		/// a
		/// </summary>
		public CopyTestStruct1 Value21 { get => Value2; set => Value2 = value; }
	}

	/// <summary>
	/// ���Խṹ��
	/// </summary>
	[TreeCopyable]
	public partial struct CopyTestStruct1
	{
		/// <summary>
		/// ����
		/// </summary>
		public int Value1 = 1;
		/// <summary>
		/// ����
		/// </summary>
		public float Value2 = 1f;
		/// <summary>
		/// ����
		/// </summary>
		public string Value3 = "f";

		/// <summary>
		/// a
		/// </summary>
		public int Value11 { get => Value1; set => Value1 = value; }

		/// <summary>
		/// a
		/// </summary>
		public float Value21 { get => Value2; set => Value2 = value; }

		public CopyTestStruct1()
		{
		}

	}

	/// <summary>
	/// ��������
	/// </summary>
	[TreeCopyable]
	public partial class CopyTest1Sub : CopyTest1
	{
		/// <summary>
		/// ����
		/// </summary>
		public int ValueSub1 = 1;
	}



	/// <summary>
	/// �������
	/// </summary>
	public class DeepCopyTest : Node
		, ComponentOf<INode>
		, AsAwake
	{ }




	/// <summary>
	/// �����ֵ�����
	/// </summary>
	[TreeCopyable]
	public partial class CopyTestDict1 : Dictionary<int, int>
	{
		/// <summary>
		/// ����
		/// </summary>
		public int Value1 = 1;

		/// <summary>
		/// ����
		/// </summary>
		public string Value11 { get; set; }
	}
}