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

		[TestCase("a super secret message", "a koeii kieiim wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, true)]
		[TestCase("!A super secret messaGe.", "!A koeii kieiim wiqssAt.", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, true)]
		[TestCase("a super s<!>ecreT message", "a koeii k<!>ieiiM wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, true)]
		[TestCase("another secret message", "aacmoii ciarrh flwjkkc", "abcdefghijk<!>lmnopqrstuvwxyz", "anotherkey", true, true)]
		[TestCase("?A Super secret !!!!Message.", "a koeii kieiim wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", false, false)]
		[TestCase("?a super secret message.", "?a koeii kieiim wiqssat.", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", false, true)]
		[TestCase("A... super secret message", "A koeii kieiim wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, false)]
		public void TestEncrypt(string input, string output, string alphabet, string key, bool keepCase, bool keepCharacters)
		{
			vigenereCipher.Alphabet = alphabet.ToCharArray();
			vigenereCipher.KeepCase = keepCase;
			vigenereCipher.KeepUnknownCharacters = keepCharacters;
			vigenereCipher.Key = key;

			var res = vigenereCipher.Encrypt(input);

			Assert.AreEqual(output, res.Output);
			Assert.AreEqual(input, res.Input);
		}

		//[TestCase("a super secret message", "a koeii kieiim wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, true)]
		//[TestCase("!A super secret messaGe.", "!A koeii kieiim wiqssAt.", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, true)]
		//[TestCase("a super s<!>ecreT message", "a koeii k<!>ieiiM wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, true)]
		//[TestCase("another secret message", "aacmoii ciarrh flwjkkc", "abcdefghijk<!>lmnopqrstuvwxyz", "anotherkey", true, true)]
		//[TestCase("?A Super secret !!!!Message.", "a koeii kieiim wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", false, false)]
		//[TestCase("?a super secret message.", "?a koeii kieiim wiqssat.", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", false, true)]
		//[TestCase("A... super secret message", "A koeii kieiim wiqssat", "abcdefghijklmnopqrstuvwxyz", "asupersecretkey", true, false)]
		//public void TestDecrypt(string input, string output, string alphabet, string key, bool keepCase, bool keepCharacters)
		//{
		//	vigenereCipher.Alphabet = alphabet.ToCharArray();
		//	vigenereCipher.KeepCase = keepCase;
		//	vigenereCipher.KeepUnknownCharacters = keepCharacters;
		//	vigenereCipher.Key = key;

		//	var res = vigenereCipher.Decrypt(input);

		//	Assert.AreEqual(res.Output, output);
		//	Assert.AreEqual(res.Input, input);
		//}

		[TestCase("", "abcdefghijklmnopqrstuvwxyz", typeof(ArgumentException))]
		[TestCase("?", "abcdefghijklmnopqrstuvwxyz", typeof(ArgumentException))]
		[TestCase("success", "abcdefghijklmnopqrstuvwxyz", null)]
		public void TestSetKey(string key, string alphabet, Type exception)
		{
			if (exception != null)
			{
				Assert.Throws(exception, delegate
				{
					vigenereCipher.Alphabet = alphabet.ToCharArray();
					vigenereCipher.Key = key;
				});
			}
			else
			{
				Assert.DoesNotThrow(delegate
				{
					vigenereCipher.Alphabet = alphabet.ToCharArray();
					vigenereCipher.Key = key;
				});
			}
		}

		[TestCase("", typeof(ArgumentException))]
		[TestCase("Aca", typeof(ArgumentException))]
		[TestCase("abcdefghijklmnopqrstuvwxyz", null)]
		public void TestSetAlphabet(string alphabet, Type exception)
		{
			if (exception != null)
			{
				Assert.Throws(exception, delegate
				{
					vigenereCipher.Alphabet = alphabet.ToCharArray();
				});
			}
			else
			{
				Assert.DoesNotThrow(delegate
				{
					vigenereCipher.Alphabet = alphabet.ToCharArray();
				});
			}
		}
	}
}