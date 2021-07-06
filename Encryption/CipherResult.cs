using System.ComponentModel;

namespace Encryption
{
	public class CipherResult : INotifyPropertyChanged
	{
		private string originalText;
		private string encryptedText;

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		public string OriginalText
		{
			get => originalText;
			set
			{
				originalText = value;
				OnPropertyChanged(nameof(OriginalText));
			}
		}

		public string EncryptedText
		{
			get => encryptedText;
			set
			{
				encryptedText = value;
				OnPropertyChanged(nameof(EncryptedText));
			}
		}

		public CipherResult(string originalText, string ecryptedText)
		{
			OriginalText = originalText;
			EncryptedText = ecryptedText;
		}

		public CipherResult()
		{
		}
	}
}