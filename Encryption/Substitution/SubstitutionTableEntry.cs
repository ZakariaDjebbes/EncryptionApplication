using System.ComponentModel;

namespace Encryption.Substitution
{
	/// <summary>
	/// Represents an entry for the <see cref="SubstitutionCipher"/>.
	/// </summary>
	public class SubstitutionTableEntry : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		private char character;
		private char substitution;

		/// <summary>
		/// The <see cref="char"/> to substitute with.
		/// </summary>
		public char Substitution
		{
			get => substitution;
			set
			{
				substitution = value;
				OnPropertyChanged(nameof(Substitution));
			}
		}

		/// <summary>
		/// The <see cref="char"/> to substitute.
		/// </summary>
		public char Character
		{
			get => character;
			set
			{
				character = value;
				OnPropertyChanged(nameof(Character));
			}
		}

		/// <summary>
		/// Creates a default instance of <see cref="SubstitutionTableEntry"/> with default <see cref="char"/> values.
		/// </summary>
		public SubstitutionTableEntry()
		{
		}

		/// <summary>
		/// Creates an instance of a <see cref="SubstitutionTableEntry"/>.
		/// </summary>
		/// <param name="character">The <see cref="char"/> to substitute</param>
		/// <param name="substitution"><see cref="char"/> to replace with</param>
		public SubstitutionTableEntry(char character, char substitution)
		{
			Character = character;
			Substitution = substitution;
		}
	}
}