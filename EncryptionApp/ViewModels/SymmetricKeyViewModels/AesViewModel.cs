using Encryption;

namespace EncryptionApp.ViewModels.SymmetricKeyViewModels
{
	internal class AesViewModel : BaseCipherViewModel
	{
		public AesViewModel(EncryptionAppViewModel parent) : base(parent)
		{
		}

		public override CipherResult Decrypt(string input)
		{
			throw new System.NotImplementedException();
		}

		public override CipherResult Encrypt(string input)
		{
			throw new System.NotImplementedException();
		}
	}
}