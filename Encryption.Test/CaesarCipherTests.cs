using System;
using Encryption.Caesar;
using NUnit.Framework;

namespace Encryption.Test
{
	[TestFixture]
	internal class CaesarCipherTests
	{
		private CaesarCipher caesarEncryption;

		[OneTimeSetUp]
		public void TestSetup()
		{
			caesarEncryption = new CaesarCipher();
		}

		[TestCase("This message shall be secret!", "Aopz tlzzhnl zohss il zljyla!", "abcdefghijklmnopqrstuvwxyz", 7, true)]
		[TestCase("This message shall be secret!", "aopz tlzzhnl zohss il zljyla!", "abcdefghijklmnopqrstuvwxyz", 7, false)]
		[TestCase("This message shall be secret!", "Lzak ewkksyw kzsdd tw kwujwl!", "abcdefghijklmnopqrstuvwxyz", 18, true)]
		[TestCase("This message shall be secret!", "pdeo iaooxca odxhh ya oaznapw", "abcdefghijklmnopqrstuvwxyz!", 23, false)]
		[TestCase("Another Message, but STILL secret!", "Frtylhw Qhxxfihs gzy XYNPP xh:whym", "abcdefg:)h!ijklmn,opqrstuvwxyz", 155, true)]
		[TestCase("abc", "abc", "abc", 3000, true)]
		public void TestEncrypt(string input, string expected, string alphabet, int shift, bool keepCase)
		{
			caesarEncryption.Shift = shift;
			caesarEncryption.KeepCase = keepCase;
			caesarEncryption.Alphabet = alphabet.ToCharArray();

			var res = caesarEncryption.Encrypt(input);

			Assert.AreEqual(expected, res.Output);
			Assert.AreEqual(input, res.Input);
		}

		[TestCase("This message shall be secret!", "Aopz tlzzhnl zohss il zljyla!", "abcdefghijklmnopqrstuvwxyz", 7, true)]
		[TestCase("this message shall be secret!", "aopz tlzzhnl zohss il zljyla!", "abcdefghijklmnopqrstuvwxyz", 7, false)]
		[TestCase("This message shall be secret!", "Lzak ewkksyw kzsdd tw kwujwl!", "abcdefghijklmnopqrstuvwxyz", 18, true)]
		[TestCase("this message shall be secret!", "pdeo iaooxca odxhh ya oaznapw", "abcdefghijklmnopqrstuvwxyz!", 23, false)]
		[TestCase("Another Message, but STILL secret!", "Frtylhw Qhxxfihs gzy XYNPP xh:whym", "abcdefg:)h!ijklmn,opqrstuvwxyz", 155, true)]
		[TestCase("abc", "abc", "abc", 3000, true)]
		public void TestDecrypt(string expected, string input, string alphabet, int shift, bool keepCase)
		{
			caesarEncryption.Shift = shift;
			caesarEncryption.KeepCase = keepCase;
			caesarEncryption.Alphabet = alphabet.ToCharArray();

			var res = caesarEncryption.Decrypt(input);

			Assert.AreEqual(expected, res.Output);
			Assert.AreEqual(input, res.Input);
		}

		[TestCase("", typeof(ArgumentException), Description = "The alphabet should not be empty")]
		[TestCase("Aca", typeof(ArgumentException), Description = "The alphabet should not contain duplications")]
		[TestCase("a", typeof(ArgumentException), Description = "The alphabet should contain atleast two distinct characters")]
		[TestCase("abcdefghijklmnopqrstuvwxyz", null)]
		public void TestSetAlphabet(string alphabet, Type exception)
		{
			if (exception != null)
			{
				Assert.Throws(exception, () => { caesarEncryption.Alphabet = alphabet.ToCharArray(); });
			}
			else
			{
				Assert.DoesNotThrow(delegate
				{
					caesarEncryption.Alphabet = alphabet.ToCharArray();
				});
			}
		}
	}
}