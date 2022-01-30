using Encryption;

namespace EncryptionApp.ViewModels
{
	internal abstract class BaseCipherViewModel : BaseViewModel
	{
		protected readonly EncryptionAppViewModel Parent;

		public abstract CipherResult Encrypt(string input);

		public abstract CipherResult Decrypt(string input);

		protected BaseCipherViewModel(EncryptionAppViewModel parent)
		{
			Parent = parent;
		}

		protected virtual void EncryptionStarted(object sender, CipherEventArgs e)
		{
		}

		protected virtual void EncryptionOngoing(object sender, CipherEventArgs e)
		{
			Parent.ElapsedTime = e.EncryptionTime.TotalSeconds;
			Parent.Progress = e.Pourcentage;
		}

		protected virtual void EncryptionFinished(object sender, CipherEventArgs e)
		{
			Parent.ElapsedTime = e.EncryptionTime.TotalSeconds;
			Parent.Progress = e.Pourcentage;
		}
	}
}