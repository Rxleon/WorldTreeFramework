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
			LoadFileSystemToTreeView();
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode a = e.Node;
		}

		// ��������һ��TreeView�ؼ���ΪtreeView1
		private void LoadFileSystemToTreeView()
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
	}
}
