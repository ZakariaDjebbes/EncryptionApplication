using System;
using System.Windows;
using Encryption;
using EncryptionApp.ViewModels.SymmetricKeyViewModels;

namespace EncryptionApp.ViewModels
{
	internal class SymmetricKeyViewModel : BaseCipherViewModel
	{
		internal enum Algorithm
		{
			Vigenere,
			Trilitere,
			Aes
		}

		private BaseCipherViewModel selectedAlgorithm;
		private Algorithm selectedComboBoxAlgorithm;

		public Algorithm SelectedComboBoxAlgorithm
		{
			get => selectedComboBoxAlgorithm;
			set
			{
				selectedComboBoxAlgorithm = value;
				UpdateAlgorithm(value);
				OnPropertyChanged(nameof(SelectedComboBoxAlgorithm));
			}
		}

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
			if (!Enum.TryParse(obj.ToString(), true, out Algorithm algorithm)) return;

			switch (algorithm)
			{
				case Algorithm.Vigenere:
					SelectedAlgorithm = new VigenereViewModel(Parent);
					break;

				case Algorithm.Trilitere:
					SelectedAlgorithm = new TrilitereViewModel(Parent);
					break;

				case Algorithm.Aes:
					SelectedAlgorithm = new AesViewModel(Parent);
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