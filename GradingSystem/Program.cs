namespace grading
{
    class Program
    {
        static bool appup()
        {
            // function to loop the app
            Console.WriteLine("Do you want to grade another mark? (Y/N)");
            string online = Console.ReadLine()!;
            online = online.ToUpper();
            if (online == "Y") {return true;}
            else {return false;}
        }

        static void Main(string[] args)
        {
            do
            {
                // greets the user and asks for a mark
                Console.WriteLine("This app will tell you what grade you got for a mark.");
                Console.WriteLine("Enter a mark: ");
                int mark = Convert.ToInt32(Console.ReadLine());
                if (mark >= 90) { Console.WriteLine("You got an A."); }
                else if (mark >= 80) { Console.WriteLine("You got a B."); }
                else if (mark >= 70) { Console.WriteLine("You got a C."); }
                else if (mark >= 60) { Console.WriteLine("You got a D."); }
                else if (mark < 60) { Console.WriteLine("You got an F."); }
                else { Console.WriteLine("Invalid input."); }
            }
            while (appup() == true);
        }
    }
}