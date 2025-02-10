using System;
using System.IO;
using System.Security.Cryptography;

public static class EncryptionHelper
{
    public static (byte[] key, byte[] iv) CreateKey()
    {
        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.GenerateKey();
            return (aes.Key, aes.IV);
        }
    }

    public static void StoreKeyInFile(byte[] key, string filename)
    {
        File.WriteAllBytes(filename, key);
    }

    public static byte[] EncryptString(string plaintext, byte[] key, byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
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

    public static string DecryptString(byte[] ciphertext, byte[] key, byte[] iv)
    {
        if (ciphertext.Length % 16 != 0)
        {
            throw new ArgumentException("Ciphertext must be a multiple of 16 bytes");
        }
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(ciphertext))
            {
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
    public static void CreatePassKey() 
    {
        var (key, iv) = CreateKey();
        StoreKeyInFile(key, "key.bin");
        StoreKeyInFile(iv, "iv.bin");
        Console.WriteLine("What is your password");
        string password = Console.ReadLine()!;
        byte[] encrypted = EncryptString(password, key, iv);
        StoreKeyInFile(encrypted, "password.bin"); 
    }
    public static void DecryptPassKey()
    {
        byte[] key = File.ReadAllBytes("key.bin");
        byte[] iv = File.ReadAllBytes("iv.bin");
        byte[] encrypted = File.ReadAllBytes("password.bin");
        string password = DecryptString(encrypted, key, iv);
        Console.WriteLine("What is your password");
        string input = Console.ReadLine()!;
        if (input == password) {Console.WriteLine("Password accepted");}
        else { 
            Console.WriteLine("Password incorrect"); 
            Environment.Exit(0); 
        }
    }
}