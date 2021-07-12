using System.Windows.Controls;
using EncryptionApp.ViewModels;

namespace EncryptionApp.Controls
{
	public partial class SymmetricKeyControl : UserControl
	{
		//private readonly SymmetricKeyViewModel model = new(null);

		public SymmetricKeyControl()
		{
			InitializeComponent();
			//DataContext = model;
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//if (model.UpdateAlgorithmCommand.CanExecute(algorithmComboBox.SelectedItem))
			//{
			//	model.UpdateAlgorithmCommand.Execute(algorithmComboBox.SelectedItem);
			//}
		}
	}
}