using System.ComponentModel;

namespace Encryption.Substitution
{
	public class SubstitutionTableEntry : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		private char character;
		private char substitution;

		public char Substitution
		{
			get { return substitution; }
			set
			{
				substitution = value;
				OnPropertyChanged(nameof(Substitution));
			}
		}

		public char Character
		{
			get { return character; }
			set
			{
				character = value;
				OnPropertyChanged(nameof(Character));
			}
		}

		public SubstitutionTableEntry()
		{

		}

		public SubstitutionTableEntry(char character, char substitution)
		{
			Character = character;
			Substitution = substitution;
		}
	}
}