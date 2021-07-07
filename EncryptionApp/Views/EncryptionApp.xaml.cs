using System.Windows;
using EncryptionApp.ViewModels;

namespace EncryptionApp.Views
{
	public partial class EncryptionApp : Window
	{
		private readonly EncryptionAppViewModel model = new();

		public EncryptionApp()
		{
			InitializeComponent();
			DataContext = model;
		}


		private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
		{
			About about = new();
			about.Show();
		}
	}
}