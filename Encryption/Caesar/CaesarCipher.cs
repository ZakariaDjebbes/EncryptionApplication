using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Encryption.Extentions;

namespace Encryption.Caesar
{
	/// <summary>
	/// Caesar encryption class
	/// </summary>
	public class CaesarCipher : BaseCipher
	{
		private ICollection<char> alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

		/// <summary>
		/// The Alphabet used to encrypt the input.
		/// The Alphabest should not have duplicates, an exception is thrown otherwise.
		/// <para>Default abcdefghijklmnopqrstuvwxyz</para>
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		public ICollection<char> Alphabet
		{
			get => alphabet;
			set
			{
				if (!IsAlphabetValid(value.ToArray()))
					throw new ArgumentException("Cannot have duplicates in alphabet", nameof(Alphabet));

				alphabet = value;
			}
		}

		/// <summary>
		/// The Caesar shift applied by the encryption.
		/// <para>Default 1.</para>
		/// </summary>
		public int Shift { get; set; } = 1;

		/// <summary>
		/// Should the encryption keep casing.
		/// </summary>
		public bool KeepCase { get; set; } = true;

		/// <summary>
		/// Constructs a CaesarCipher with a default alphabet and shift.
		/// </summary>
		public CaesarCipher()
		{
		}

		/// <summary>
		/// Constructs a CaesarCipher object with a user defined alphabet and shift.
		/// </summary>
		/// <param name="alphabet">The alphabet used for shifting</param>
		/// <param name="shift">Caesar shift</param>
		public CaesarCipher(ICollection<char> alphabet, int shift, bool caseSpecific)
		{
			Alphabet = alphabet;
			Shift = shift;
			KeepCase = caseSpecific;
		}

		/// <summary>
		/// <para>
		///		Encrypts an input string using the caesar cipher for a specific alphabet.
		///		Characters that are not included in the alphabet will be kept as is.
		/// </para>
		/// </summary>
		/// <param name="inputText">The text to encrypt</param>
		/// <returns>
		///		<see cref="CipherResult"/>
		/// </returns>
		public override CipherResult Encrypt(string inputText)
		{
			if (inputText is null)
			{
				throw new ArgumentNullException(nameof(inputText));
			}

			var alphabetList = Alphabet.ToList();
			StringBuilder builder = new StringBuilder();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (int i = 0; i < inputText.Length; i++)
			{
				char res = inputText[i];

				if (alphabetList.CaseInsensitiveContains(inputText[i]))
				{
					var index = alphabetList.CaseInsensitiveIndexOf(inputText[i]);
					int offset = (index + Shift) % Alphabet.Count;
					char newChar = alphabetList[offset];

					if ((KeepCase && char.IsLower(inputText[i])) || !KeepCase)
						res = char.ToLower(newChar);
					else
						res = char.ToUpper(newChar);
				}

				builder.Append(res);
				OnEncryptionOngoing(i * 100 / inputText.Length, watch.Elapsed.TotalSeconds);
			}

			OnEncryptionFinished(100, watch.Elapsed.TotalSeconds);
			watch.Stop();
			EncryptionResult = new CipherResult(
				inputText,
				builder.ToString());

			return EncryptionResult;
		}

		private static bool IsAlphabetValid(char[] alphabet)
		{
			for (int i = 0; i < alphabet.Length; i++)
			{
				alphabet[i] = char.ToLower(alphabet[i]);
			}

			return alphabet.Length == alphabet.Distinct().Count();
		}
	}
}