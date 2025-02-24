class App
{
    //check if the user wants to continue
    public static bool Appup()
    {
        Console.WriteLine("Do you want to continue? (y/n)");
        string online = Console.ReadLine()!;
        online = online.ToLower();
        if (online == "y")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //creates the notes folder if it doesnt exist
    public static void NoteFolder()
    {
        if (!Directory.Exists("notes"))
        {
            System.IO.Directory.CreateDirectory("notes");
            Console.WriteLine("Folder Created");
        }
    }
    //decrypts the notes and returns the list
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
    //Makes a note and adds it to the notes folder, keeping it encrypted
    public static void AddNote(byte[] key, byte[] iv)
    {
        Console.WriteLine("Enter the name of the note");
        string note = Console.ReadLine()!;
        //Encrypts the note name
        byte[] encryptedNote = EncryptionHelper.EncryptString(note, key, iv);
        note = Convert.ToHexString(encryptedNote);
        Console.WriteLine("Please enter a description to the note");
        string description = Console.ReadLine()!;
        //Encrypts the description
        byte[] encryptedDescriptionBytes = EncryptionHelper.EncryptString(description, key, iv);
        if (encryptedDescriptionBytes != null)
        {
            string encryptedDescription = Convert.ToBase64String(encryptedDescriptionBytes);
            string path = $"notes/{note}";
            try
            {
                //Write the encrypted description to the notes folder
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
    /*Edits the note for the user to decide what to do 
    true means the edit command continues false leave the editing*/
    public static bool EditNote(string path, byte[] key, byte[] iv)
    {
        Console.WriteLine("Enter the one of the following commands");
        Console.WriteLine("Edit Description, Delete Note, Edit title, Back");
        string command = Console.ReadLine()!.ToLower();
        if (command == "edit description")
        {
            Console.WriteLine("Enter the new description");
            string description = Console.ReadLine()!;
            byte[] encryptedDescription = EncryptionHelper.EncryptString(description, key, iv);
            description = Convert.ToBase64String(encryptedDescription);
            File.WriteAllText(path, description);
            return true;
        }
        else if (command == "delete note")
        {
            File.Delete(path);
            return false;
        }
        else if (command == "edit title")
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
        else if (command == "back") { return false; }
        else{ return true; }
    }
}