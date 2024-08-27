using System;


namespace WorldTree
{
	/// <summary>
	/// DotNetTestNodeRule
	/// </summary>
	public static partial class DotNetInitRule
	{
		private static OnEnable<DotNetInit> Enable1 = (self) =>
		{
			self.Log("�����");
		};

		private static OnAdd<DotNetInit> Add = (self) =>
		{
			self.Log(" ��ʼ��������");
			self.AddComponent(out SerializeTest _);
		};

		private static OnUpdate<DotNetInit> Update = (self) =>
		{
			self.Log($"��ʼ���£�����������4");
		};

		// ��Ҫ�޸Ĵ�������,Rule����ִ����Forech��ΪFor

		private static OnUpdateTime<DotNetInit> UpdateTime = (self, timeSpan) =>
		{
			//����������a
			if (Console.KeyAvailable)
			{
				var key = Console.ReadKey(intercept: true);
				if (key.Key == ConsoleKey.D)
				{
					self.Log($"�������� 'D' ��������");
					self.Root.AddComponent(out CodeLoader _).HotReload();
				}
			}
			//self.Log($"��ʼ���£�����{timeSpan.TotalSeconds}");
		};

		private static OnDisable<DotNetInit> Disable = (self) =>
		{
			self.Log("ʧ���");
		};

		private static OnRemove<DotNetInit> Remove = (self) =>
		{
			self.Log($"��ʼ�رգ���");
		};
	}


}
