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
        public int corridorMinLenght = 2;

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
        public int[,] Generate(Point mapSize, Point startCell, int corridorMinLenght, int seed)
        {
            this.mapSize = mapSize;
            this.startCell = startCell;
            this.corridorMinLenght = corridorMinLenght;
            
            array = new int[mapSize.X, mapSize.Y];

            pseudoRand = new PseudoRandom(seed);

            setCell(startCell);

            return array;
        }

        /// <summary>
        /// Sets cell to floor
        /// </summary>
        /// <param name="cell">Coorinates of cell</param>
        void setCell(Point cell)
        {
            array[cell.X, cell.Y] = 1;
            List<Point> neigbours = fillNeigbourListRandomly(cell);

            for(int i = 0; i < neigbours.Count; i++)
            {
                //Todo Use this old check cell if CorridorminLengt == 1
                /*if (checkCell(neigbours[i], cell))
                {
                    array[neigbours[i].X,neigbours[i].Y] = 1;

                    int x = neigbours[i].X - cell.X;
                    int y = neigbours[i].Y - cell.Y;

                    if (neigbours[i].X + x >= 0 && neigbours[i].X + x < mapSize.X && neigbours[i].Y + y >= 0 && neigbours[i].Y + y < mapSize.Y)
                        setCell(new Point(neigbours[i].X + x, neigbours[i].Y + y));
                }*/

                if (checkCell(neigbours[i], cell, corridorMinLenght))
                {
                    int x = neigbours[i].X - cell.X;
                    int y = neigbours[i].Y - cell.Y;

                    for (int j = 1; j <= corridorMinLenght; j++)
                    {
                        array[cell.X + x * j, cell.Y + y * j] = 1;

                        if (j == corridorMinLenght)
                            setCell(new Point(cell.X + x * j, cell.Y + y * j));
                    }
                }
            }
        }

        /// <summary>
        ///  Check cells forward awailable
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        bool checkCell(Point cell, Point origin, int forward)
        {
            int x = cell.X - origin.X;
            int y = cell.Y - origin.Y;

            for (int i = 1; i <= forward; i++)
            {
                if (origin.X + x * i < 0 || origin.X + x * i >= mapSize.X || origin.Y + y * i < 0 || origin.Y + y * i >= mapSize.Y)
                    return false;

                if (array[origin.X + x * i, origin.Y + y * i] != 0)
                    return false;
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
        List<Point> fillNeigbourListRandomly(Point cell)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(cell.X - 1, cell.Y));
            list.Add(new Point(cell.X + 1, cell.Y));
            list.Add(new Point(cell.X, cell.Y - 1));
            list.Add(new Point(cell.X, cell.Y + 1));
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
