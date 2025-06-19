using Avalonia.Controls;
using System;
using WorldTree;

namespace App
{
	public partial class MainWindow : Window
	{
		/// <summary>
		/// �������ʱ��
		/// </summary>
		public static DateTime UpdateTime;
		public MainWindow()
		{
			InitializeComponent();

			//Type ruleType = typeof(MainWorldRule);
			//Type nodeType = typeof(MainWorld);

			WorldLineManager lineManager = new();
			lineManager.Options = new();
			lineManager.LogType = typeof(WorldLog);
			lineManager.Create(0, typeof(AvaloniaWorldHeart), 1000, typeof(MainWorld));
			//lineManager.MainLine.GetGlobalRuleExecutor(out IRuleExecutor<WinFromEntry> globalRuleExecutor);
			//globalRuleExecutor.Send((Form)this); //���ʹ��ڵ�ȫ�ַ���


		}
	}
}