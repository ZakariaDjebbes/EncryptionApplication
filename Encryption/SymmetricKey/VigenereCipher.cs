using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Encryption.Extentions;

namespace Encryption.SymmetricKey
{
	public class VigenereCipher : BaseCipher
	{
		private IList<char> alphabet;
		private string key;

		/// <summary>
		/// The Alphabet <see cref="IList{T}"/> used by <see cref="Encrypt(string)"/> to encrypt the input.
		/// The Alphabet can not have duplicates or contain less than 1 item, an <see cref="ArgumentException"/> is thrown otherwise.
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
		/// The key used by <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/>.
		/// The key cannot be empty and all its characters must be included in the alphabet,
		/// an <see cref="ArgumentException"/> is thrown otherwise
		/// </summary>
		public string Key
		{
			get => key;
			set
			{
				if (!IsKeyValid(value))
				{
					throw new ArgumentException($"The value of {nameof(Key)} cannot be empty", nameof(Key));
				}

				key = value;
			}
		}

		/// <summary>
		/// Should the <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/> maintain the case of the input?
		/// <para>
		/// Default: <see langword="true"/>.
		/// </para>
		/// </summary>
		public bool KeepCase { get; set; }

		/// <summary>
		/// Should the <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/> maintain the
		/// characters unincluded in the alphabet?
		/// <para>
		///	Default: <see langword="true"/>
		/// </para>
		/// </summary>
		public bool KeepUnknownCharacters { get; set; }

		/// <summary>
		/// Constructs a <see cref="VigenereCipher"/> with a user defined <see cref="Key"/>
		/// </summary>
		/// <param name="key">The key used by <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/>.</param>
		public VigenereCipher(string key)
		{
			Alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
			Key = key;
		}

		/// <summary>
		/// Constructs a <see cref="VigenereCipher"/> with a user defined <see cref="Key"/> and <see cref="Alphabet"/>.
		/// </summary>
		/// <param name="alphabet">The alphabet <see cref="IList{T}"/> used for encryption.</param>
		/// <param name="key">The key used by <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/>.</param>
		public VigenereCipher(IList<char> alphabet, string key)
		{
			Alphabet = alphabet;
			Key = key;
		}

		/// <summary>
		/// Constructs a <see cref="VigenereCipher"/> with a user defined attributes.
		/// </summary>
		/// <param name="alphabet">The alphabet <see cref="IList{T}"/> used for encryption.</param>
		/// <param name="key">The key used by <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/>.</param>
		/// <param name="keepCase">Should the <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/>
		/// maintain the case of the input?</param>
		/// <param name="keepUnknownCharacters">Should the <see cref="Encrypt(string)"/> and <see cref="Decrypt(string)"/>
		/// maintain the characters unincluded in the alphabet?</param>
		public VigenereCipher(IList<char> alphabet, string key, bool keepCase, bool keepUnknownCharacters)
		{
			Alphabet = alphabet;
			Key = key;
			KeepCase = keepCase;
			KeepUnknownCharacters = keepUnknownCharacters;
		}

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
					var shift = Alphabet.CaseInsensitiveIndexOf(Key[i % Key.Length]);
					var offset = ((index + shift) % Alphabet.Count + Alphabet.Count) % Alphabet.Count;
					var newChar = Alphabet[offset];

					if ((KeepCase && char.IsLower(input[i])) || !KeepCase)
						res = char.ToLower(newChar);
					else
						res = char.ToUpper(newChar);

					builder.Append(res);
				}
				else if (!Alphabet.CaseInsensitiveContains(input[i]) && KeepUnknownCharacters)
				{
					{
						builder.Append(res);
					}
				}
				OnEncryptionOngoing(i * 100 / input.Length, watch.Elapsed);
			}

			OnEncryptionFinished(100, watch.Elapsed);
			watch.Stop();

			return new CipherResult(input, builder.ToString());
		}

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
					var shift = Alphabet.CaseInsensitiveIndexOf(Key[i % Key.Length]);
					var offset = ((index - shift) % Alphabet.Count + Alphabet.Count) % Alphabet.Count;
					var newChar = Alphabet[offset];

					if ((KeepCase && char.IsLower(input[i])) || !KeepCase)
						res = char.ToLower(newChar);
					else
						res = char.ToUpper(newChar);

					builder.Append(res);
				}
				else if (!Alphabet.CaseInsensitiveContains(input[i]) && KeepUnknownCharacters)
				{
					{
						builder.Append(res);
					}
				}
				OnEncryptionOngoing(i * 100 / input.Length, watch.Elapsed);
			}

			OnEncryptionFinished(100, watch.Elapsed);
			watch.Stop();

			return new CipherResult(input, builder.ToString());
		}

		private static bool IsAlphabetValid(IList<char> alphabet)
		{
			// Alphabet must contain at least a character
			if (alphabet.Count < 1)
				return false;

			// Alphabet must not contain any duplicated characters, even if the case of characters isn't specific.
			alphabet = alphabet.ToLower();

			return alphabet.Count == alphabet.Distinct().Count();
		}

		// Key cannot be empty
		private bool IsKeyValid(string key) =>
			!string.IsNullOrEmpty(key) && key.All(t => Alphabet.CaseInsensitiveContains(t));
	}
}