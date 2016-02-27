using System;

namespace MazeGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
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
                Console.WriteLine("Enter a seed value:"); //NEED TRY AND CATCH(Exception e) not int!
                int seed = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a height of the labyrint:");
                int height = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a width of the labyrint:");
                int width = int.Parse(Console.ReadLine());

                // LABYRINT PRINT
                //Map map = new DepthFirstSearch(width, height, seed);
                //map.printMap();

                for (int i = 0; i < width; i++) { Console.Write("_"); }
                Console.Write("\n");
                
                // CAVE PRINT
                Map map2 = new CelluralAutomata(width, height, seed);
                map2.PrintMap();

                // NEW LABYRINT
                while (true)
                { 
                    Console.WriteLine("Press N to create a new labyrinth or Q to quit");
                    char option2 = (char) Console.Read();
                    if (option2 == 'Q')
                    {
                        option = option2;
                        break;
                    }
                    else if (option2 == 'N')
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("Wrong value, please try again.");
                    } 
               } 
            }            
        }
    }
}
