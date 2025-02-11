class App
{
    public static bool Appup()
    {
        Console.WriteLine("Do you want to continue? (y/n)");
        string online = Console.ReadLine()!;
        if (online == "y")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void NoteFolder()
    {
        if (!Directory.Exists("notes"))
        {
            System.IO.Directory.CreateDirectory("notes");
            Console.WriteLine("Folder Created");
        }
    }
    public static string[] GetNotes(byte[] key, byte[] iv)
    {
        if (!Directory.EnumerateFileSystemEntries("notes").Any())
        {
            Console.WriteLine("No notes found");
            return null!;
        }
        else
        {
            string[] encryptedNotes = Directory.GetFiles("notes");
            string[] decryptedNotes = new string[encryptedNotes.Length];
            for (int i = 0; i < encryptedNotes.Length; i++)
            {
                string encryptedNote = Path.GetFileNameWithoutExtension(encryptedNotes[i]);
                byte[] encryptedBytes = Convert.FromHexString(encryptedNote);
                string decryptedNote = EncryptionHelper.DecryptString(encryptedBytes, key, iv);
                decryptedNotes[i] = decryptedNote;
            }
            return decryptedNotes;
        }
    }
    public static void AddNote(byte[] key, byte[] iv)
    {
        Console.WriteLine("Enter the name of the note");
        string note = Console.ReadLine()!;
        byte[] encryptedNote = EncryptionHelper.EncryptString(note, key, iv);
        note = Convert.ToHexString(encryptedNote);
        Console.WriteLine("Please enter a description to the note");
        string description = Console.ReadLine()!;
        byte[] encryptedDescriptionBytes = EncryptionHelper.EncryptString(description, key, iv);
        if (encryptedDescriptionBytes != null)
        {
            string encryptedDescription = Convert.ToBase64String(encryptedDescriptionBytes);
            string path = $"notes/{note}";
            Console.WriteLine(path);
            try
            {
                File.WriteAllText(path, encryptedDescription);
                Console.WriteLine("Note created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Error encrypting description");
        }
    
    }
    public static bool EditNote(string path, byte[] key, byte[] iv)
    {
        Console.WriteLine("Enter the one of the following commands");
        Console.WriteLine("Edit Description, Delete Note, Edit title, Back");
        string command = Console.ReadLine()!;
        if (command == "Edit Description")
        {
            Console.WriteLine("Enter the new description");
            string description = Console.ReadLine()!;
            byte[] encryptedDescription = EncryptionHelper.EncryptString(description, key, iv);
            description = Convert.ToBase64String(encryptedDescription);
            File.WriteAllText(path, description);
            return true;
        }
        else if (command == "Delete Note")
        {
            File.Delete(path);
            return false;
        }
        else if (command == "Edit title")
        {
            Console.WriteLine("Enter the new title");
            string title = Console.ReadLine()!;
            byte[] encryptedTitle = EncryptionHelper.EncryptString(title, key, iv);
            title = Convert.ToHexString(encryptedTitle);
            string newpath = $"notes/{title}";
            File.Move(path, newpath);
            path = newpath;
            return false;
        }
        else if (command == "Back") { return false; }
        else{ return true; }
    }
}