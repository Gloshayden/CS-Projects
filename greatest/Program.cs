using System.Security.Cryptography.X509Certificates;

namespace guessnumber
{
    class Program
    {
        static bool appup()
        {
            // function to loop the app
            Console.WriteLine("Do you want to check another set of numbers? (Y/N)");
            string online = Console.ReadLine()!;
            online = online.ToUpper();
            if (online == "Y") {return true;}
            else {return false;}
        }

        static void Main(string[] args)
        {
            do
            {
                int[] list = {0, 0, 0};
                Console.WriteLine("Welcome to the greatest number finder");
                Console.WriteLine("you will need to enter 3 numbers and it will show the higest one");
                Console.WriteLine("Enter the first number: ");
                list[0] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the second number: ");
                list[1] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the third number: ");
                list[2] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"The greatest number is {list.Max()}");
                
            }
            while (appup() == true);
        }
    }
}