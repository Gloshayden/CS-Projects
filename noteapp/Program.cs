/* this app is far from perfect and will break if you dont put the write note name when asked 
when i have more time i plan on fixing this but for now the program works (from what i tested)
please let me know if you find any bugs by creating a github issue*/
namespace noteapp
{
    class Program
    {
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
                Console.WriteLine("Do you want to add a note? (y/n)");
                string answer = Console.ReadLine()!;
                if (answer == "y") { App.AddNote(key, iv); }
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
                    while (!App.EditNote(path,key,iv)) { break; }
                }
            } while (App.Appup() == true);
        }   
    }
}