using System.Collections.Generic;

namespace Encryption.Substitution
{

	public class SubstitutionCipher : BaseCipher
	{
		public ICollection<SubstitutionTableEntry> SubstitutionTableEntries { get; set; }

		public SubstitutionCipher(ICollection<SubstitutionTableEntry> substitutionTableEntries)
		{
			SubstitutionTableEntries = substitutionTableEntries;
		}

		public override CipherResult Encrypt(string input)
		{
			throw new System.NotImplementedException();
		}
	}
}