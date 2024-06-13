namespace Veterinaria.Utils;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class CryptoHelper
{
	private static readonly string keyString = "E546C8DF278CD5931069B522E695D4F2";
	private static readonly string ivString = "A16E64C06762B5D3";

	public static string EncryptString(string plainText)
	{
		using (Aes aesAlg = Aes.Create())
		{
			aesAlg.Key = Encoding.UTF8.GetBytes(keyString);
			aesAlg.IV = Encoding.UTF8.GetBytes(ivString);

			ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

			using (MemoryStream msEncrypt = new MemoryStream())
			{
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
					{
						swEncrypt.Write(plainText);
					}
				}
				return Convert.ToBase64String(msEncrypt.ToArray());
			}
		}
	}

	public static string DecryptString(string cipherText)
	{
		using (Aes aesAlg = Aes.Create())
		{
			aesAlg.Key = Encoding.UTF8.GetBytes(keyString);
			aesAlg.IV = Encoding.UTF8.GetBytes(ivString);

			ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

			using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
			{
				using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				{
					using (StreamReader srDecrypt = new StreamReader(csDecrypt))
					{
						return srDecrypt.ReadToEnd();
					}
				}
			}
		}
	}
}

