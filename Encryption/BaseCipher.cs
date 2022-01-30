using System;

namespace Encryption
{
	/// <summary>
	/// <see langword="abstract"/> class that provides simple implementation of the events defined by <see cref="IBaseCipher"/>
	/// </summary>
	public abstract class BaseCipher : IBaseCipher
	{
		public event EventHandler<EventArgs> EncryptionStarted;

		public event EventHandler<CipherEventArgs> EncryptionOngoing;

		public event EventHandler<CipherEventArgs> EncryptionFinished;

		protected void OnEncryptionStarted()
			=> EncryptionStarted?.Invoke(this, EventArgs.Empty);

		protected void OnEncryptionOngoing(float pourcentage, TimeSpan encryptionTime)
			=> EncryptionOngoing?.Invoke(this, new CipherEventArgs(pourcentage, encryptionTime));

		protected void OnEncryptionFinished(float pourcentage, TimeSpan encryptionTime)
			=> EncryptionFinished?.Invoke(this, new CipherEventArgs(pourcentage, encryptionTime));

		/// <summary>
		/// Encrypts an input <see cref="string"/> using the algorithm and options that extends this class.
		/// </summary>
		/// <param name="input">the input <see cref="string"/> to encrypt.</param>
		/// <returns><see cref="CipherResult"/></returns>
		public abstract CipherResult Encrypt(string input);

		/// <summary>
		/// Decrypts an input <see cref="string"/> using the algorithm and options that extends this class.
		/// </summary>
		/// <param name="input"></param>
		/// <returns><see cref="CipherResult"/></returns>
		public abstract CipherResult Decrypt(string input);
	}
}