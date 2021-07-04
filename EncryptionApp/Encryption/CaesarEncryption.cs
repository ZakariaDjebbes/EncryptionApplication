using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace EncryptionApp.Encryption
{
	/// <summary>
	/// Caesar encryption class
	/// </summary>
	public class CaesarEncryption : BaseEncryption
	{
		private const int LOWER_ALPHABET_MIN_CODE = 97;
		private const int UPPER_ALPHABET_MIN_CODE = 65;

		public CaesarEncryption()
		{
		}

		/// <summary>
		/// <para>
		///		Encrypts an input string using the caesar cipher for Alphabet only.
		/// </para>
		/// <para>
		///		To include number encryption use : 
		///		<see cref="EncryptTextAndNumbers(string, int)"/>
		/// </para>
		/// </summary>
		/// <param name="inputText">The text to encrypt</param>
		/// <param name="shift">Caesar shift</param>
		/// <returns>
		///		<see cref="EncryptionResult"/>
		/// </returns>
		public EncryptionResult EncryptText(string inputText, int shift = 1)
		{
			if (inputText is null)
			{
				throw new ArgumentNullException(nameof(inputText));
			}

			StringBuilder builder = new StringBuilder();
			Stopwatch watch = new();
			watch.Start();
			OnEncryptionStarted();

			for (int i = 0; i < inputText.Length; i++)
			{
				char res = inputText[i];

				if (char.IsUpper(inputText[i]))
				{
					res = (char)((inputText[i] + shift - UPPER_ALPHABET_MIN_CODE) % 26 + UPPER_ALPHABET_MIN_CODE);
				}
				else if(char.IsLower(inputText[i]))
				{
					res = (char)((inputText[i] + shift - LOWER_ALPHABET_MIN_CODE) % 26 + LOWER_ALPHABET_MIN_CODE);
				}

				OnEncryptionOngoing((i * 100) / inputText.Length, watch.Elapsed.TotalSeconds);
				builder.Append(res);
			}

			OnEncryptionFinished(100, watch.Elapsed.TotalSeconds);
			watch.Stop();
			EncryptionResult = new EncryptionResult(
				inputText,
				builder.ToString(),
				EncryptionResultStatus.Done);

			return EncryptionResult;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inputText"></param>
		/// <param name="shift"></param>
		/// <returns></returns>
		public EncryptionResult EncryptTextAndNumbers(string inputText, int shift)
		{
			return null;
		}
	}
}