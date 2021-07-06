using System.Collections.ObjectModel;
using System.Linq;
using Encryption.Substitution;
using EncryptionApp.Commands;

namespace EncryptionApp.ViewModels
{
	internal class SubstitutionCipherViewModel : BaseViewModel
	{
		private readonly EncryptionAppViewModel parent;

		private ObservableCollection<SubstitutionTableEntry> entries;
		private bool caseSpecific;

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
		}

		public DelegateCommand DeleteRowCommand => new(DeleteRow);

		private void DeleteRow(object parameter)
		{
			if (parameter is SubstitutionTableEntry entry)
				Entries.Remove(entry);
		}
	}
}