using System;
using Encryption.SymmetricKey;
using NUnit.Framework;

namespace Encryption.Test
{
	[TestFixture]
	public class VigenereCipherTests
	{
		private VigenereCipher vigenereCipher;

		[OneTimeSetUp]
		public void Setup()
		{
			vigenereCipher = new("required");
		}

		public void TestEncrypt(object arg1, object arg2, object arg3)
		{
			throw new NotImplementedException();
		}

		public void TestDecrypt(object arg1, object arg2, object arg3)
		{
			throw new NotImplementedException();
		}
	}
}