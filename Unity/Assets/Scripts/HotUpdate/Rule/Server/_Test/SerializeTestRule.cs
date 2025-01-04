/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2024/8/27 15:17

* ������

*/
using System;

namespace WorldTree
{

	public static partial class SerializeTest1Rule
	{

		static unsafe Action<SerializeTest> OnAddSerializeTest1 = (self) =>
		{

			//���д�㲻һ��������
			NodeClassDataTest<int, float, int> testData = new();
			testData.ValueT1 = 987;
			testData.ValueT2 = 45.321f;
			testData.ValueT3 = 1234567;

			UnitDictionary<int, float> objDict = new();


			self.AddTemp(out TreePackByteSequence sequenceWrite);
		};
	}

	public static partial class SerializeTestRule
	{
		static OnAdd<SerializeTest> OnAddSerializeTest = (self) =>
		{
			self.Log($"Ƕ�����л����ԣ���������");
			//���д�㲻һ��������
			NodeClassDataTest<int, float, int> testData = new();
			testData.ValueT1 = 987;
			testData.ValueT2 = 45.321f;
			testData.ValueT3 = 1234567;


			//Ƕ������
			testData.DataTest1 = new NodeClassDataTest1<int, float>();
			testData.DataTest1.TestInts = new[] { 1, 3, 5, 88 };
			testData.DataTest1.TestT2 = 5.789456f;
			testData.DataTest1.ValueT4Dict = new UnitDictionary<int, string>()
			{
					{ 1, "A1.145f����������" },
					{ 2, "A2.278f��" },
					{ 3, "A3.312f��" },
			};

			testData.DataTestBase = new NodeClassDataBase()
			{
				TestInts = new[] { 17, 31, 54, 8899 },
				TestT2 = 5,
				//TestFloat_T = 1.999f,
			};



			// ���л�
			self.AddTemp(out TreePackByteSequence sequenceWrite).Serialize(testData);
			byte[] bytes = sequenceWrite.ToBytes();

			self.Log($"���л��ֽڳ���{bytes.Length}");

			// �����л�
			self.AddTemp(out TreePackByteSequence sequenceRead).SetBytes(bytes);
			NodeClassDataTest<int, float, int> testData2 = null;
			sequenceRead.Deserialize(ref testData2);
			string logText = $"�����л�{testData2.ValueT1} {testData2.ValueT2}  �����ֶΣ�{testData2.ValueT3}  Ƕ�����ֶΣ� {testData2.DataTest1.TestT2}  ";

			logText += $"\n�ֵ䣺";
			if (testData2.DataTest1.ValueT4Dict == null)
			{
				logText += $"null !!, ";
			}
			else
			{
				foreach (var item in testData2.DataTest1.ValueT4Dict)
				{
					logText += $"{item.Key} {item.Value}, ";
				}
			}


			logText += $"\n���飺";
			if (testData2.DataTest1.TestInts == null)
			{
				logText += $"null !!, ";
			}
			else
			{
				foreach (var item in testData2.DataTest1.TestInts)
				{
					logText += $"{item}, ";
				}
			}


			logText += $"\n�������飺";
			NodeClassDataBase nodeClassDataSub = testData2.DataTestBase as NodeClassDataBase;
			//logText += $" {nodeClassDataSub.TestFloat_T} ";
			foreach (var item in nodeClassDataSub.TestInts)
			{
				logText += $"{item}, ";
			}
			self.Log(logText);
		};
	}
}