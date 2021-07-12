using Encryption;

namespace EncryptionApp.ViewModels
{
	internal abstract class BaseCipherViewModel : BaseViewModel
	{
		protected readonly EncryptionAppViewModel parent;

		public abstract CipherResult Encrypt(string input);

		public abstract CipherResult Decrypt(string input);

		public BaseCipherViewModel(EncryptionAppViewModel parent)
		{
			this.parent = parent;
		}

		protected virtual void EncryptionStarted(object sender, CipherEventArgs e)
		{
		}

		protected virtual void EncryptionOngoing(object sender, CipherEventArgs e)
		{
			parent.ElapsedTime = e.EncryptionTime.TotalSeconds;
			parent.Progress = e.Pourcentage;
		}

		protected virtual void EncryptionFinished(object sender, CipherEventArgs e)
		{
			parent.ElapsedTime = e.EncryptionTime.TotalSeconds;
			parent.Progress = e.Pourcentage;
		}
	}
}