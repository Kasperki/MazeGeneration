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
            
            //TODO WELCOME MESSAGE - INFO

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
            

            Map map = new Map(75, 45, 1339);
            map.printMap();

            
            Console.ReadLine();
            Console.ReadLine();
             
        }
    }
}
