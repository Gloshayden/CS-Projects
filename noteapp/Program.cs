namespace noteapp
{
    class Program
    {
        static bool appup()
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
        static void NoteFolder()
        {
            if (!Directory.Exists("notes"))
            {
                System.IO.Directory.CreateDirectory("notes");
                Console.WriteLine("Folder Created");
            }
        }
        static string[] GetNotes(byte[] key, byte[] iv)
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
        static void AddNote(byte[] key, byte[] iv)
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
        static bool EditNote(string path, byte[] key, byte[] iv)
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
        static void Main(string[] args)
        {
            if (!File.Exists("key.bin") && !File.Exists("password.bin") && !File.Exists("iv.bin"))
            {
                EncryptionHelper.CreatePassKey();
            }
            else 
            {
                byte[] key = File.ReadAllBytes("key.bin");
                byte[] iv = File.ReadAllBytes("iv.bin");
                EncryptionHelper.DecryptPassKey(); 
            }
            do
            {
                byte[] key = File.ReadAllBytes("key.bin");
                byte[] iv = File.ReadAllBytes("iv.bin");
                NoteFolder();
                string[] notes = GetNotes(key, iv);
                while (notes == null) 
                {
                   Console.WriteLine("Do you want to add a note? (y/n)");
                    string nonoteanswer = Console.ReadLine()!;
                    if (nonoteanswer == "y") { AddNote(key, iv); } 
                    notes = GetNotes(key, iv);
                }
                for (int i = 0; i < notes.Length; i++)
                {
                    Console.WriteLine(notes[i]);
                }
                Console.WriteLine("Do you want to add a note? (y/n)");
                string answer = Console.ReadLine()!;
                if (answer == "y") { AddNote(key, iv); }
                Console.WriteLine("Do you want to read a note? (y/n)");
                answer = Console.ReadLine()!;
                if (answer == "y")
                {
                    Console.WriteLine("Enter the name of the note you want to read");
                    string note = Console.ReadLine()!;
                    byte[] encryptedNote = EncryptionHelper.EncryptString(note, key, iv);
                    note = Convert.ToHexString(encryptedNote);
                    string path = $"notes/{note}";
                    string[] encryptedLines = File.ReadAllLines(path);
                    for (int i = 0; i < encryptedLines.Length; i++)
                    {
                        string encryptedLine = encryptedLines[i];
                        byte[] encryptedBytes = Convert.FromBase64String(encryptedLine);
                        string decryptedLine = EncryptionHelper.DecryptString(encryptedBytes, key, iv);
                        encryptedLines[i] = decryptedLine;
                        Console.WriteLine(encryptedLines[i]);
                        Console.WriteLine();
                    }
                    while (!EditNote(path,key,iv)) { break; }
                }
            } while (appup() == true);
        }   
    }
}