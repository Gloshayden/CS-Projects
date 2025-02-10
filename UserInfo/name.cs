using System;

namespace UserInfo
{
    class Program
    {
        ststic bool appup()
        {
            Console.WriteLine("Do you want to continue? (y/n)");
            string online = Console.ReadLine()!;
            if (online == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static string GetName()
        {
            static string firstncheck()
            {
                Console.WriteLine("Please enter your first name (Max of 20 characters):");
                string firstn = Console.ReadLine();
                firstn = firstn.ToLower();
                return firstn;
            }
            static string lastncheck()
            {
                Console.WriteLine("Please enter your last name (Max of 20 characters):");
                string lastn = Console.ReadLine();
                lastn = lastn.ToUpper();
                return lastn;
            }
            string firstn = firstncheck();
            string lastn = lastncheck();
            if (firstn.Length > 20 || lastn.Length > 20)
            {
                Console.WriteLine("Name too long, please try again.");
                return "retry";
            }
            else
            {
                return string.Concat(firstn, " ", lastn);
            }
        }
        static int GetAge()
        {
            static int agecheck()
            {
                Console.WriteLine("Please enter your age (must be between 18 and 80):");
                int age = Convert.ToInt32(Console.ReadLine());
                return age;
            }
            int age = agecheck();
            if (age < 18 || age > 80)
            {
                Console.WriteLine("Age out of range, please try again.");
                return 0;
            }
            else
            {
                return age;
            }
        }
        static void Main(string[] args)
        {
            do
            {
                string Uname = GetName();
                while (Uname == "retry")
                {
                    Uname = GetName();
                }
                Console.WriteLine($"Full name: {Uname}");
                int age = GetAge();
                while (age == 0)
                {
                    age = GetAge();
                }
                Console.WriteLine($"Welcome {Uname}, you are {age} years old.");
            } while (appup() == true);
        }
    }
}
