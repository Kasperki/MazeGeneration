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

            Map map = new Map(30,30);
            map.printMap();

            Console.ReadLine();
            Console.ReadLine();
        }
    }
}
