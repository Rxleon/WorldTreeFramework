/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2025/5/21 15:45

* ������

*/

namespace WorldTree.Server
{
	/// <summary>
	/// ��������
	/// </summary>
	[TreeCopyable]
	public class CopyTest1
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
		/// a
		/// </summary>
		public int Value11 { get => Value1; set => Value1 = value; }

		/// <summary>
		/// a
		/// </summary>
		public float Value21 { get => Value2; set => Value2 = value; }
	}

	/// <summary>
	/// ��������
	/// </summary>
	public class CopyTest1Sub
	{
		/// <summary>
		/// ����
		/// </summary>
		public int Value1 = 1;
	}



	/// <summary>
	/// �������
	/// </summary>
	public class DeepCopyTest : Node
		, ComponentOf<INode>
		, AsAwake
	{ }

}