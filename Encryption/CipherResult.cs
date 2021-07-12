using System.ComponentModel;

namespace Encryption
{
	/// <summary>
	/// Result of any encrption or decryption of <see cref="IBaseCipher"/> implementations.
	/// <para>
	///		Implements the <see cref="INotifyPropertyChanged"/> interface.
	/// </para>
	/// </summary>
	public class CipherResult : INotifyPropertyChanged
	{
		private string input;
		private string output;

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		/// <summary>
		/// The input <see langword="string"/> provided to a <see cref="IBaseCipher"/> to be either encrypted or decrypted
		/// </summary>
		public string Input
		{
			get => input;
			set
			{
				input = value;
				OnPropertyChanged(nameof(Input));
			}
		}

		/// <summary>
		/// The outputed <see langword="string"/> ecrypted or decrypted by a <see cref="IBaseCipher"/>.
		/// </summary>
		public string Output
		{
			get => output;
			set
			{
				output = value;
				OnPropertyChanged(nameof(Output));
			}
		}

		/// <summary>
		/// Constructs a <see cref="CipherResult"/> object.
		/// </summary>
		/// <param name="input">The input <see langword="string"/> provided to the <see cref="IBaseCipher"/></param>
		/// <param name="output">The outputed <see langword="string"/> ecrypted or decrypted by a <see cref="IBaseCipher"/>.</param>
		public CipherResult(string input, string output)
		{
			Input = input;
			Output = output;
		}

		/// <summary>
		/// Constructs a default <see cref="CipherResult"/>.
		/// </summary>
		public CipherResult()
		{
		}
	}
}