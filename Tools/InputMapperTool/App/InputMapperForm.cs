namespace InputMapperTool
{
	public partial class InputMapperForm : Form
	{
		public InputMapperForm()
		{
			InitializeComponent();
		}

		private void InputMapperForm_Load(object sender, EventArgs e)
		{
			NewTreeView();
			NewlistBox();
			NewlistView();
			NewTabControl();
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode a = e.Node;
		}

		// ��������һ��TreeView�ؼ���ΪtreeView1
		private void NewTreeView()
		{
			// ������нڵ�
			treeView1.Nodes.Clear();
			// �������ڵ�
			TreeNode rootNode = new TreeNode("�ҵĵ���");
			// �����ڵ���ӵ�TreeView�ؼ�
			treeView1.Nodes.Add(rootNode);

			// ������һ����������ȡָ��Ŀ¼�µ�������Ŀ¼���ļ�
			// �����Ϊֱ����Ӽ���ʾ���ӽڵ�
			TreeNode documentsNode = new TreeNode("�ĵ�");
			documentsNode.Text = "�ĵ�ceshi";
			rootNode.Nodes.Add(documentsNode);
			// ... ��������ӽڵ�
			// չ�����ڵ�
			rootNode.Expand();
		}

		private void NewlistBox()
		{
			listBox1.Items.Clear();
			listBox1.Items.Add("����");
			listBox1.Items.Add("����2");
		}
		private void NewlistView()
		{
			this.listView1.Items.Clear(); //����б�
			this.listView1.BeginUpdate();   //���ݸ��£�UI��ʱ����ֱ��EndUpdate���ƿؼ���������Ч������˸�������߼����ٶ�

			for (int i = 0; i < 10; i++)   //���10������
			{
				ListViewItem lvi = new ListViewItem();

				lvi.ImageIndex = i;     //ͨ����imageList�󶨣���ʾimageList�е�i��ͼ��

				lvi.Text = "subitem" + i;

				lvi.SubItems.Add("��2��,��" + i + "��");

				lvi.SubItems.Add("��3��,��" + i + "��");

				this.listView1.Items.Add(lvi);
			}

			this.listView1.EndUpdate();  //�������ݴ���UI����һ���Ի��ơ�

		}
		private void NewTabControl()
		{
			// ����TabControl�ؼ�
			var tabControl = new TabControl();
			tabControl.Multiline = true; // ����Multiline����ΪTrue

			// ��ӱ�ǩҳ
			var tabPage1 = new TabPage("��ǩҳ1");
			var tabPage2 = new TabPage("��ǩҳ2");
			var tabPage3 = new TabPage("��ǩҳ3");
			tabControl.TabPages.AddRange(new TabPage[] { tabPage1, tabPage2, tabPage3 });

			tabPage3.Controls.Add(new Label { Text = "���Ǳ�ǩҳ3������", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter });

			// ���TabControl�ؼ�������
			this.Controls.Add(tabControl);
		}



		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedItem != null)
			{
				MessageBox.Show($"ѡ���{listBox1.SelectedItem}");

			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}
	}
}
