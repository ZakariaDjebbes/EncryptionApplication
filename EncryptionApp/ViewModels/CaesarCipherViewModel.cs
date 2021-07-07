using System.Linq;
using System.Windows;
using Encryption;
using Encryption.Caesar;

namespace EncryptionApp.ViewModels
{
	internal class CaesarCipherViewModel : BaseCipherViewModel
	{
		private readonly CaesarCipher caesarCipher;
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

		public override CipherResult Encrypt(string input)
		{
			caesarCipher.Alphabet = alphabet.ToCharArray();
			caesarCipher.Shift = shift;
			caesarCipher.KeepCase = keepCase;

			return caesarCipher.Encrypt(input);
		}

		public override CipherResult Decrypt(string input)
		{
			caesarCipher.Alphabet = alphabet.ToCharArray();
			caesarCipher.Shift = shift;
			caesarCipher.KeepCase = keepCase;

			return caesarCipher.Decrypt(input);
		}

		public CaesarCipherViewModel(EncryptionAppViewModel parent)
		{
			this.parent = parent;
			Shift = 1;
			KeepCase = true;
			Alphabet = "abcdefghijklmnopqrstuvwxyz";
			caesarCipher = new(Alphabet.ToCharArray(), Shift, KeepCase);
			caesarCipher.EncryptionOngoing += EncryptionOngoing;
			caesarCipher.EncryptionFinished += EncryptionFinished;
		}

		private void EncryptionOngoing(object sender, CipherEventArgs e)
		{
			parent.ElapsedTime = e.EncryptionTime.TotalSeconds;
			parent.Progress = e.Pourcentage;
		}

		private void EncryptionFinished(object sender, CipherEventArgs e)
		{
			parent.ElapsedTime = e.EncryptionTime.TotalSeconds;
			parent.Progress = e.Pourcentage;
		}
	}
}