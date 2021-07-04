using System;
using EncryptionApp.Encryption;

namespace EncryptionApp.ViewModels
{
	internal class CaesarAlgorithmViewModel : BaseViewModel
	{
		private readonly CaesarEncryption caesarEncryption;
		private readonly EncryptionAppViewModel parent; 

		private int shift;

		public int Shift
		{
			get { return shift; }
			set
			{
				shift = value;
				OnPropertyChanged(nameof(Shift));
			}
		}

		private bool encryptNumbers;

		public bool EncryptNumbers
		{
			get { return encryptNumbers; }
			set
			{
				encryptNumbers = value;
				OnPropertyChanged(nameof(EncryptNumbers));
			}
		}

		public EncryptionResult Encrypt(string input)
		{
			if (EncryptNumbers)
				return caesarEncryption.EncryptTextAndNumbers(input, shift);
			else
				return caesarEncryption.EncryptText(input, Shift);
		}

		public CaesarAlgorithmViewModel(EncryptionAppViewModel parent)
		{
			caesarEncryption = new();
			caesarEncryption.EncryptionOngoing += EncryptionOngoing;
			caesarEncryption.EncryptionFinished += EncryptionFinished;
			this.parent = parent;
			Shift = 1;
		}

		private void EncryptionOngoing(object sender, EncryptionArgs e)
		{
			parent.ElapsedTime = e.ElapsedSeconds;
			parent.Progress = e.Pourcentage;
		}

		private void EncryptionFinished(object sender, EncryptionArgs e)
		{
			parent.ElapsedTime = e.ElapsedSeconds;
			parent.Progress = e.Pourcentage;
		}
	}
}