using System;
using System.Windows.Input;
using EncryptionApp.ViewModels;

namespace DirectionClient.Commands
{
	internal class UpdateViewCommand : ICommand
	{
		private EncryptionAppViewModel _vm;

		public UpdateViewCommand(EncryptionAppViewModel vm)
		{
			_vm = vm ?? throw new ArgumentNullException(nameof(vm));
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (parameter.ToString().Equals("Caesar"))
			{
				_vm.SelectedViewModel = new CaesarAlgorithmViewModel(_vm);
			}

			if (parameter.ToString().Equals("Symmetric"))
			{
				_vm.SelectedViewModel = new SymmetricKeyViewModel();
			}
		}
	}
}