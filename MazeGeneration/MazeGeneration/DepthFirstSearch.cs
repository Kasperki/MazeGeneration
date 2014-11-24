using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGeneration
{
    class DepthFirstSearch
    {
        /// <summary>
        /// Array of generated map
        /// </summary>
        int[,] array;
        
        //**********************
        //CONTROL PARAMETERS
        //**********************

        /// <summary>
        /// Size of map
        /// </summary>
        Point mapSize;

        /// <summary>
        /// Start cell of maze generation
        /// </summary>
        Point startCell;

        /// <summary>
        /// Controls the maze "direction" Default 0.5f
        /// </summary>
        public float horizontalIntencity = 0.5f;

        /// <summary>
        /// Controls the minium lenght of maze -> Complexity of maze
        /// </summary>
        public int corridorMinLength = 2;

        /// <summary>
        /// Controls the minium width of walls -> Complexity of maze
        /// </summary>
        public int wallWidth
        {
            get
            {
                return _wallWidth;
            }
            set
            {
                if (value < corridorWidth)
                    _wallWidth = corridorWidth;
                else
                    _wallWidth = value;

                if (value <= 0)
                    _wallWidth = 1;
            }
        }

        private int _wallWidth = 3;

        /// <summary>
        /// Controls the width of corridor
        /// </summary>
        public int corridorWidth   
        {
            get
            {
                return _corridorWidth;
            }
            set
            {
                if (value <= 0)
                    _corridorWidth = 1;
                else
                    _corridorWidth = value;
            }
        }

        private int _corridorWidth = 3;

        /// <summary>
        /// Controls do we generate rooms on maze
        /// </summary>
        public bool generateRooms = true;

        /// <summary>
        /// Controls room generation intencity
        /// </summary>
        public float roomIntencity = 0.05f;
        
        //Control rooms size
        private int minRoomWidth = 3;
        private int maxRoomWidth = 8;
        private int minRoomHeigth = 3;
        private int maxRoomHeigth = 8;

        //generation ids
        //private int wallid = 0;
        private int corridoorid = 1;
        private int roomid = 2;

        /// <summary>
        /// PseudoRandom
        /// </summary>
        PseudoRandom pseudoRand;

        /// <summary>
        /// Generates map
        /// </summary>
        /// <param name="mapSize">Size of map</param>
        /// <param name="startCell">Start cell</param>
        /// <param name="seed">Seed of map</param>
        /// <returns>Map array</returns>
        public int[,] Generate(Point mapSize, Point startCell, int corridorMinLength, int seed)
        {
            this.mapSize = mapSize;
            this.startCell = startCell;
            
            array = new int[mapSize.X, mapSize.Y];

            pseudoRand = new PseudoRandom(seed);

            setCell(startCell);

            return array;
        }

        /// <summary>
        /// Sets cell to floor recursive
        /// </summary>
        /// <param name="cell">Coorinates of cell</param>
        void setCell(Point cell,int xLast = 0,int yLast = 0)
        {
            array[cell.X, cell.Y] = corridoorid;
            List<Point> neigbours = fillNeigbourListRandomly(cell, xLast, yLast);

            for(int i = 0; i < neigbours.Count; i++)
            {
                //CorridoorMinLenght = 1
                if (corridorMinLength == 1)
                {
                    if (checkCell(neigbours[i], cell))
                    {
                        array[neigbours[i].X, neigbours[i].Y] = corridoorid;

                        int x = neigbours[i].X - cell.X;
                        int y = neigbours[i].Y - cell.Y;

                        if (neigbours[i].X + x >= 0 && neigbours[i].X + x < mapSize.X && neigbours[i].Y + y >= 0 && neigbours[i].Y + y < mapSize.Y)
                            setCell(new Point(neigbours[i].X + x, neigbours[i].Y + y));
                    }
                }
                //CorridoorMinLength > 1
                else
                {
                    if (checkCell(neigbours[i], cell, corridorMinLength, _wallWidth))
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
                        if (generateRooms && (pseudoRand.Next(0,100) / 100.0f) < roomIntencity)
                            setRoom(cell,pseudoRand.Next(minRoomWidth,maxRoomWidth),pseudoRand.Next(minRoomHeigth,maxRoomHeigth));

                        for (int j = 1; j <= corridorMinLength; j++)
                        {
                            for (int width = 0; width < _corridorWidth; width++)
                            {
                                array[cell.X + x * j + xlarge * width, cell.Y + y * j + ylarge * width] = corridoorid;
                            }
                            
                            if (j == corridorMinLength)
                                setCell(new Point(cell.X + x * j, cell.Y + y * j),xlarge,ylarge);
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
        void setRoom(Point startCell, int width, int heigth)
        {
            for (int x = startCell.X; x < startCell.X + width; x++)
            {
                for (int y = startCell.Y; y < startCell.Y + heigth; y++)
                {
                    if (x >= 0 && x < mapSize.X && y >= 0 && y < mapSize.Y)
                        array[x, y] = roomid;
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
        bool checkCell(Point cell, Point origin, int forward, int wallWidth)
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
        bool checkCell(Point cell, Point origin)
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
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Fills list of neighbours randomly
        /// </summary>
        /// <param name="cell">Cells neighbours</param>
        /// <returns></returns>
        List<Point> fillNeigbourListRandomly(Point cell,int x = 0,int y = 0)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(cell.X - 1, cell.Y));
            
            if(x == 0)
                list.Add(new Point(cell.X + 1, cell.Y));
            else
                list.Add(new Point(cell.X + _corridorWidth, cell.Y));

            list.Add(new Point(cell.X, cell.Y - 1));

            if (y == 0)
                list.Add(new Point(cell.X, cell.Y + 1));
            else
                list.Add(new Point(cell.X, cell.Y + _corridorWidth));

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
