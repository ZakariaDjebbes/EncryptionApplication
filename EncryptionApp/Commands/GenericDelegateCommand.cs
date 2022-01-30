using System;
using System.Windows.Input;

namespace EncryptionApp.Commands
{
	public class DelegateCommand<T> : ICommand
	{
		private readonly Predicate<T> canExecute;
		private readonly Action<T> execute;

		public event EventHandler CanExecuteChanged;

		public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public DelegateCommand(Action<T> execute) : this(execute, null)
		{
		}

		public virtual bool CanExecute(object parameter) => canExecute == null || canExecute((T)parameter);

		public void Execute(object parameter) => execute((T)parameter);

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}