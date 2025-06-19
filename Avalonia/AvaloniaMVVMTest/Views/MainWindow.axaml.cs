using Avalonia.Controls;
using System.Diagnostics;

namespace AvaloniaMVVMTest.Views
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new ViewModels.MainWindowViewModel();
		}

		private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			// ����д�������߼�
			Console.Text = "Button_Click��ť������ˣ�";
			Debug.WriteLine("Button_Click��ť�������!");
		}


	}
}