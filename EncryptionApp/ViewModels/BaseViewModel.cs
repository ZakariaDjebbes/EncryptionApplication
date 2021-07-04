using System;
using System.ComponentModel;
using EncryptionApp.Encryption;

namespace EncryptionApp.ViewModels
{
	internal class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		private EncryptionResult encryptionResult;

		public EncryptionResult EncryptionResult
		{
			get { return encryptionResult; }
			set
			{
				encryptionResult = value;
				OnPropertyChanged(nameof(EncryptionResult));
			}
		}
	}
}