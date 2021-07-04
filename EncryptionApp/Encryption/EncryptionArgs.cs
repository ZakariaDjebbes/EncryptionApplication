using System;

namespace EncryptionApp.Encryption
{
	public class EncryptionArgs : EventArgs
	{
		public float Pourcentage { get; set; }
		public double ElapsedSeconds { get; set; }
	}
}