using System;

namespace Encryption
{
	public abstract class BaseCipher : IBaseCipher
	{
		
		public event EventHandler<EventArgs> EncryptionStarted;

		public event EventHandler<CipherEventArgs> EncryptionOngoing;

		public event EventHandler<CipherEventArgs> EncryptionFinished;

		protected void OnEncryptionStarted()
			=> EncryptionStarted?.Invoke(this, new EventArgs());

		protected void OnEncryptionOngoing(float pourcentage, TimeSpan encryptionTime)
			=> EncryptionOngoing?.Invoke(this, new CipherEventArgs(pourcentage, encryptionTime));

		protected void OnEncryptionFinished(float pourcentage, TimeSpan encryptionTime)
			=> EncryptionFinished?.Invoke(this, new CipherEventArgs(pourcentage, encryptionTime));

		public abstract CipherResult Encrypt(string input);
		public abstract CipherResult Decrypt(string input);
	}
}