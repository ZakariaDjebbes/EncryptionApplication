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
		private ICollection<char> alphabet;

		/// <summary>
		/// The Alphabet used by <see cref="Encrypt(string)"/> to encrypt the input.
		/// The Alphabet should not have duplicates, an <see cref="ArgumentException"/> is thrown otherwise.
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
		/// The Caesar shift applied by the <see cref="Encrypt(string)"/>.
		/// <para>Default 1.</para>
		/// </summary>
		public int Shift { get; set; }

		/// <summary>
		/// Should the <see cref="Encrypt(string)"/> keep casing.
		/// </summary>
		public bool KeepCase { get; set; }

		/// <summary>
		/// Constructs a CaesarCipher with a default alphabet, shift and casing.
		/// </summary>
		public CaesarCipher()
		{
			Shift = 1;
			KeepCase = true;
			Alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
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
		///		Encrypts an input <see cref="string"/> using the caesar cipher for the <see cref="Alphabet"/>.
		///		Characters that are not included in the <see cref="Alphabet"/> will be kept as is.
		/// </para>
		/// </summary>
		/// <param name="input">The <see cref="string"/> to encrypt</param>
		/// <returns>
		///		<see cref="CipherResult"/>
		/// </returns>
		public override CipherResult Encrypt(string input)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			var alphabetList = Alphabet.ToList();
			StringBuilder builder = new();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (int i = 0; i < input.Length; i++)
			{
				char res = input[i];

				if (alphabetList.CaseInsensitiveContains(input[i]))
				{
					var index = alphabetList.CaseInsensitiveIndexOf(input[i]);
					int offset = (index + Shift) % Alphabet.Count;
					char newChar = alphabetList[offset];

					if ((KeepCase && char.IsLower(input[i])) || !KeepCase)
						res = char.ToLower(newChar);
					else
						res = char.ToUpper(newChar);
				}

				builder.Append(res);
				OnEncryptionOngoing(i * 100 / input.Length, watch.Elapsed);
			}

			OnEncryptionFinished(100, watch.Elapsed);
			watch.Stop();

			return new CipherResult(
				input,
				builder.ToString());
		}

		/// <summary>
		///		Decrypts an input <see cref="string"/> using the caesar cipher for the <see cref="Alphabet"/>.
		///		Characters that are not included in the <see cref="Alphabet"/> will be kept as is. 
		/// </summary>
		/// <param name="input">The <see cref="string"/> to decrypt</param>
		/// <returns>
		///		<see cref="CipherResult"/>
		/// </returns>
		public override CipherResult Decrypt(string input)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			var alphabetList = Alphabet.ToList();
			StringBuilder builder = new();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (int i = 0; i < input.Length; i++)
			{
				char res = input[i];

				if (alphabetList.CaseInsensitiveContains(input[i]))
				{
					var index = alphabetList.CaseInsensitiveIndexOf(input[i]);
					int offset = (index - Shift) % Alphabet.Count;
					char newChar = alphabetList[offset];

					if ((KeepCase && char.IsLower(input[i])) || !KeepCase)
						res = char.ToLower(newChar);
					else
						res = char.ToUpper(newChar);
				}

				builder.Append(res);
				OnEncryptionOngoing(i * 100 / input.Length, watch.Elapsed);
			}

			OnEncryptionFinished(100, watch.Elapsed);
			watch.Stop();

			return new CipherResult(
				input,
				builder.ToString());
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