using System;
using Encryption.Extentions;
using NUnit.Framework;

namespace Encryption.Test
{
	[TestFixture]
	internal class CharExtentionsTests
	{
		[TestCase("abcdefghijklmnopqrstuvwxyz", 'c', 2)]
		[TestCase("abcdefghijklmnopqrstuvwxyz", 'Z', 25)]
		[TestCase("AsString", 's', 1)]
		[TestCase("AaAaCc", 'a', 0)]
		[TestCase("abcdefghijklmnopqrstuvwxyz", '[', -1)]
		[TestCase("", 'd', -1)]
		public void TestCaseInsensitiveIndexOf(string list, char character, int index)
			=> Assert.AreEqual(list.ToCharArray().CaseInsensitiveIndexOf(character), index);

		[TestCase("abcdefghijklmnopqrstuvwxyz", 'c', true)]
		[TestCase("abcdefghijklmnopqrstuvwxyz", 'Z', true)]
		[TestCase("Astring", 's', true)]
		[TestCase("AaAaCc", 'a', true)]
		[TestCase("abcdefghijklmnopqrstuvwxyz", '[', false)]
		[TestCase("", 'd', false)]
		public void TestCaseInsensitiveContains(string list, char character, bool contains)
			=> Assert.AreEqual(list.ToCharArray().CaseInsensitiveContains(character), contains);

		[TestCase("Input", "input")]
		[TestCase("", "")]
		[TestCase("a", "a")]
		[TestCase("AaA", "aaa")]
		[TestCase("aAa", "aaa")]
		public void TestToLower(string input, string lower)
			=> Assert.AreEqual(input.ToCharArray().ToLower(), lower.ToCharArray());
	}
}