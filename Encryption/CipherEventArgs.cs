using System;

namespace Encryption
{
	public class CipherEventArgs : EventArgs
	{
		public CipherEventArgs(float pourcentage, double elapsedSeconds)
		{
			Pourcentage = pourcentage;
			ElapsedSeconds = elapsedSeconds;
		}

		public float Pourcentage { get; }
		public double ElapsedSeconds { get; }
	}
}