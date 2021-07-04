﻿using System;

namespace EncryptionApp.Encryption
{
	public abstract class BaseEncryption
	{
		/// <summary>
		/// Fires whenever an encryption starts.
		/// </summary>
		public event EventHandler<EventArgs> EncryptionStarted;

		/// <summary>
		/// Fires whenever a character in the input is encrypted.
		/// <para>
		///		Contains the encryption arguments.
		/// </para>
		/// see <see cref="EncryptionArgs"/>
		/// </summary>
		public event EventHandler<EncryptionArgs> EncryptionOngoing;

		/// <summary>
		/// Fires whenever an encryption is finished.
		/// <para>
		///		Contains the encryption arguments.
		/// </para>
		/// see <see cref="EncryptionArgs"/>
		/// </summary>
		public event EventHandler<EncryptionArgs> EncryptionFinished;


		/// <summary>
		/// The Encryption result of the last encrypted input.
		/// </summary>
		public EncryptionResult EncryptionResult
		{
			get
			{
				if (encryptionResult == null)
					encryptionResult = new EncryptionResult(
						string.Empty,
						string.Empty,
						EncryptionResultStatus.None);

				return encryptionResult;
			}

			protected set
			{
				encryptionResult = value;
			}
		}

		private EncryptionResult encryptionResult;

		protected void OnEncryptionStarted()
			=> EncryptionStarted?.Invoke(this, new EventArgs());

		protected void OnEncryptionOngoing(float pourcentage, double elapsedTime)
			=> EncryptionOngoing?.Invoke(this, new EncryptionArgs { Pourcentage = pourcentage, ElapsedSeconds = elapsedTime });

		protected void OnEncryptionFinished(float pourcentage, double elapsedTime)
			=> EncryptionFinished?.Invoke(this, new EncryptionArgs { Pourcentage = pourcentage, ElapsedSeconds = elapsedTime });
	}
}