using System.IO;

namespace noteapp
{
    class Program
    {
        static Boolean NoteFolder()
        {
            if (!Directory.Exists("notes"))
            {
                System.IO.Directory.CreateDirectory("notes");
                Console.WriteLine("Folder Created");
                return true;
            }
            else
            {
                Console.WriteLine("Folder Already Exists");
                return true;
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
        static void Main(string[] args)
        {
            NoteFolder();
            string[] notes = GetNotes();
            for (int i = 0; i < notes.Length; i++)
            {
                Console.WriteLine(notes[i]);
            }
            Console.WriteLine("Hello World!");
        }
    }
}