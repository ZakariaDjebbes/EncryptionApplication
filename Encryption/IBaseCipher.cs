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

		CipherResult Encrypt(string input);
		CipherResult Decrypt(string input);
	}
}