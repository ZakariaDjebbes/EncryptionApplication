using System.ComponentModel;
using Encryption;

namespace EncryptionApp.ViewModels
{
	internal abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		private CipherResult encryptionResult;

		public CipherResult CipherResult
		{
			get { return encryptionResult; }
			set
			{
				encryptionResult = value;
				OnPropertyChanged(nameof(CipherResult));
			}
		}
	}
}