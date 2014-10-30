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
        /// Todo PseudoRandom
        /// </summary>
        PseudoRandom rand = new PseudoRandom(4578);

        /// <summary>
        /// Generates map
        /// </summary>
        /// <param name="mapSize"></param>
        /// <param name="startCell"></param>
        /// <returns>Map array</returns>
        public int[,] Generate(Point mapSize, Point startCell)
        {
            this.mapSize = mapSize;
            this.startCell = startCell;
            array = new int[mapSize.X, mapSize.Y];

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

            //Todo Change checkCell to look Xcount cells forward.

            for(int i = 0; i < neigbours.Count; i++)
            {
                if (checkCell(neigbours[i], cell) == 1)
                {
                    array[neigbours[i].X,neigbours[i].Y] = 1;

                    int x = neigbours[i].X - cell.X;
                    int y = neigbours[i].Y - cell.Y;

                    if (neigbours[i].X + x >= 0 && neigbours[i].X + x < mapSize.X && neigbours[i].Y + y >= 0 && neigbours[i].Y + y < mapSize.Y)
                        setCell(new Point(neigbours[i].X + x, neigbours[i].Y + y));
                }
            }
        }

       /// <summary>
       /// Check if the cell is available
       /// </summary>
       /// <param name="cell">Cell to check</param>
       /// <param name="origin">Parent cell</param>
       /// <returns>Returns 1 if available, 0 if not</returns>
        int checkCell(Point cell, Point origin)
        {
            if (cell.X >= 0 && cell.X < mapSize.X && cell.Y >= 0 && cell.Y < mapSize.Y)
            {
                if (array[cell.X, cell.Y] != 0)
                    return 0;

                if (cell.X - 1 >= 0 && cell.X - 1 != origin.X)
                    if (array[cell.X - 1, cell.Y] != 0)
                        return 0;

                if (cell.X + 1 < mapSize.X && cell.X + 1 != origin.X)
                    if (array[cell.X + 1, cell.Y] != 0)
                        return 0;

                if (cell.Y - 1 >= 0 && cell.Y - 1 != origin.Y)
                    if (array[cell.X, cell.Y - 1] != 0)
                        return 0;

                if (cell.Y + 1 < mapSize.Y && cell.Y + 1 != origin.Y)
                    if (array[cell.X, cell.Y + 1] != 0)
                        return 0;

                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Fills list of neighbours randomly
        /// </summary>
        /// <param name="cell"></param>
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
                if ((rand.Next(0, 100) / 100.0f) < horizontalIntencity)
                {
                    if (rand.Next(0, 2) == 0 && !neighbours.Contains(list[0]))
                        neighbours.Add(list[0]);
                    else if (!neighbours.Contains(list[1]))
                        neighbours.Add(list[1]);
                }
                else
                {
                    if (rand.Next(0, 2) == 0 && !neighbours.Contains(list[2]))
                        neighbours.Add(list[2]);
                    else if (!neighbours.Contains(list[3]))
                        neighbours.Add(list[3]);
                }
            }

            return neighbours;
        }
    }
}
