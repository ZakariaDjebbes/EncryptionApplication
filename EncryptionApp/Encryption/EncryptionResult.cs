using System.ComponentModel;

namespace EncryptionApp.Encryption
{
	public enum EncryptionResultStatus
	{
		Done,
		Faiiled,
		None
	}

	public class EncryptionResult : INotifyPropertyChanged
	{
		private EncryptionResultStatus status;
		private string originalText;
		private string encryptedText;

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		public EncryptionResultStatus Status
		{
			get => status; 
			set
			{
				status = value;
				OnPropertyChanged(nameof(Status));
			}
		}

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

		public EncryptionResult(string originalText, string ecryptedText, EncryptionResultStatus status)
		{
			OriginalText = originalText;
			EncryptedText = ecryptedText;
			Status = status;
		}

		public EncryptionResult()
		{
		}
	}
}