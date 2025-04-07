namespace guessnumber
{
    class Program
    {
        static bool appup()
        {
            // function to loop the app
            Console.WriteLine("Do you want to Check another year? (Y/N)");
            string online = Console.ReadLine()!;
            online = online.ToUpper();
            if (online == "Y") {return true;}
            else {return false;}
        }

        static void Main(string[] args)
        {
            do
            {
                // greets the user and asks for a year
                Console.WriteLine("This app will tell you if a year is a leap year or not.");
                Console.WriteLine("Enter a year: ");
                int year = Convert.ToInt32(Console.ReadLine());
                if (year % 4 == 0)
                {
                    Console.WriteLine($"{year} is a leap year.");
                }
                else
                {
                    Console.WriteLine($"{year} is not a leap year.");
                }
            }
            while (appup() == true);
        }
    }
}