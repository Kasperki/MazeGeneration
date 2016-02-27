using System;

namespace MazeGeneration
{
    /// <summary>
    /// Generates cave like formations
    /// </summary>
    class CelluralAutomata : Map
    {
        //**********************
        //CONTROL PARAMETERS
        //**********************

        /// <summary>
        /// How many times to iterate the celluralData
        /// </summary>
        public int Iterations = 3;

        /// <summary>
        /// 0.0 - 1.0f
        /// </summary>
        public float Density = 0.45f;

        /// <summary>
        /// 
        /// </summary>
        public int WallToFloor = 3;

        /// <summary>
        /// 
        /// </summary>
        public int FloorToWall = 4;

        public CelluralAutomata(int mapSizeX, int mapSizeY, int seed) : base(mapSizeX, mapSizeY, seed)
        {
            mapArray = GenerateCelluralAutomata(Iterations, Density, WallToFloor, FloorToWall);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iterations"></param>
        /// <param name="density"></param>
        /// <param name="wallToFloor">If smaller than neighbours then change to floor</param>
        /// <param name="floorToWall">if bigger than neightbours then change to wall</param>
        /// <returns></returns>
        protected int[,] GenerateCelluralAutomata(int iterations, float density, int wallToFloor, int floorToWall)
        {
            int[,] tempArray;

            FillArray(density);

            tempArray = mapArray;

            for (int n = 0; n < iterations; n++)
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    for (int y = 0; y < mapSizeY; y++)
                    {
                        int walls = CountWalls(x, y);

                        if (mapArray[x, y] == 1 && walls < wallToFloor)
                            tempArray[x, y] = (int)TileType.Floor;

                        if (mapArray[x, y] == 0 && walls > floorToWall)
                            tempArray[x, y] = (int)TileType.Wall;
                    }
                }
                mapArray = tempArray;
            }

            return mapArray;
        }

        /// <summary>
        /// Fills map array with wall
        /// </summary>
        /// <param name="density">0.0 - 1.0</param>
        protected void FillArray(float density)
        {
            if (density < 0 || density > 1)
                throw new IndexOutOfRangeException();

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (pseudoRand.Next(0, 1000) <= density * 1000)
                    {
                        mapArray[x, y] = (int)TileType.Wall;
                    }
                }
            }
        }

        /// <summary>
        /// Counts walls on nerby cells using Von Neiman 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected int CountWalls(int x, int y)
        {
            int wallCount = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    // The middle point is the same as the passed Grid Coordinate, so skip it
                    if (i != 0 || j != 0)
                    {
                        if (i + x > 0 && i + x < mapSizeX && j + y > 0 && j + y < mapSizeY)
                        {
                            if (mapArray[i + x, j + y] == 1)
                                wallCount++;
                        }
                    }
                }
            }

            return wallCount;
        }
    }
}
