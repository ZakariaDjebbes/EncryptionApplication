using Encryption.Caesar;
using NUnit.Framework;

namespace Encryption.Test
{
	[TestFixture]
	internal class CaesarCipherTests
	{
		private CaesarCipher caesarEncryption;
		private string input;

		[OneTimeSetUp]
		public void TestSetup()
		{
			caesarEncryption = new();
			input = "TestInput";
			caesarEncryption.Alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
		}

		[Test]
		public void TestShiftOneKeepCaseSuccess()
		{
			string output = "UftuJoqvu";

			caesarEncryption.Shift = 1;
			caesarEncryption.KeepCase = true;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}

		[Test]
		public void TestShiftFiveKeepCaseSuccess()
		{
			string output = "YjxyNsuzy";

			caesarEncryption.KeepCase = true;
			caesarEncryption.Shift = 5;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}

		[Test]
		public void TestShiftFiveKeepCaseFail()
		{
			string output = "YjxyNsuza";

			caesarEncryption.KeepCase = true;
			caesarEncryption.Shift = 5;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreNotEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}

		[Test]
		public void TestShiftOneKeepCaseFail()
		{
			string output = "UftuJbqvu";

			caesarEncryption.Shift = 1;
			caesarEncryption.KeepCase = true;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreNotEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}

		[Test]
		public void TestShiftOneNoKeepCaseSuccess()
		{
			string output = "uftujoqvu";

			caesarEncryption.Shift = 1;
			caesarEncryption.KeepCase = false;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}

		[Test]
		public void TestShiftOneNoKeepCaseFail()
		{
			string output = "uftujoqgu";

			caesarEncryption.Shift = 1;
			caesarEncryption.KeepCase = false;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreNotEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}


		[Test]
		public void TestShiftFiveNoKeepCaseSuccess()
		{
			string output = "yjxynsuzy";

			caesarEncryption.Shift = 5;
			caesarEncryption.KeepCase = false;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}

		[Test]
		public void TestShiftFiveNoKeepCaseFail()
		{
			string output = "yjxynsrzy";

			caesarEncryption.Shift = 5;
			caesarEncryption.KeepCase = false;

			var res = caesarEncryption.Encrypt(input);

			Assert.AreNotEqual(res.EncryptedText, output);
			Assert.AreEqual(res.OriginalText, input);
		}
	}
}