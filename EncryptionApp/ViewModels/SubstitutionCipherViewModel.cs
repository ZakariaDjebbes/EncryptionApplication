using System.Collections.ObjectModel;
using Encryption;
using Encryption.Substitution;
using EncryptionApp.Commands;

namespace EncryptionApp.ViewModels
{
	internal class SubstitutionCipherViewModel : BaseCipherViewModel
	{
		private readonly SubstitutionCipher substitutionCipher;

		private ObservableCollection<SubstitutionTableEntry> entries;
		private bool caseSpecific;

		public DelegateCommand DeleteRowCommand => new(DeleteRow);

		public bool CaseSpecific
		{
			get => caseSpecific;
			set
			{
				caseSpecific = value;
				OnPropertyChanged(nameof(CaseSpecific));
			}
		}

		public ObservableCollection<SubstitutionTableEntry> Entries
		{
			get => entries;
			set
			{
				entries = value;
				OnPropertyChanged(nameof(Entries));
			}
		}

		public SubstitutionCipherViewModel(EncryptionAppViewModel parent) : base(parent)
		{
			CaseSpecific = false;
			Entries = new ObservableCollection<SubstitutionTableEntry>();

			for (var i = 97; i <= 122; i++)
			{
				Entries.Add(new SubstitutionTableEntry((char)i, (char)(i + 1)));
			}

			substitutionCipher = new SubstitutionCipher(Entries, CaseSpecific);
			substitutionCipher.EncryptionOngoing += EncryptionOngoing;
			substitutionCipher.EncryptionFinished += EncryptionFinished;
		}

		public override CipherResult Encrypt(string input)
		{
			substitutionCipher.SubstitutionTableEntries = Entries;
			substitutionCipher.CaseSpecific = CaseSpecific;

			return substitutionCipher.Encrypt(input);
		}

		public override CipherResult Decrypt(string input)
		{
			substitutionCipher.SubstitutionTableEntries = Entries;
			substitutionCipher.CaseSpecific = CaseSpecific;

			return substitutionCipher.Decrypt(input);
		}

		private void DeleteRow(object parameter)
		{
			if (parameter is SubstitutionTableEntry entry)
				Entries.Remove(entry);
		}
	}
}