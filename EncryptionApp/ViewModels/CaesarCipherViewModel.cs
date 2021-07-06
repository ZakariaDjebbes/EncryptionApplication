using System.Linq;
using System.Windows;
using Encryption;
using Encryption.Caesar;

namespace EncryptionApp.ViewModels
{
	internal class CaesarCipherViewModel : BaseViewModel
	{
		private readonly CaesarCipher caesarEncryption;
		private readonly EncryptionAppViewModel parent;
		private int shift;
		private bool keepCase;
		private string alphabet;

		public int Shift
		{
			get { return shift; }
			set
			{
				shift = value;
				OnPropertyChanged(nameof(Shift));
			}
		}

		public bool KeepCase
		{
			get { return keepCase; }
			set
			{
				keepCase = value;
				OnPropertyChanged(nameof(KeepCase));
			}
		}

		public string Alphabet
		{
			get { return alphabet; }
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

		private bool IsAlphabetValid(char[] alphabet)
		{
			for (int i = 0; i < alphabet.Length; i++)
			{
				alphabet[i] = char.ToLower(alphabet[i]);
			}

			return alphabet.Length == alphabet.Distinct().Count();
		}

		public CipherResult Encrypt(string input)
		{
			caesarEncryption.Alphabet = alphabet.ToCharArray();
			caesarEncryption.Shift = shift;
			caesarEncryption.KeepCase = keepCase;

			return caesarEncryption.Encrypt(input);
		}

		public CaesarCipherViewModel(EncryptionAppViewModel parent)
		{
			this.parent = parent;
			Shift = 1;
			KeepCase = true;
			Alphabet = "abcdefghijklmnopqrstuvwxyz";
			var zebi = Alphabet.ToCharArray();
			caesarEncryption = new(Alphabet.ToCharArray(), Shift, KeepCase);
			caesarEncryption.EncryptionOngoing += EncryptionOngoing;
			caesarEncryption.EncryptionFinished += EncryptionFinished;
		}

		private void EncryptionOngoing(object sender, CipherEventArgs e)
		{
			parent.ElapsedTime = e.ElapsedSeconds;
			parent.Progress = e.Pourcentage;
		}

		private void EncryptionFinished(object sender, CipherEventArgs e)
		{
			parent.ElapsedTime = e.ElapsedSeconds;
			parent.Progress = e.Pourcentage;
		}
	}
}