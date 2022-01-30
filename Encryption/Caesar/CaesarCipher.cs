using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Encryption.Extentions;

namespace Encryption.Caesar
{
	/// <summary>
	/// Provides an implementation of the rotation cipher, also called caesar cipher.
	/// </summary>
	public class CaesarCipher : BaseCipher
	{
		private IList<char> alphabet;

		/// <summary>
		/// The Alphabet <see cref="IList{T}"/> used by <see cref="Encrypt(string)"/> to encrypt the input.
		/// The Alphabet must not have duplicates, an <see cref="ArgumentException"/> is thrown otherwise.
		/// <para>Default: abcdefghijklmnopqrstuvwxyz</para>
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		public IList<char> Alphabet
		{
			get => alphabet;
			set
			{
				if (!IsAlphabetValid(value.ToArray()))
				{
					throw new ArgumentException("Alphabet must contain atleast 2 characters and have no duplicate characters (ignoring case).", nameof(Alphabet));
				}

				alphabet = value;
			}
		}

		/// <summary>
		/// The Caesar shift applied by the <see cref="Encrypt(string)"/>.
		/// <para>Default: 1.</para>
		/// </summary>
		public int Shift { get; set; }

		/// <summary>
		/// Should the <see cref="Encrypt(string)"/> keep casing.
		/// <para>
		/// Default: <see langword="true"/>.
		/// </para>
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
		/// <param name="alphabet">The alphabet <see cref="IList{T}"/> used for shifting</param>
		/// <param name="shift">Caesar shift</param>
		/// <param name="caseSpecific">Should the shift be case specific or no?</param>
		public CaesarCipher(IList<char> alphabet, int shift, bool caseSpecific)
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

			StringBuilder builder = new();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (var i = 0; i < input.Length; i++)
			{
				var res = input[i];

				if (Alphabet.CaseInsensitiveContains(input[i]))
				{
					var index = Alphabet.CaseInsensitiveIndexOf(input[i]);
					var offset = ((index + Shift) % Alphabet.Count + Alphabet.Count) % Alphabet.Count;
					var newChar = Alphabet[offset];

					if (KeepCase && char.IsUpper(input[i]))
						res = char.ToUpper(newChar);
					else
						res = char.ToLower(newChar);
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

			StringBuilder builder = new();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (var i = 0; i < input.Length; i++)
			{
				var res = input[i];

				if (Alphabet.CaseInsensitiveContains(input[i]))
				{
					var index = Alphabet.CaseInsensitiveIndexOf(input[i]);
					// C# mod things, unlucky
					var offset = ((index - Shift) % Alphabet.Count + Alphabet.Count) % Alphabet.Count;
					var newChar = Alphabet[offset];

					if (KeepCase && char.IsUpper(input[i]))
						res = char.ToUpper(newChar);
					else
						res = char.ToLower(newChar);
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

		private static bool IsAlphabetValid(IList<char> alphabet)
		{
			// Alphabet must contain at least two characters
			if (alphabet.Count < 2)
				return false;

			// Alphabet must not contain any duplicated characters, even if the case of characters isn't specific.
			for (var i = 0; i < alphabet.Count; i++)
			{
				alphabet[i] = char.ToLower(alphabet[i]);
			}

			return alphabet.Count == alphabet.Distinct().Count();
		}
	}
}