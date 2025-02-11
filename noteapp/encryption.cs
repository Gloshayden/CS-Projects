using System;
using System.IO;
using System.Security.Cryptography;

public static class EncryptionHelper
{
    //Generate a new AES key and IV
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
    //Method to encrypt the notes name and contents
    public static byte[] EncryptString(string plaintext, byte[] key, byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            //Uses the key and iv to encrypt the provided text
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
    //Mostly the same as the encryption method but uses the key and iv to decrypt
    public static string DecryptString(byte[] ciphertext, byte[] key, byte[] iv)
    {
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
    /*Generates a key and iv and stores them in files and asked the user to make a password
    deleting the password file or the if/key files will render the app unusable if all files are deleted
    new key and iv will be generated rendering the notes secure*/
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
    //Verifys the password using the key and iv and the saved password
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