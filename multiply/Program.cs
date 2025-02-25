using System.Security.Cryptography.X509Certificates;

namespace guessnumber
{
    class Program
    {
        static bool appup()
        {
            // function to loop the app
            Console.WriteLine("Do you want to multiply another number? (Y/N)");
            string online = Console.ReadLine()!;
            online = online.ToUpper();
            if (online == "Y") {return true;}
            else {return false;}
        }

        static void Main(string[] args)
        {
            do
            {
                // greets the user and asks for a number
                Console.WriteLine("Welcome this app will give you the first 12 multiples of a number.");
                Console.WriteLine("Enter a number: ");
                int num;
                try {num = Convert.ToInt32(Console.ReadLine());} 
                catch {
                    Console.WriteLine("Please enter a number.");
                    continue;
                }
                for (int i = 1; i <= 12; i++)
                { Console.WriteLine($"{i}: {num * i}"); }
                // format, multiple number: the multiple
            }
            while (appup() == true);
        }
    }
}