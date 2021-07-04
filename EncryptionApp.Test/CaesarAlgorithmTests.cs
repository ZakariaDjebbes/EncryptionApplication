using EncryptionApp.Encryption;
using NUnit.Framework;

namespace EncryptionApp.Test
{
	[TestFixture]
	internal class CaesarAlgorithmTests
	{
		[Test]
		public void TestCaesarEcryptionWithoutNumbersShiftOneSuccess()
		{
			string input = "TestInput";
			string output = "UftuJoqvu";

			CaesarEncryption caesarEncryption = new();
			var res = caesarEncryption.EncryptText(input);

			Assert.AreEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
			Assert.AreEqual(res.Status, EncryptionResultStatus.Done);
		}

		[Test]
		public void TestCaesarEcryptionWithoutNumbersShiftFiveSuccess()
		{
			string input = "TestInput";
			string output = "YjxyNsuzy";
			int shift = 5;

			CaesarEncryption caesarEncryption = new();
			var res = caesarEncryption.EncryptText(input, shift);

			Assert.AreEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
			Assert.AreEqual(res.Status, EncryptionResultStatus.Done);
		}

		[Test]
		public void TestCaesarEcryptionWithoutNumbersShiftFiveFail()
		{
			string input = "TestInput";
			string output = "YjxyNsuza";
			int shift = 5;

			CaesarEncryption caesarEncryption = new();
			var res = caesarEncryption.EncryptText(input, shift);

			Assert.AreNotEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
			Assert.AreEqual(res.Status, EncryptionResultStatus.Done);
		}

		[Test]
		public void TestCaesarEcryptionWithoutNumbersShiftOneFail()
		{
			string input = "TestInput";
			string output = "UftuJbqvu";

			CaesarEncryption caesarEncryption = new();
			var res = caesarEncryption.EncryptText(input);

			Assert.AreNotEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
			Assert.AreEqual(res.Status, EncryptionResultStatus.Done);
		}
	}
}