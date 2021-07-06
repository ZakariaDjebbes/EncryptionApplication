using System;

namespace Encryption
{
	public abstract class BaseCipher
	{
		/// <summary>
		/// Fires whenever an encryption starts.
		/// </summary>
		public event EventHandler<EventArgs> EncryptionStarted;

		/// <summary>
		/// Fires whenever a character in the input is encrypted.
		/// <para>
		///		Contains the encryption arguments.
		/// </para>
		/// see <see cref="CipherEventArgs"/>
		/// </summary>
		public event EventHandler<CipherEventArgs> EncryptionOngoing;

		/// <summary>
		/// Fires whenever an encryption is finished.
		/// <para>
		///		Contains the encryption arguments.
		/// </para>
		/// see <see cref="CipherEventArgs"/>
		/// </summary>
		public event EventHandler<CipherEventArgs> EncryptionFinished;


		/// <summary>
		/// The Encryption result of the last encrypted input.
		/// </summary>
		public CipherResult EncryptionResult
		{
			get
			{
				if (encryptionResult == null)
					encryptionResult = new CipherResult(
						string.Empty,
						string.Empty);

				return encryptionResult;
			}

			protected set
			{
				encryptionResult = value;
			}
		}

		private CipherResult encryptionResult;

		protected void OnEncryptionStarted()
			=> EncryptionStarted?.Invoke(this, new EventArgs());

		protected void OnEncryptionOngoing(float pourcentage, double elapsedTime)
			=> EncryptionOngoing?.Invoke(this, new CipherEventArgs(pourcentage, elapsedTime));

		protected void OnEncryptionFinished(float pourcentage, double elapsedTime)
			=> EncryptionFinished?.Invoke(this, new CipherEventArgs(pourcentage, elapsedTime));

		public abstract CipherResult Encrypt(string input);
	}
}