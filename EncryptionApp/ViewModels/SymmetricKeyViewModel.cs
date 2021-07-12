using System;
using System.Windows;
using Encryption;
using EncryptionApp.Commands;
using EncryptionApp.ViewModels.SymmetricKeyViewModels;

namespace EncryptionApp.ViewModels
{
	internal class SymmetricKeyViewModel : BaseCipherViewModel
	{
		internal enum Algorithm
		{
			Vigenere,
			Trilitere,
			AES
		}

		private BaseCipherViewModel selectedAlgorithm;

		public DelegateCommand UpdateAlgorithmCommand => new(UpdateAlgorithm);

		public BaseCipherViewModel SelectedAlgorithm
		{
			get => selectedAlgorithm;
			set
			{
				selectedAlgorithm = value;
				OnPropertyChanged(nameof(SelectedAlgorithm));
			}
		}

		public SymmetricKeyViewModel(EncryptionAppViewModel parent) : base(parent)
		{
			SelectedAlgorithm = new VigenereViewModel(parent);
		}

		public override CipherResult Decrypt(string input)
			=> SelectedAlgorithm.Decrypt(input);

		public override CipherResult Encrypt(string input)
			=> SelectedAlgorithm.Encrypt(input);

		private void UpdateAlgorithm(object obj)
		{
			if (Enum.TryParse(obj.ToString(), true, out Algorithm algorithm))
			{
				switch (algorithm)
				{
					case Algorithm.Vigenere:
						SelectedAlgorithm = new VigenereViewModel(parent);
						break;

					case Algorithm.Trilitere:
						SelectedAlgorithm = new TrilitereViewModel(parent);
						break;

					default:
						MessageBox.Show(
							$"The algorithm {algorithm} is not yet implemented.",
							"Unimplemented Algorithm",
							MessageBoxButton.OK,
							MessageBoxImage.Information);
						break;
				}
			}
		}
	}
}