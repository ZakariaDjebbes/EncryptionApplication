using System.ComponentModel;
using System.Windows;
using EncryptionApp.ViewModels;

namespace EncryptionApp.Views
{
	public partial class EncryptionApp : Window
	{
		private readonly EncryptionAppViewModel model = new();
		private readonly BackgroundWorker encryptionWorker = new();

		public EncryptionApp()
		{
			InitializeComponent();
			DataContext = model;
			encryptionWorker.DoWork += EncryptionWorker_DoWork;
		}

		private void EncryptionWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (string.IsNullOrEmpty(model.InputText))
			{
				MessageBox.Show(
					messageBoxText: "Enter something to encrypt first!",
					caption: "Nothing to encrypt",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Error);
				return;
			}

			if (model.SelectedViewModel is CaesarCipherViewModel caesarVM)
			{
				var res = caesarVM.Encrypt(model.InputText);
				model.EncryptionResult = res;
			}
			else if (model.SelectedViewModel is SubstitutionCipherViewModel substitutionVM)
			{

			}
		}

		private void EncryptButton_Click(object sender, RoutedEventArgs e)
		{
			encryptionWorker.RunWorkerAsync();
		}

		private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
		{
			About about = new();
			about.Show();
		}
	}
}