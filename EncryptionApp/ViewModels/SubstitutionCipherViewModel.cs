using System.Collections.ObjectModel;
using Encryption;
using Encryption.Substitution;
using EncryptionApp.Commands;

namespace EncryptionApp.ViewModels
{
	internal class SubstitutionCipherViewModel : BaseCipherViewModel
	{
		private readonly EncryptionAppViewModel parent;
		private readonly SubstitutionCipher substitutionCipher;

		private ObservableCollection<SubstitutionTableEntry> entries;
		private bool caseSpecific;

		public DelegateCommand DeleteRowCommand => new(DeleteRow);

		public bool CaseSpecific
		{
			get { return caseSpecific; }
			set
			{
				caseSpecific = value;
				OnPropertyChanged(nameof(CaseSpecific));
			}
		}

		public ObservableCollection<SubstitutionTableEntry> Entries
		{
			get { return entries; }
			set
			{
				entries = value;
				OnPropertyChanged(nameof(Entries));
			}
		}

		public SubstitutionCipherViewModel(EncryptionAppViewModel parent)
		{
			this.parent = parent;
			CaseSpecific = false;
			Entries = new ObservableCollection<SubstitutionTableEntry>();

			for (int i = 97; i <= 122; i++)
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
			throw new System.NotImplementedException();
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

		private void DeleteRow(object parameter)
		{
			if (parameter is SubstitutionTableEntry entry)
				Entries.Remove(entry);
		}
	}
}