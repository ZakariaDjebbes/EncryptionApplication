using System.ComponentModel;

namespace Encryption
{
	/// <summary>
	/// Result of any encrption or decryption of <see cref="IBaseCipher"/> implementations.
	/// </summary>
	public class CipherResult : INotifyPropertyChanged
	{
		private string originalText;
		private string encryptedText;

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		/// <summary>
		/// The input text provided to a <see cref="IBaseCipher"/>
		/// </summary>
		public string OriginalText
		{
			get => originalText;
			set
			{
				originalText = value;
				OnPropertyChanged(nameof(OriginalText));
			}
		}

		/// <summary>
		/// The outputed text ecrypted by a <see cref="IBaseCipher"/>.
		/// </summary>
		public string EncryptedText
		{
			get => encryptedText;
			set
			{
				encryptedText = value;
				OnPropertyChanged(nameof(EncryptedText));
			}
		}

		/// <summary>
		/// Constructs a <see cref="CipherResult"/> object.
		/// </summary>
		/// <param name="originalText">The input text provided to the <see cref="IBaseCipher"/></param>
		/// <param name="ecryptedText">The outputed text ecrypted by the <see cref="IBaseCipher"/>.</param>
		public CipherResult(string originalText, string ecryptedText)
		{
			OriginalText = originalText;
			EncryptedText = ecryptedText;
		}

		/// <summary>
		/// Constructs a default <see cref="CipherResult"/>.
		/// </summary>
		public CipherResult()
		{
		}
	}
}