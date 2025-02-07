using System.Formats.Asn1;
using System.IO;

namespace noteapp
{
    class Program
    {
        static bool appup()
        {
            Console.WriteLine("Do you want to continue? (Y/N)");
            string online = Console.ReadLine();
            if (online == "Y")
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
            else
            {
                Console.WriteLine("Folder Already Exists");
            }
        }
        static string[] GetNotes()
        {
            if (!Directory.EnumerateFileSystemEntries("notes").Any())
            {
                Console.WriteLine("No notes found");
                return null;
            }
            else
            {
                string[] notes = Directory.GetFiles("notes");
                return notes;
            }
        }
        static void AddNote()
        {
            Console.WriteLine("Enter the name of the note");
            string note = Console.ReadLine();
            string path = $"notes/{note}.txt";
            using (File.Create(path)) { }
            Console.WriteLine("Do you want to add a description to the note? (y/n)");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                Console.WriteLine("Enter the description");
                string description = Console.ReadLine();
                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(description);
                }
            }
        }
        static string EditNote(string path)
        {
            Console.WriteLine("Enter the one of the following commands");
            Console.WriteLine("Edit Description, Delete Note, Edit title, Back");
            string command = Console.ReadLine();
            if (command == "Edit Description")
            {
                Console.WriteLine("Enter the new description");
                string description = Console.ReadLine();
                File.WriteAllText(path, description);
                return "new description added";
            }
            else if (command == "Delete Note")
            {
                File.Delete(path);
                return "note deleted";
            }
            else if (command == "Edit title")
            {
                Console.WriteLine("Enter the new title");
                string title = Console.ReadLine();
                string newpath = $"notes/{title}.txt";
                File.Move(path, newpath);
                path = newpath;
                return "back";
            }
            else if (command == "Back")
            {
                return "back";
            }
            else{ return "invalid command"; }
        }
        static void Main(string[] args)
        {
            do
            {
                NoteFolder();
                string[] notes = GetNotes();
                for (int i = 0; i < notes.Length; i++)
                {
                    Console.WriteLine(notes[i].Remove(0,6));
                }
                Console.WriteLine("Do you want to add a note? (y/n)");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    AddNote();
                }
                Console.WriteLine("Do you want to read a note? (y/n)");
                answer = Console.ReadLine();
                if (answer == "y")
                {
                    Console.WriteLine("Enter the name of the note you want to read");
                    string note = Console.ReadLine();
                    string path = $"notes/{note}.txt";
                    string[] lines = File.ReadAllLines(path);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        Console.WriteLine(lines[i]);
                    }
                    while (EditNote(path) != "back")
                    {
                        Console.WriteLine(EditNote(path));
                    }
                }
            } while (appup() == true);
        }   
    }
}