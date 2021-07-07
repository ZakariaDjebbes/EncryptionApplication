using Encryption;

namespace EncryptionApp.ViewModels
{
	internal abstract class BaseCipherViewModel : BaseViewModel
	{
		public abstract CipherResult Encrypt(string input);
		public abstract CipherResult Decrypt(string input);
	}
}