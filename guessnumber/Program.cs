namespace guessnumber
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
        static int setdiff()
        {
            Console.Write("Enter a difficulty (easy, medium, hard, insane): ");
            string userInput = Console.ReadLine();
            if (userInput == "easy")
            {
                return 1;
            }
            else if (userInput == "medium")
            {
                return 2;
            }
            else if (userInput == "hard")
            {
                return 3;
            }
            else if (userInput == "insane")
            {
                return 4;
            }
            else
            {
                return 0;
            }

        }
        static int randomnum(int diffnum)
        {
            Random random = new Random();
            int min = 1;
            int max = 6*diffnum;
            Console.WriteLine($"The number is between {min} and {max}");
            int Number = random.Next(min, max);
            return Number;
        }
        static void Main(string[] args)
        {
            do
            {
                int diff = setdiff();
                while (diff == 0)
                {
                    Console.WriteLine("Invalid input");
                    diff = setdiff();
                }
                int Number = randomnum(diff);
                Console.WriteLine("try and guess the number");
                int choice = Convert.ToInt32(Console.ReadLine());
                while (choice != Number)
                {
                    Console.WriteLine("try again");
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("You win");
            } 
            while (appup() == true);
        }
    }
}