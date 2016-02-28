using System;

namespace MazeGeneration
{
    class Map
    {
        /// <summary>
        /// MapSizeX
        /// </summary>
        protected int mapSizeX
        {
            get { return mapArray.GetLength(0); }
        }

        /// <summary>
        /// MapSizeY
        /// </summary>
        protected int mapSizeY
        {
            get { return mapArray.GetLength(1); }
        }

        /// <summary>
        /// Array containing map data
        /// </summary>
        protected int[,] mapArray;

        /// <summary>
        /// "Tileset" in ASCII marks
        /// </summary>
        private const string MapString = "#...E";

        /// <summary>
        /// PseudoRandom
        /// </summary>
        protected PseudoRandom pseudoRand;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="x">Width of map</param>
        /// <param name="y">Height of map</param>
        /// <param name="seed">Seed of map</param>
        public Map(int mapSizeX, int mapSizeY, int seed)
        {
            mapArray = new int[mapSizeX, mapSizeY];
            pseudoRand = new PseudoRandom(seed);
        }

        /// <summary>
        /// Prints map to console output using MapString 
        /// </summary>
        public void PrintMap()
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    Console.Write(MapString.Substring(mapArray[x, y], 1));
                    
                    if (x == mapSizeX - 1)
                        Console.Write("\n");
                }
            }

            for (int i = 0; i < mapSizeX; i++) { Console.Write("_"); }
            Console.Write("\n");
        }

    }
}
