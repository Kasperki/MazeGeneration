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
        /// "Tileset" in ASCII marks
        /// </summary>
        public string mapMarks = "#..SEoO(.)(.)";

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="x">Width of map</param>
        /// <param name="y">Height of map</param>
        /// <param name="seed">Seed of map</param>
        public Map(int mapSizeX, int mapSizeY, int seed)
        {
            this.mapSizeX = mapSizeX; 
            this.mapSizeY = mapSizeY;

            mapArray = new int[mapSizeX, mapSizeY];

            DepthFirstSearch dpSearch = new DepthFirstSearch();
            int[,] tempArray = dpSearch.Generate(new Point(mapSizeX - 2, mapSizeY - 2), new Point((mapSizeX - 2) / 2, (mapSizeY - 2) / 2), 3 ,seed);
            //Adds Outer walls
            for (int x = 1; x < mapSizeX - 1; x++)
            {
                for (int y = 1; y < mapSizeY - 1; y++)
                {
                    mapArray[x, y] = tempArray[x-1, y-1];
                }
            }
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
                    Console.Write(mapMarks.Substring(mapArray[x, y],1));
                    
                    if (x == mapSizeX - 1)
                        Console.Write("\n");
                }
            }
            
        }

    }
}
