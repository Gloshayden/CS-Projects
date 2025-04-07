namespace divby5
{
    class Program
    {
        static bool appup()
        {
            // function to loop the app
            Console.WriteLine("Do you want to check another number? (Y/N)");
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
                Console.WriteLine("Welcome this app will check if a number is divisible by 5.");
                Console.WriteLine("Enter a number: ");
                int num;
                try {num = Convert.ToInt32(Console.ReadLine());} 
                catch {
                    Console.WriteLine("Please enter a number.");
                    continue;
                }
                if (num % 5 == 0)
                {
                    Console.WriteLine($"{num} is divisible by 5.");
                }
                else
                {
                    Console.WriteLine($"{num} is not divisible by 5.");
                }
            }
            while (appup() == true);
        }
    }
}