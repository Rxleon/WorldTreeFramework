using WorldTree;

namespace InputBind
{
	internal static class Program
	{
		/// <summary>
		/// ����
		/// </summary>
		private static InputBindForm form = new InputBindForm();

		/// <summary>
		/// �������ʱ��
		/// </summary>
		public static DateTime UpdateTime;

		[STAThread]
		static void Main()
		{
			Type ruleType = typeof(MainWorldRule);
			Type nodeType = typeof(MainWorld);

			ApplicationConfiguration.Initialize();
			WorldLineManager lineManager = new();
			lineManager.Options = new();
			lineManager.LogType = typeof(WorldLog);
			lineManager.Create(0, typeof(WinFormWorldHeart), 1000, typeof(MainWorld));
			Application.Run(form);

			//��ӽڵ㣬�ڵ�����¼��õ�From

			UpdateTime = DateTime.Now;

			while (form.IsOpen)
			{
				Thread.Sleep(1000);
				lineManager.MainUpdate?.Invoke(DateTime.Now - UpdateTime);
				UpdateTime = DateTime.Now;
			}
		}
	}
}