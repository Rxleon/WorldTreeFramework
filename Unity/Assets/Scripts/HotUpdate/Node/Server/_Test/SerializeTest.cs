/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2024/8/27 15:15

* ������

*/
namespace WorldTree
{
	/// <summary>
	/// ��������
	/// </summary>
	[TreePackSerializable]
	public partial class NodeClassDataTest<T1, T2, T3>
		where T1 : unmanaged
		where T2 : unmanaged

	{
		/// <summary>
		/// ���Է���1
		/// </summary>
		public T1 ValueT1 = default;

		/// <summary>
		/// ���Է���2
		/// </summary>
		public T2 ValueT2 = default;


		/// <summary>
		/// ���Է���3
		/// </summary>
		public T3 ValueT3 { get; set; } = default;

		/// <summary>
		/// ����class
		/// </summary>
		public NodeClassDataTest1<T1, T2> DataTest1 = default;


		/// <summary>
		/// ����class
		/// </summary>
		public NodeClassDataBase DataTestBase;

	}

	/// <summary>
	/// ��������2
	/// </summary>
	[TreePackSerializable]
	public partial struct NodeClassDataTest1<T1, T2>
	//where T1 : unmanaged
	{
		/// <summary>
		/// ��������
		/// </summary>
		public T1[] TestInts { get; set; }

		/// <summary>
		/// ���Ը���
		/// </summary>
		public T2 TestT2 { get; set; }

		/// <summary>
		/// �����ֵ�
		/// </summary>
		public UnitDictionary<int, string> ValueT4Dict;
	}


	/// <summary>
	/// ��������3
	/// </summary>
	[TreePackSerializable]
	[TreePackSub(typeof(NodeClassDataSub1<int>))]
	[TreePackSub(typeof(NodeClassDataSub2))]
	public partial class NodeClassDataBase
	{
		/// <summary>
		/// ��������
		/// </summary>
		public int[] TestInts { get; set; }

		/// <summary>
		/// ���Ը���
		/// </summary>
		public int TestT2 { get; set; }
	}

	/// <summary>
	/// ��������3
	/// </summary>
	[TreePackSerializable]
	public partial class NodeClassDataSub1<T> : NodeClassDataBase
	{
		/// <summary>
		/// ��������
		/// </summary>
		public int TestInt_T;
	}

	/// <summary>
	/// ��������4
	/// </summary>
	[TreePackSerializable]
	public partial class NodeClassDataSub2 : NodeClassDataBase
	{
		/// <summary>
		/// ��������
		/// </summary>
		public float TestFloat_T;
	}


	/// <summary>
	/// ���л�����
	/// </summary>
	public class SerializeTest : Node
		, ComponentOf<INode>
		, AsAwake
	{ }

}