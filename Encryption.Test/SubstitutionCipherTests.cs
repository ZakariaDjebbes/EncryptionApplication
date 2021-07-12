using System;
using Encryption.Substitution;
using NUnit.Framework;

namespace Encryption.Test
{
	[TestFixture]
	internal class SubstitutionCipherTests
	{
		private SubstitutionCipher substitutionCipher;

		[OneTimeSetUp]
		public void TestSetup()
		{
			substitutionCipher = new();
		}

		public void TestEncrypt(string input, string output, string characters, string substitutions, bool caseSpecific)
		{
			throw new NotImplementedException();
		}

		public void TestDecrypt(string input, string output, string characters, string substitutions, bool caseSpecific)
		{
			throw new NotImplementedException();
		}
	}
}