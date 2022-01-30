using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Encryption;
using Encryption.SymmetricKey;

namespace EncryptionApp.ViewModels.SymmetricKeyViewModels
{
	internal class VigenereViewModel : BaseCipherViewModel
	{
		private readonly VigenereCipher vigenereCipher;

		private bool keepCase;
		private bool maintainUnknownCharacters;
		private string alphabet;
		private string key;

		public bool KeepCase
		{
			get => keepCase;
			set
			{
				keepCase = value;
				OnPropertyChanged(nameof(KeepCase));
			}
		}

		public bool MaintainUnknownCharacters
		{
			get => maintainUnknownCharacters;
			set
			{
				maintainUnknownCharacters = value;
				OnPropertyChanged(nameof(MaintainUnknownCharacters));
			}
		}

		public string Alphabet
		{
			get => alphabet;
			set
			{
				if (!IsAlphabetValid(value.ToCharArray()))
				{
					MessageBox.Show(
						"Cannot have duplicated characters in the alphabet. If you wish to include upper case characters, use the below checkbox.",
						"Duplicate Characters",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					return;
				}

				alphabet = value;
				OnPropertyChanged(nameof(Alphabet));
			}
		}

		public string Key
		{
			get => key;
			set
			{
				if (!IsKeyValid(value))
				{
					MessageBox.Show(
						"The key must contain atleast 1 character",
						"Incorrect Key",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					return;
				}

				key = value;
				OnPropertyChanged(nameof(Key));
			}
		}

		public VigenereViewModel(EncryptionAppViewModel parent) : base(parent)
		{
			Key = "defaultkey";
			Alphabet = "abcdefghijklmnopqrstuvwxyz";
			KeepCase = true;
			MaintainUnknownCharacters = true;
			vigenereCipher = new VigenereCipher(Alphabet.ToCharArray(), Key, KeepCase, MaintainUnknownCharacters);
			vigenereCipher.EncryptionOngoing += EncryptionOngoing;
			vigenereCipher.EncryptionFinished += EncryptionFinished;
		}

		public override CipherResult Encrypt(string input)
		{
			vigenereCipher.Alphabet = Alphabet.ToCharArray();
			vigenereCipher.KeepCase = KeepCase;
			vigenereCipher.KeepUnknownCharacters = MaintainUnknownCharacters;
			vigenereCipher.Key = Key;

			return vigenereCipher.Encrypt(input);
		}

		public override CipherResult Decrypt(string input)
		{
			vigenereCipher.Alphabet = Alphabet.ToCharArray();
			vigenereCipher.KeepCase = KeepCase;
			vigenereCipher.KeepUnknownCharacters = MaintainUnknownCharacters;
			vigenereCipher.Key = Key;

			return vigenereCipher.Decrypt(input);
		}

		private static bool IsAlphabetValid(IList<char> alphabet)
		{
			if (alphabet.Count < 1)
				return false;

			for (var i = 0; i < alphabet.Count; i++)
			{
				alphabet[i] = char.ToLower(alphabet[i]);
			}

			return alphabet.Count == alphabet.Distinct().Count();
		}

		public static bool IsKeyValid(string key)
			=> !string.IsNullOrEmpty(key);
	}
}