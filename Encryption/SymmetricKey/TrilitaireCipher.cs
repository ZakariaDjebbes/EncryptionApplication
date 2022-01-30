using System;

namespace Encryption.SymmetricKey;

public class TrilitereCipher : BaseCipher
{
	public enum Trilitaire
	{
	}

	public bool MaintainNonAlphabeticCharacters { get; set; }

	public TrilitereCipher(bool maintainNonAlphabeticCharacters)
	{
	}

	public override CipherResult Encrypt(string input)
	{
		OnEncryptionStarted();

		OnEncryptionFinished(100, TimeSpan.MaxValue);
		throw new NotImplementedException();
	}

	public override CipherResult Decrypt(string input)
	{
		OnEncryptionStarted();

		OnEncryptionFinished(100, TimeSpan.MaxValue);
		throw new NotImplementedException();
	}
}