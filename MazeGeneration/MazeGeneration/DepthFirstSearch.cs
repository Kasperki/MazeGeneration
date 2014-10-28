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
        int[,] array;
        Point mapSize;
        Random rand = new Random();

        /// <summary>
        /// Generates map
        /// </summary>
        /// <param name="mapSize"></param>
        /// <param name="startCell"></param>
        /// <returns>Map array</returns>
        public int[,] Generate(Point mapSize, Point startCell)
        {
            this.mapSize.X = mapSize.X;
            this.mapSize.Y = mapSize.Y;
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

            List<Point> neigbours = new List<Point>();
            neigbours.Add(new Point(cell.X - 1, cell.Y));
            neigbours.Add(new Point(cell.X + 1, cell.Y));
            neigbours.Add(new Point(cell.X, cell.Y - 1));
            neigbours.Add(new Point(cell.X, cell.Y + 1));

            //Todo Suffle List - Based on vertical / horizontal maze intenisty
             //Go List Trough ->
             
            //Todo Change checkCell to look Xcount cells forward.

            while (neigbours.Count > 0)
            {
                int rnd = rand.Next(0, neigbours.Count);

                if (checkCell(neigbours[rnd], cell) == 0)
                {
                    neigbours.RemoveAt(rnd);
                }
                else if (checkCell(neigbours[rnd], cell) == 1)
                {
                    array[neigbours[rnd].X,neigbours[rnd].Y] = 1;

                    int x = neigbours[rnd].X - cell.X;
                    int y = neigbours[rnd].Y - cell.Y;

                    if (neigbours[rnd].X + x >= 0 && neigbours[rnd].X + x < mapSize.X && neigbours[rnd].Y + y >= 0 && neigbours[rnd].Y + y < mapSize.Y)
                        setCell(new Point(neigbours[rnd].X + x, neigbours[rnd].Y + y));
                    else
                        neigbours.RemoveAt(rnd);
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
    
    }
}
