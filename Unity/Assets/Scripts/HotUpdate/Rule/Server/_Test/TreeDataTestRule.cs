using System.IO;

namespace WorldTree
{


	//添加一个标记序列化的枚举
	//利用嫁接的方式，将节点挂到树上，并彻底初始化

	public static partial class TreeDataTestRule
	{
		static int Key = nameof(Key).GetFNV1aHash32();
		static int Value = nameof(Value).GetFNV1aHash32();

		static unsafe OnAdd<TreeDataTest> OnAdd2 = (self) =>
		{
			if (self != null) return;

			TypeHashGeneratorRule.Example();
		};

		static unsafe OnAdd<TreeDataTest> OnAdd = (self) =>
		{
			//if (self != null) return;

			//self.AddChild(out self.treeData);
			//self.treeData.Name = "测试123";
			//self.treeData.Age = 18789;

			//self.treeData.AddChild(out TreeDataNodeDataTest2 child);
			//child.Name = "测试4658";
			//child.Age = 788723;

			//byte[] bytes = TreeDataHelper.SerializeNode(self.treeData);
			string filePath = "C:\\Users\\admin\\Desktop\\新建文件夹\\TreeDataTest.bytes";

			//self.Log($"序列化字节长度{bytes.Length}\n");

			////保存到桌面文件
			//File.WriteAllBytes(filePath, bytes);
			//self.Log($"序列化保存！！!");

			//self.treeData.Dispose();
			//self.treeData = null;


			//读取桌面文件
			byte[] bytes = File.ReadAllBytes(filePath);
			TreeDataHelper.DeseralizeNode(self, bytes).TryGraftSelfToTree(self);
			self.Log($"反序列化！！!{bytes.Length}");
			self.Log(NodeRule.ToStringDrawTree(self));
		};



		static unsafe OnUpdate<TreeDataNodeDataTest2> OnUpdate = (self) =>
		{
			self.Log($"TreeDataNodeDataTest{self.Name}:{self.Age}");
		};



		static unsafe OnAdd<TreeDataTest> OnAdd1 = (self) =>
		{
			if (self != null) return;
			AData data = self.AddComponent(out AData _);

			data.AInt = 401.5f;
			data.Ints = new int[][,,]{
				new int[2,2,5]{ { { 1, 2, 30, 4, 5 }, { 20, 45, 90, 75, 23 } }, { { 1, 23, 360, 84, 5 }, { 2, 5, 9, 5, 2 } } },
				new int[2,1,5]{ { { 1220, 45, 90, 75, 23 } }, { { 1, 23, 360, 84, 5 }} },
			};

			data.DataDict = new UnitDictionary<int, string>()
			{
				{ 1, "1.1f测123" },
				{ 2, "2.2f测试123" },
				{ 3, "3.3f" },
				{ 4, "4.4f" },
				{ 5, "5.5f"},
			};

			AData aDataBase = data;
			byte[] bytes = TreeDataHelper.SerializeNode(aDataBase);
			self.RemoveComponent<TreeDataTest, AData>();
			self.Log($"序列化字节长度{bytes.Length}\n");


			TreeSpade treeSpade = TreeDataHelper.DeseralizeNode(self, bytes);
			treeSpade.TryGraftSelfToTree(self);
			self.TryGetComponent(out AData data2);
			string logText = $"\n反序列化{data2.AInt} \n";

			logText += $"\n数组数量{data2.Ints.Length} :\n";
			foreach (var item in data2.Ints)
			{
				logText += $"数组长度{item.Length} :";

				foreach (var item1 in item)
				{
					logText += $"{item1} ";
				}
				//logText += $"{item} ";
			}
			if (data2.DataDict != null)
			{
				logText += $"\n字典数量{data2.DataDict.Count} :\n";
				foreach (var item in data2.DataDict)
				{
					logText += $"[{item.Key}: {item.Value}],";
				}
			}
			self.Log(logText);
			self.Log("\n反序列化结构打印\n");

			self.Log(NodeRule.ToStringDrawTree(self));

		};
	}


}