using System;

namespace Test
{
    class Program
    {
        // gets a choice from the user for what type of equation they want
        static string GetString()
        {
            Console.WriteLine("Enter enter a choice of a type below (must include caps):");
            Console.WriteLine("Add, Minus,Divide, Multiply, Sqrt");
            string choice = Console.ReadLine();
            return choice;
        }
        // gets two numbers from the user for use in later equations
        static int[] GetNums()
        {
            Console.WriteLine("Enter two numbers for the sum. Numbers must be whole.");
            int inp1 = Convert.ToInt32(Console.ReadLine());
            int inp2 = Convert.ToInt32(Console.ReadLine());
            return new int[] { inp1, inp2 };
        }
        // ask user if they want to continue if not return false and kills app
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
        static void Main(string[] args)
        {
            do
            {
                string choice = GetString();
                if (choice == "Add")
                {
                    int[] nums = GetNums();
                    int sum = nums[0] + nums[1];
                    Console.WriteLine("The sum is " + sum);
                }
                else if (choice == "Minus")
                {
                    int[] nums = GetNums();
                    int sum = nums[0] - nums[1];
                    Console.WriteLine("The difference is " + sum);
                }
                else if (choice == "Divide")
                {
                    int[] nums = GetNums();
                    int sum = nums[0] / nums[1];
                    Console.WriteLine("The quotient is " + sum);
                }
                else if (choice == "Multiply")
                {
                    int[] nums = GetNums();
                    int sum = nums[0] * nums[1];
                    Console.WriteLine("The product is " + sum);
                }
                else if (choice == "Sqrt")
                {
                    Console.WriteLine("Enter the number for the square root. Number must be whole.");
                    int SqrtInp = Convert.ToInt32(Console.ReadLine());
                    int sum = (int)Math.Sqrt(SqrtInp);
                    Console.WriteLine("The square root is " + sum);
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            while (appup() == true);
        }
    }
}