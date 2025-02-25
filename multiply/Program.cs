namespace guessnumber
{
    class Program
    {
        static bool appup()
        {
            Console.WriteLine("Do you want to multiply another number? (Y/N)");
            string online = Console.ReadLine()!;
            online = online.ToUpper();
            if (online == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Welcome this app will give you the first 12 multiples of a number.");
                Console.WriteLine("Enter a number: ");
                int num = Convert.ToInt32(Console.ReadLine());
                for (int i = 1; i <= 12; i++)
                {
                    Console.WriteLine($"{i}: {num * i}");
                }
            }
            while (appup() == true);
        }
    }
}