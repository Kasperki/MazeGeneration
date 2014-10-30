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
            //TODO ASK FOR SEED AND MAPSIZE
 
            //PseudoRandom Test
            /*
            int size = 10;
            PseudoRandom rand = new PseudoRandom(666);
            int[] luvut = new int[size];
            for (int i = 0; i < size ; i++)
            {
                luvut[i] = rand.Next(1, 70);
                Console.WriteLine(luvut[i]);
            }
            */

            Map map = new Map(60,30);
            map.printMap();

            
            Console.ReadLine();
            Console.ReadLine();
             
        }
    }
}
