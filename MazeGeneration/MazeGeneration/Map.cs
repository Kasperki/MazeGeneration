using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGeneration
{
    class Map
    {
        /// <summary>
        /// Map size values
        /// </summary>
        int mapSizeX, mapSizeY;
        
        /// <summary>
        /// Array containing map data
        /// </summary>
        int[,] mapArray;

        /// <summary>
        /// Dictionary of used ASCII marks
        /// </summary>
        string mapMarks = "#.|o:;____";

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="x">Width of map</param>
        /// <param name="y">Height of map</param>
        public Map(int mapSizeX,int mapSizeY)
        {
            this.mapSizeX = mapSizeX; 
            this.mapSizeY = mapSizeY;
            
            DepthFirstSearch dpSearch = new DepthFirstSearch();
            mapArray = dpSearch.Generate(new Point(mapSizeX, mapSizeY), new Point(1,1));
        }

        /// <summary>
        /// Prints map to console output using mapMarks 
        /// </summary>
        public void printMap()
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    Console.Write(mapMarks.Substring(mapArray[x, y],1) + "");
                    
                    if (x == mapSizeX - 1)
                        Console.Write("\n");
                }
            }
            
        }

    }
}
