using System.Collections.Generic;
using System.Linq;

namespace Encryption.Extentions
{
	internal static class CharExtensions
	{
		/// <summary>
		/// Finds the index of a <see cref="char"/> in a <see cref="IList{char}"/> ignoring case.
		/// </summary>
		/// <param name="character">The <see cref="char"/> to look for</param>
		/// <returns>The index of the <see cref="char"/> in the <see cref="IList{T}{char}"/> or -1 if it does not exist.</returns>
		internal static int CaseInsensitiveIndexOf(this IList<char> collection, char character)
		{
			for (var i = 0; i < collection.Count; i++)
			{
				if (collection[i] == char.ToLower(character) || collection[i] == char.ToUpper(character))
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Checks if a <see cref="IList{char}"/> contains a <see cref="char"/> ignoring case.
		/// </summary>
		/// <param name="character">The <see cref="char"/> to look for</param>
		/// <returns>True if the <see cref="IList{char}"/> contains the <see cref="char"/>, false otherwise.</returns>
		internal static bool CaseInsensitiveContains(this IList<char> collection, char character)
			=> collection.Any(t => t == char.ToLower(character) || t == char.ToUpper(character));

		/// <summary>
		/// Returns a lower case copy of an <see cref="IList{T}"/>.
		/// </summary>
		/// <returns><see cref="IList{char}"/></returns>
		internal static IList<char> ToLower(this IList<char> collection)
		{
			var result = new char[collection.Count];

			for (var i = 0; i < collection.Count; i++)
			{
				if (char.IsLetter(collection[i]) && char.IsUpper(collection[i]))
				{
					result[i] = char.ToLower(collection[i]);
				}
				else
				{
					result[i] = collection[i];
				}
			}

			return result;
		}
	}
}