using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MazeGeneration
{
    public class PseudoRandom
    {
        private int seed;
        private int M = 2147483647;
        private int A = 16807;
        private int Q, R;
        /// <summary>
        /// Constructor, insert a seed value
        /// </summary>
        /// <param name="seed"></param>
        public PseudoRandom(int seed)
        {
            this.seed = seed;
            Q = M / A;
            R = M % A;
        }
        /// <summary>
        /// Returns a random integer
        /// </summary>
        public int Next()
        {
            seed = A * (seed % Q) - R * (seed / Q);
            if (seed <= 0){
                seed += M;
            }
            return seed;
        }
        /// <summary>
        /// Returns a random integer from specific range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public int Next(int min, int max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("min is larger than max");

            if (min < 0 || max < 0)
                throw new ArgumentOutOfRangeException("parameter can't be negative");

            return min + (Next() % (max - min));
        }
    }
}
