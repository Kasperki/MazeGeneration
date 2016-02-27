using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeGeneration
{
    class DepthFirstSearch
    {
        /// <summary>
        /// Array of generated map
        /// </summary>
        private int[,] array;
        
        //**********************
        //CONTROL PARAMETERS
        //**********************

        /// <summary>
        /// Size of map
        /// </summary>
        private Point mapSize;

        /// <summary>
        /// Start cell of maze generation
        /// </summary>
        private Point startCell;

        /// <summary>
        /// Controls the maze "direction" Default 0.5f
        /// </summary>
        public float horizontalIntencity = 0.5f;

        /// <summary>
        /// Controls the minium lenght of maze -> Complexity of maze
        /// </summary>
        public int corridorMinLength = 3;

        /// <summary>
        /// Controls the minium width of walls -> Complexity of maze
        /// </summary>
        public int WallWidth
        {
            get
            {
                return wallWidth;
            }
            set
            {
                if (value < CorridorWidth)
                    wallWidth = CorridorWidth;
                else
                    wallWidth = value;

                if (value <= 0)
                    wallWidth = 1;
            }
        }

        private int wallWidth = 5;

        /// <summary>
        /// Controls the width of corridor
        /// </summary>
        public int CorridorWidth   
        {
            get
            {
                return corridorWidth;
            }
            set
            {
                if (value <= 0)
                    corridorWidth = 1;
                else
                    corridorWidth = value;
            }
        }

        private int corridorWidth = 2;

        /// <summary>
        /// Controls do we generate rooms on maze
        /// </summary>
        public bool GenerateRooms = true;

        /// <summary>
        /// Controls room generation intencity
        /// </summary>
        public float RoomIntencity = 0.05f;
        
        //Room Size
        private const int MinRoomWidth = 3;
        private const int MaxRoomWidth = 8;
        private const int MinRoomHeigth = 3;
        private const int MaxRoomHeigth = 8;

        //generation ids
        private const int Wallid = 0;
        private const int Corridoorid = 1;
        private const int Roomid = 2;

        /// <summary>
        /// PseudoRandom
        /// </summary>
        private PseudoRandom pseudoRand;

        /// <summary>
        /// Generates map
        /// </summary>
        /// <param name="mapSize">Size of map</param>
        /// <param name="startCell">Start cell</param>
        /// <param name="seed">Seed of map</param>
        /// <returns>Map array</returns>
        public int[,] Generate(Point mapSize, Point startCell, int seed)
        {
            this.mapSize = mapSize;
            this.startCell = startCell;
            
            array = new int[mapSize.X, mapSize.Y];

            pseudoRand = new PseudoRandom(seed);

            SetCell(startCell);

            return array;
        }

        /// <summary>
        /// Sets cell to floor recursive
        /// </summary>
        /// <param name="cell">Coorinates of cell</param>
        void SetCell(Point cell,int xLast = 0,int yLast = 0)
        {
            array[cell.X, cell.Y] = Corridoorid;
            List<Point> neigbours = FillNeigbourListRandomly(cell, xLast, yLast);

            for(int i = 0; i < neigbours.Count; i++)
            {
                //CorridoorMinLenght = 1
                if (corridorMinLength == 1)
                {
                    if (CheckCell(neigbours[i], cell))
                    {
                        array[neigbours[i].X, neigbours[i].Y] = Corridoorid;

                        int x = neigbours[i].X - cell.X;
                        int y = neigbours[i].Y - cell.Y;

                        if (neigbours[i].X + x >= 0 && neigbours[i].X + x < mapSize.X && neigbours[i].Y + y >= 0 && neigbours[i].Y + y < mapSize.Y)
                            SetCell(new Point(neigbours[i].X + x, neigbours[i].Y + y));
                    }
                }
                //CorridoorMinLength > 1
                else
                {
                    if (CheckCell(neigbours[i], cell, corridorMinLength, wallWidth))
                    {
                        int x = neigbours[i].X - cell.X;
                        int y = neigbours[i].Y - cell.Y;

                        x = Math.Max(-1, x);
                        x = Math.Min(1, x);
                        y = Math.Max(-1,y);
                        y = Math.Min(1, y);

                        int xlarge = 0;
                        int ylarge = 0;

                        if (x == 0)
                            xlarge = 1;
                        else
                            ylarge = 1;

                        //Generate rooms
                        if (GenerateRooms && (pseudoRand.Next(0,100) / 100.0f) < RoomIntencity)
                            SetRoom(cell,pseudoRand.Next(MinRoomWidth,MaxRoomWidth),pseudoRand.Next(MinRoomHeigth,MaxRoomHeigth));

                        for (int j = 1; j <= corridorMinLength; j++)
                        {
                            for (int width = 0; width < corridorWidth; width++)
                            {
                                array[cell.X + x * j + xlarge * width, cell.Y + y * j + ylarge * width] = Corridoorid;
                            }
                            
                            if (j == corridorMinLength)
                                SetCell(new Point(cell.X + x * j, cell.Y + y * j),xlarge,ylarge);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates room
        /// </summary>
        /// <param name="startCell">start cell of room</param>
        /// <param name="width">width of room</param>
        /// <param name="heigth">height of room</param>
        private void SetRoom(Point startCell, int width, int heigth)
        {
            for (int x = startCell.X; x < startCell.X + width; x++)
            {
                for (int y = startCell.Y; y < startCell.Y + heigth; y++)
                {
                    if (x >= 0 && x < mapSize.X && y >= 0 && y < mapSize.Y)
                        array[x, y] = Roomid;
                }
            }
        }

        /// <summary>
        ///  Check cells forward awailable
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="origin"></param>
        /// <param name="forward">How many cells forward to check</param>
        /// <returns>Returns true if available, false if not</returns>
        private bool CheckCell(Point cell, Point origin, int forward, int wallWidth)
        {
            int x = cell.X - origin.X;
            int y = cell.Y - origin.Y;

            int xWidth = 0;
            int yHeight = 0;
            int xLarge = 0;
            int yLarge = 0;

            if (x == 0)
            {
                xWidth = wallWidth;
                xLarge = 1;
            }
            else
            {
                yHeight = wallWidth;
                yLarge = 1;
            }

            for (int i = 1; i <= forward; i++)
            {
                if (origin.X + x * i + xWidth < 0 || origin.X + x * i + xWidth >= mapSize.X || origin.Y + y * i + yHeight < 0 || origin.Y + y * i + yHeight >= mapSize.Y)
                    return false;

                for (int width = 0; width < wallWidth; width++)
                {
                    if (array[origin.X + x * i + xLarge * width, origin.Y + y * i + yLarge * width] != 0)
                        return false;
                }
            }

            return true;
        }

       /// <summary>
       /// Check if the cell is available
       /// </summary>
       /// <param name="cell">Cell to check</param>
       /// <param name="origin">Parent cell</param>
       /// <returns>Returns true if available, false if not</returns>
        private bool CheckCell(Point cell, Point origin)
        {
            if (cell.X >= 0 && cell.X < mapSize.X && cell.Y >= 0 && cell.Y < mapSize.Y)
            {
                if (array[cell.X, cell.Y] != 0)
                    return false;

                if (cell.X - 1 >= 0 && cell.X - 1 != origin.X)
                    if (array[cell.X - 1, cell.Y] != 0)
                        return false;

                if (cell.X + 1 < mapSize.X && cell.X + 1 != origin.X)
                    if (array[cell.X + 1, cell.Y] != 0)
                        return false;

                if (cell.Y - 1 >= 0 && cell.Y - 1 != origin.Y)
                    if (array[cell.X, cell.Y - 1] != 0)
                        return false;

                if (cell.Y + 1 < mapSize.Y && cell.Y + 1 != origin.Y)
                    if (array[cell.X, cell.Y + 1] != 0)
                        return false;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Fills list of neighbours randomly
        /// </summary>
        /// <param name="cell">Cells neighbours</param>
        /// <returns></returns>
        private List<Point> FillNeigbourListRandomly(Point cell, int x = 0, int y = 0)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(cell.X - 1, cell.Y));
            
            if(x == 0)
                list.Add(new Point(cell.X + 1, cell.Y));
            else
                list.Add(new Point(cell.X + corridorWidth, cell.Y));

            list.Add(new Point(cell.X, cell.Y - 1));

            if (y == 0)
                list.Add(new Point(cell.X, cell.Y + 1));
            else
                list.Add(new Point(cell.X, cell.Y + corridorWidth));

            List<Point> neighbours = new List<Point>();

            while (!neighbours.Contains(list[0]) || !neighbours.Contains(list[1]) || !neighbours.Contains(list[2]) || !neighbours.Contains(list[3]))
            {
                if ((pseudoRand.Next(0, 100) / 100.0f) < horizontalIntencity)
                {
                    if (pseudoRand.Next(0, 2) == 0 && !neighbours.Contains(list[0]))
                        neighbours.Add(list[0]);
                    else if (!neighbours.Contains(list[1]))
                        neighbours.Add(list[1]);
                }
                else
                {
                    if (pseudoRand.Next(0, 2) == 0 && !neighbours.Contains(list[2]))
                        neighbours.Add(list[2]);
                    else if (!neighbours.Contains(list[3]))
                        neighbours.Add(list[3]);
                }
            }

            return neighbours;
        }
    }
}
