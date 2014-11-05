using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace MazeGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            char option, option2 = 'x';
            bool loop = true;
            int seed = 0, height = 0, width = 0;
            //TODO WELCOME MESSAGE - INFO

            Console.WriteLine("********************************************************************************");
            Console.WriteLine("****                           LABYRINT CREATOR                             ****");
            Console.WriteLine("********************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                          Welcome to Labyrint creator 0.5");
            Console.WriteLine("                              Press E to continue...");
            option = (char)Console.Read();
            Console.ReadLine();
            while (option == 'E')
            {
                Console.Clear();

                // LABYRINT DETAILS
                Console.WriteLine("Enter a seed value:");
                seed = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a height of the labyrint:");
                height = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a width of the labyrint:");
                width = int.Parse(Console.ReadLine());

                // LABYRINT PRINT
                Map map = new Map(width, height, seed);
                map.printMap();


                // NEW LABYRINT
                while(loop)
                { 
                    Console.WriteLine("Press N to create a new labyrinth or Q to quit");
                    option2 =(char) Console.Read();
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
                        loop = true;
                    } 
               }
                loop = true;
            }

            
            //TODO LOOP WHILE EXIT COMMAND
            //TODO ASK FOR SEED, MAPSIZE, MAZE COMPLEXITY AND HORIZONTAL INFLUENCE.
 
            //PseudoRandom Test
            /*
            int size = 1000;
            PseudoRandom rand = new PseudoRandom(666);
            int[] luvut = new int[size];
            for (int i = 0; i < size ; i++)
            {
                luvut[i] = rand.Next(0,100);
                Console.Write(luvut[i]+",");
            }*/
            

<<<<<<< HEAD
=======
            Map map = new Map(75, 45, 1339);
            map.printMap();
>>>>>>> origin/master

             
        }
    }
}
