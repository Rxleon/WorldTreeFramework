using System;
using System.Windows.Forms;
using WorldTree;
namespace InputBind
{
	public partial class InputBindForm : Form
	{

		/// <summary>
		/// �������ʱ��
		/// </summary>
		public static DateTime UpdateTime;

		public InputBindForm()
		{

			InitializeComponent();

			WorldLineManager lineManager = new();
			lineManager.Options = new();
			lineManager.LogType = typeof(WorldLog);
			lineManager.Create(0, typeof(WinFormWorldHeart), 1000, typeof(MainWorld));
			lineManager.MainLine.GetGlobalRuleExecutor(out IRuleExecutor<WinFromEntry> globalRuleExecutor);
			globalRuleExecutor.Send((Form)this); //���ʹ��ڵ�ȫ�ַ���
		}

		private void Form1_Load(object sender, EventArgs e)
		{


		}

		private void button1_Click(object sender, EventArgs e)
		{
			//Controls
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}
	}





}
