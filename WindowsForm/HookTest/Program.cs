using WorldTree;

namespace HookTest
{
	internal class Program
	{
		private static Form1 form1 = new Form1();

		public static int a = 0;

		public static WorldTreeCore core = new WorldTreeCore();

		private static void Main(string[] args)
		{

			form1.labelT.Text = $"����:{a++}";

			core.Log = Console.WriteLine;
			core.LogWarning = Console.WriteLine;
			core.LogError = Console.Error.WriteLine;
			//������������ �趨���Ϊ1000ms
			core.Init(typeof(WorldHeart), 1000);

			core.World.AddComponent(out Entry _);

			Type ruleType = typeof(EntryRule);//��ֹ���򼯱��Ż���
			Type nodeType = typeof(DotNetInit);

			form1.TextBox.Text = NodeRule.ToStringDrawTree(core);
			Application.Run(form1);
		}
	}
}