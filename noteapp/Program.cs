/* this app is far from perfect and will break if you dont put the write note name when asked 
when i have more time i plan on fixing this but for now the program works (from what i tested)
please let me know if you find any bugs by creating a github issue*/
namespace noteapp
{
    class Program
    {
        static void Main(string[] args)
        {
            // check if the password and encryption files exist
            if (!File.Exists("key.bin") & !File.Exists("password.bin") & !File.Exists("iv.bin"))
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
                while (notes == null) 
                {
                   Console.WriteLine("Do you want to add a note? (y/n)");
                    string nonoteanswer = Console.ReadLine()!;
                    if (nonoteanswer == "y") { App.AddNote(key, iv); } 
                    notes = App.GetNotes(key, iv);
                }
                for (int i = 0; i < notes.Length; i++)
                {
                    Console.WriteLine(notes[i]);
                }
                //checks if the user wants to add a note or read a note
                Console.WriteLine("Do you want to Read or Add a note (Read/Add)");
                string answer = Console.ReadLine()!;
                if (answer == "Add") { App.AddNote(key, iv); }
                else if (answer == "Read")
                {
                    //reads the note that the user wants and returns the decrypted note
                    Console.WriteLine("Enter the name of the note you want to read");
                    string note = Console.ReadLine()!;
                    byte[] encryptedNote = EncryptionHelper.EncryptString(note, key, iv);
                    string notetitle = Convert.ToHexString(encryptedNote);
                    string path = $"notes/{notetitle}";
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