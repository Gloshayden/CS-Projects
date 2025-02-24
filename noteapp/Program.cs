/* this app is far from perfect but for now the program works (from what i tested)
please let me know if you find any bugs by creating a github issue*/
namespace noteapp
{
    class Program
    {
        static void Main(string[] args)
        {
            // check if the password and encryption files exist
            if (!File.Exists("key.bin") && !File.Exists("password.bin") && !File.Exists("iv.bin"))
            {
                EncryptionHelper.CreatePassKey();
            }
            else 
            {
                //verifys the user
                byte[] key = File.ReadAllBytes("key.bin");
                byte[] iv = File.ReadAllBytes("iv.bin");
                EncryptionHelper.DecryptPassKey(); 
            }
            do
            {
                //gets the decrypted file names
                byte[] key = File.ReadAllBytes("key.bin");
                byte[] iv = File.ReadAllBytes("iv.bin");
                App.NoteFolder();
                string[] notes = App.GetNotes(key, iv);
                Console.WriteLine();
                while (notes == null) 
                {
                   Console.WriteLine("Do you want to add a note? (y/n)");
                    string nonoteanswer = Console.ReadLine()!;
                    if (nonoteanswer == "y") { App.AddNote(key, iv); } 
                    notes = App.GetNotes(key, iv);
                }
                Console.WriteLine("Notes:");
                for (int i = 0; i < notes.Length; i++)
                {
                    Console.WriteLine(notes[i]);
                }
                //checks if the user wants to add a note or read a note
                Console.WriteLine();
                Console.WriteLine("Do you want to Read or Add a note (Read/Add)");
                string answer = Console.ReadLine()!.ToLower();
                if (answer == "add") { App.AddNote(key, iv); }
                else if (answer == "read")
                {
                    //reads the note that the user wants and returns the decrypted note
                    Console.WriteLine("Enter the name of the note you want to read");
                    string note = Console.ReadLine()!;
                    byte[] encryptedNote = EncryptionHelper.EncryptString(note, key, iv);
                    string notetitle = Convert.ToHexString(encryptedNote);
                    string path = $"notes/{notetitle}";
                    if (!File.Exists(path))
                    {
                        Console.WriteLine("Note not found!");
                        Console.WriteLine();
                        Environment.Exit(0);
                    }
                    string[] encryptedLines = File.ReadAllLines(path);
                    for (int i = 0; i < encryptedLines.Length; i++)
                    {
                        //decrypts and prints the note
                        string encryptedLine = encryptedLines[i];
                        byte[] encryptedBytes = Convert.FromBase64String(encryptedLine);
                        string decryptedLine = EncryptionHelper.DecryptString(encryptedBytes, key, iv);
                        encryptedLines[i] = decryptedLine;
                        Console.WriteLine($"note name: {note}"); Console.WriteLine();
                        Console.WriteLine(encryptedLines[i]); Console.WriteLine();
                    }
                    while (!App.EditNote(path,key,iv)) { break; }
                }
            } while (App.Appup() == true);
        }   
    }
}