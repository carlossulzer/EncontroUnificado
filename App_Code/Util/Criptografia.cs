using System;

namespace Util
{
	/// <summary>
	/// Summary description for Criptografia.
	/// </summary>
	public class Criptografia
	{
		public Criptografia()
		{
		}

		public static string encriptar(string variavel)
		{
			byte[] byteClearBytes = new System.Text.UnicodeEncoding().GetBytes(variavel); // Encodes a specified range of characters from a Unicode character array or a String and stores the results into a specified byte array.
			byte[] byteHashedBytes = ((System.Security.Cryptography.HashAlgorithm) System.Security.Cryptography.CryptoConfig.CreateFromName("MD5")).ComputeHash(byteClearBytes); // Overloaded. Creates a new instance of the specified cryptographic object.
			return BitConverter.ToString(byteHashedBytes);
		}
	}
}


