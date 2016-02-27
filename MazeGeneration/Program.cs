using System;

namespace MazeGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;

            Console.WriteLine("********************************************************************************");
            Console.WriteLine("****                           LABYRINT CREATOR                             ****");
            Console.WriteLine("********************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                          Welcome to Labyrint creator 0.5");
            Console.WriteLine("                              Press E to continue...");
            char option = (char)Console.Read();
            Console.ReadLine();
            while (option == 'E')
            {
                Console.Clear();
                // LABYRINT DETAILS
                Console.WriteLine("Enter a seed value:"); //NEED TRY AND CATCH(Exception e) not int!! Tai Flush Console?
                int seed = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a height of the labyrint:");
                int height = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a width of the labyrint:");
                int width = int.Parse(Console.ReadLine());

                // LABYRINT PRINT
                Map map = new Map(width, height, seed);
                map.printMap();

                // NEW LABYRINT
                while(loop)
                { 
                    Console.WriteLine("Press N to create a new labyrinth or Q to quit");
                    char option2 = (char) Console.Read();
                    if (option2 == 'Q')
                    {
                        option = option2;
                        loop = false;
                    }
                    else if (option2 == 'N')
                    {
                        loop = false;
                    }
                    else
                    {
                        Console.Write("Wrong value, please try again.");
                    } 
               } 
               
                loop = true;
            }            
        }
    }
}
