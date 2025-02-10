using System;
using System.IO;
using System.Security.Cryptography;

public static class EncryptionHelper
{
    public static byte[] CreateKey()
    {
        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.GenerateKey();
            return aes.Key;
        }
    }

    public static void StoreKeyInFile(byte[] key, string filename)
    {
        File.WriteAllBytes(filename, key);
    }

    public static byte[] EncryptString(string plaintext, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.GenerateIV();
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plaintext);
                }
                return ms.ToArray();
            }
        }
    }

    public static string DecryptString(byte[] ciphertext, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.GenerateIV();
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var ms = new MemoryStream(ciphertext);
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}