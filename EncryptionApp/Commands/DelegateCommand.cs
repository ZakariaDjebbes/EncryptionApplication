using System;
using System.Windows.Input;

namespace EncryptionApp.Commands
{
	public class DelegateCommand : ICommand
	{
		private readonly Predicate<object> canExecute;
		private readonly Action<object> execute;

		public event EventHandler CanExecuteChanged;

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public DelegateCommand(Action<object> execute) : this(execute, null)
		{
		}

		public virtual bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);

		public void Execute(object parameter)
		{
			execute(parameter);
		}

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}