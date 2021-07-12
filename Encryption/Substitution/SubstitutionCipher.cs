using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Encryption.Extentions;

namespace Encryption.Substitution
{
	/// <summary>
	/// Provides an implementation of by character substitution cipher.
	/// </summary>
	public class SubstitutionCipher : BaseCipher
	{
		/// <summary>
		/// The <see cref="ICollection{T}"/> that is used by <see cref="Encrypt(string)"/> to substitute characters.
		/// <para>
		/// Default: Characters: a..z, Substitutions: b..{
		/// </para>
		/// </summary>
		/// TODO : Ensure duplication isnt allowed
		public ICollection<SubstitutionTableEntry> SubstitutionTableEntries { get; set; }

		/// <summary>
		/// Should the <see cref="Encrypt(string)"/> be case specific when substituting a <see cref="char"/>.
		/// <para>
		/// Default: <see langword="false"/>.
		/// </para>
		/// </summary>
		public bool CaseSpecific { get; set; }

		/// <summary>
		/// Constructs a <see cref="SubstitutionCipher"/> with user defined <see cref="SubstitutionTableEntries"/>
		/// and <see cref="CaseSpecific"/>.
		/// </summary>
		/// <param name="substitutionTableEntries">The <see cref="ICollection{T}"/> that is used by <see cref="Encrypt(string)"/> to substitute characters.</param>
		/// <param name="caseSpecific">Should the <see cref="Encrypt(string)"/> be case specific when substituting a <see cref="char"/>.</param>
		public SubstitutionCipher(ICollection<SubstitutionTableEntry> substitutionTableEntries, bool caseSpecific)
		{
			SubstitutionTableEntries = substitutionTableEntries;
			CaseSpecific = caseSpecific;
		}

		/// <summary>
		/// Constructs a <see cref="SubstitutionCipher"/> with default values for
		/// <see cref="SubstitutionTableEntries"/> and <see cref="CaseSpecific"/>.
		/// </summary>
		public SubstitutionCipher()
		{
			SubstitutionTableEntries = new List<SubstitutionTableEntry>();

			for (int i = 97; i < 122; i++)
			{
				SubstitutionTableEntries.Add(new SubstitutionTableEntry((char)i, (char)(i + 1)));
			}
		}

		/// <summary>
		///	<para>
		///		Encrypts an input <see cref="string"/> using the <see cref="SubstitutionTableEntries"/> to
		///		substitute characters with their substitutions. Characters not included in the <see cref="SubstitutionTableEntries"/>
		///		will be kept as is. If case specific is set to true, characters in the <see cref="SubstitutionTableEntries"/>
		///		that do not match the case will be kept as is.
		///	</para>
		/// </summary>
		/// <param name="input">The <see cref="string"/> to encrypt</param>
		/// <returns><see cref="CipherResult"/></returns>
		public override CipherResult Encrypt(string input)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			StringBuilder builder = new();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (int i = 0; i < input.Length; i++)
			{
				var res = input[i];
				SubstitutionTableEntry entry = null;

				if (CaseSpecific)
					entry = SubstitutionTableEntries.Where(x => x.Character.Equals(res)).FirstOrDefault();
				else
				{
					var index = SubstitutionTableEntries.Select(x => x.Character).ToList().CaseInsensitiveIndexOf(res);
					if (index != -1)
						entry = SubstitutionTableEntries.ToList()[index];
				}

				if (entry != null)
				{
					res = char.IsLower(input[i]) ?
						  char.ToLower(entry.Substitution) :
						  char.ToUpper(entry.Substitution);
				}

				builder.Append(res);
				OnEncryptionOngoing(i * 100 / input.Length, watch.Elapsed);
			}

			OnEncryptionFinished(100, watch.Elapsed);
			watch.Stop();

			return new CipherResult(
				input,
				builder.ToString());
		}

		/// <summary>
		/// <para>
		///		Decrypts an input <see cref="string"/> using the <see cref="SubstitutionTableEntries"/> to
		///		substitute substitutions with their original characters. Substitutions not included in the <see cref="SubstitutionTableEntries"/>
		///		will be kept as is. If case specific is set to true, substitutions in the <see cref="SubstitutionTableEntries"/>
		///		that do not match the case will be kept as is.
		///	</para>
		/// </summary>
		/// <param name="input">The <see cref="string"/> to decrypt</param>
		/// <returns><see cref="CipherResult"/></returns>
		public override CipherResult Decrypt(string input)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			StringBuilder builder = new();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (int i = 0; i < input.Length; i++)
			{
				var res = input[i];
				SubstitutionTableEntry entry = null;

				if (CaseSpecific)
					entry = SubstitutionTableEntries.Where(x => x.Substitution.Equals(res)).FirstOrDefault();
				else
				{
					var index = SubstitutionTableEntries.Select(x => x.Substitution).ToList().CaseInsensitiveIndexOf(res);
					if (index != -1)
						entry = SubstitutionTableEntries.ToList()[index];
				}

				if (entry != null)
				{
					res = char.IsLower(input[i]) ?
						  char.ToLower(entry.Character) :
						  char.ToUpper(entry.Character);
				}

				builder.Append(res);
				OnEncryptionOngoing(i * 100 / input.Length, watch.Elapsed);
			}

			OnEncryptionFinished(100, watch.Elapsed);
			watch.Stop();

			return new CipherResult(
				input,
				builder.ToString());
		}
	}
}