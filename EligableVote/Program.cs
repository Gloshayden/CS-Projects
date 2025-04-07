namespace voting
{
    class Program
    {
        static bool appup()
        {
            // function to loop the app
            Console.WriteLine("Do you want to check another age? (Y/N)");
            string online = Console.ReadLine()!;
            online = online.ToUpper();
            if (online == "Y") {return true;}
            else {return false;}
        }

        static void Main(string[] args)
        {
            do
            {
                // greets the user and asks an age
                Console.WriteLine("This app will tell you if you can vote or not.");
                Console.WriteLine("Enter your age: ");
                int age = Convert.ToInt32(Console.ReadLine());
                if (age >= 18)
                {
                    Console.WriteLine("You are eligible to vote.");
                }
                else
                {
                    Console.WriteLine("You are not eligible to vote.");
                }
            }
            while (appup() == true);
        }
    }
}