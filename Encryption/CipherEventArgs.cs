using System;

namespace Encryption
{
	/// <summary>
	/// Event arguments for <see cref="IBaseCipher"/> Ongoing and Finished events
	/// </summary>
	public class CipherEventArgs : EventArgs
	{
		/// <summary>
		/// Constructs a <see cref="CipherEventArgs"/> object.
		/// </summary>
		/// <param name="pourcentage">The current pourcentage of the encryption.</param>
		/// <param name="encryptionTime">The current <see cref="TimeSpan"/> of the encryption</param>
		public CipherEventArgs(float pourcentage, TimeSpan encryptionTime)
		{
			Pourcentage = pourcentage;
			EncryptionTime = encryptionTime;
		}

		/// <summary>
		/// The current pourcentage of the encryption.
		/// </summary>
		public float Pourcentage { get; }

		/// <summary>
		/// The current <see cref="TimeSpan"/> of the encryption
		/// </summary>
		public TimeSpan EncryptionTime { get; }
	}
}