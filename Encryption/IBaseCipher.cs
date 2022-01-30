using System;

namespace Encryption
{
	/// <summary>
	/// The interface for all cipher classes that either implement <see cref="IBaseCipher"/> or extend <see cref="BaseCipher"/>
	/// </summary>
	public interface IBaseCipher
	{
		/// <summary>
		/// Fires whenever an encryption is finished.
		/// <para>
		///		Contains the encryption arguments.
		/// </para>
		/// see <see cref="CipherEventArgs"/>
		/// </summary>
		event EventHandler<CipherEventArgs> EncryptionFinished;

		/// <summary>
		/// Fires whenever a character in the input is encrypted.
		/// <para>
		///		Contains the encryption arguments.
		/// </para>
		/// see <see cref="CipherEventArgs"/>
		/// </summary>
		event EventHandler<CipherEventArgs> EncryptionOngoing;

		/// <summary>
		/// Fires whenever an encryption starts.
		/// </summary>
		event EventHandler<EventArgs> EncryptionStarted;

		/// <summary>
		/// Encrypts an input <see cref="string"/> using the algorithm and options that extends this class.
		/// </summary>
		/// <param name="input">the input <see cref="string"/> to encrypt.</param>
		/// <returns><see cref="CipherResult"/></returns>
		CipherResult Encrypt(string input);

		/// <summary>
		/// Decrypts an input <see cref="string"/> using the algorithm and options that extends this class.
		/// </summary>
		/// <param name="input"></param>
		/// <returns><see cref="CipherResult"/></returns>
		CipherResult Decrypt(string input);
	}
}