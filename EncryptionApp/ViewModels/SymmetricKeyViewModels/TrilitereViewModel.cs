using Encryption;

namespace EncryptionApp.ViewModels.SymmetricKeyViewModels
{
	internal class TrilitereViewModel : BaseCipherViewModel
	{
		public override CipherResult Decrypt(string input)
		{
			throw new System.NotImplementedException();
		}

		public override CipherResult Encrypt(string input)
		{
			throw new System.NotImplementedException();
		}

		public TrilitereViewModel(EncryptionAppViewModel parent) : base(parent)
		{
		}
	}
}