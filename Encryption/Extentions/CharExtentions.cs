using System.Collections.Generic;

namespace Encryption.Extentions
{
	internal static class CharExtentions
	{

		/// <summary>
		/// Finds the index of a character in a <see cref="List{char}"/> ignoring case.
		/// </summary>
		/// <param name="character">The character to look for</param>
		/// <returns>The index of the character or -1 if it does not exist.</returns>
		internal static int CaseInsensitiveIndexOf(this List<char> list, char character)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == char.ToLower(character) || list[i] == char.ToUpper(character))
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Checks if a <see cref="List{char}"/> contains a character.
		/// </summary>
		/// <param name="character">The character to look for</param>
		/// <returns>True if the list contains the character, false otherwise.</returns>
		internal static bool CaseInsensitiveContains(this List<char> list, char character)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == char.ToLower(character) || list[i] == char.ToUpper(character))
				{
					return true;
				}
			}

			return false;
		}
	}
}