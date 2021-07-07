using System.Collections.Generic;
using System.Linq;
using Encryption.Extentions;
using NUnit.Framework;

namespace Encryption.Test
{
	[TestFixture]
	internal class CharExtentionsTests
	{
		private List<char> testData;

		[OneTimeSetUp]
		public void Setup()
		{
			testData = "abcdefghijklmnopqrstuvwxy".ToList(); //Doesnt contain z
		}

		[Test]
		public void TestCaseInsensitiveIndexOfLowerSuccess()
		{
			var res = testData.CaseInsensitiveIndexOf('c');

			Assert.AreNotEqual(res, -1);
			Assert.AreEqual(res, 2);
		}

		[Test]
		public void TestCaseInsensitiveIndexOfUpperSuccess()
		{
			var res = testData.CaseInsensitiveIndexOf('C');

			Assert.AreNotEqual(res, -1);
			Assert.AreEqual(res, 2);
		}

		[Test]
		public void TestCaseInsensitiveIndexOfLowerFail()
		{
			var res = testData.CaseInsensitiveIndexOf('z');

			Assert.AreEqual(res, -1);
		}

		[Test]
		public void TestCaseInsensitiveIndexOfUpperFail()
		{
			var res = testData.CaseInsensitiveIndexOf('Z');

			Assert.AreEqual(res, -1);
		}

		[Test]
		public void TestCaseInsensitiveContainsLowerSuccess()
		{
			var res = testData.CaseInsensitiveContains('c');

			Assert.AreEqual(res, true);
		}
		
		[Test]
		public void TestCaseInsensitiveContainsUpperSuccess()
		{
			var res = testData.CaseInsensitiveContains('C');

			Assert.AreEqual(res, true);
		}

		[Test]
		public void TestCaseInsensitiveContainsLowerFail()
		{
			var res = testData.CaseInsensitiveContains('z');

			Assert.AreEqual(res, false);
		}

		[Test]
		public void TestCaseInsensitiveContainsUpperFail()
		{
			var res = testData.CaseInsensitiveContains('Z');

			Assert.AreEqual(res, false);
		}
	}
}