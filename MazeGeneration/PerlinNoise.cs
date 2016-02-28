﻿using System;

namespace MazeGeneration
{
    public class PerlinNoiseGenerator
    {
        private const int GradientSizeTable = 256;
        private const int HashMask = 255;
        private readonly Random random;
        private readonly float[] gradients = new float[GradientSizeTable * 3];

        private readonly byte[] hash = new byte[] {
          225,155,210,108,175,199,221,144,203,116, 70,213, 69,158, 33,252,
          5, 82,173,133,222,139,174, 27,  9, 71, 90,246, 75,130, 91,191,
          169,138,  2,151,194,235, 81,  7, 25,113,228,159,205,253,134,142,
          248, 65,224,217, 22,121,229, 63, 89,103, 96,104,156, 17,201,129,
          36,  8,165,110,237,117,231, 56,132,211,152, 20,181,111,239,218,
          170,163, 51,172,157, 47, 80,212,176,250, 87, 49, 99,242,136,189,
          162,115, 44, 43,124, 94,150, 16,141,247, 32, 10,198,223,255, 72,
          53,131, 84, 57,220,197, 58, 50,208, 11,241, 28,  3,192, 62,202,
          18,215,153, 24, 76, 41, 15,179, 39, 46, 55,  6,128,167, 23,188,
          106, 34,187,140,164, 73,112,182,244,195,227, 13, 35, 77,196,185,
          26,200,226,119, 31,123,168,125,249, 68,183,230,177,135,160,180,
          12,  1,243,148,102,166, 38,238,251, 37,240,126, 64, 74,161, 40,
          184,149,171,178,101, 66, 29, 59,146, 61,254,107, 42, 86,154,  4,
          236,232,120, 21,233,209, 45, 98,193,114, 78, 19,206, 14,118,127,
          48, 79,147, 85, 30,207,219, 54, 88,234,190,122, 95, 67,143,109,
          137,214,145, 93, 92,100,245,  0,216,186, 60, 83,105, 97,204, 52
        };

        public PerlinNoiseGenerator(int seed)
        {
            random = new Random(seed);
            InitGradients();
        }

        private void InitGradients()
        {
            for (int i = 0; i < GradientSizeTable; i++)
            {
                float z = 1f - 2f * (float)random.NextDouble();
                float r = (float)Math.Sqrt(1f - z * z);
                float theta = 2 * (float)Math.PI * (float)random.NextDouble();

                gradients[i * 3] = r * (float)Math.Cos(theta);
                gradients[i * 3 + 1] = r * (float)Math.Sin(theta);
            }
        }

        public float Noise2D(float x, float y)
        {
            int xi = (int)Math.Floor(x) & HashMask;
            float fx0 = x - xi;
            float fx1 = fx0 - 1;

            int yi = (int)Math.Floor(y) & HashMask;
            float fy0 = y - yi;
            float fy1 = fy0 - 1;

            int aa = hash[hash[xi] + yi];
            int ab = hash[hash[xi + 1] + yi];
            int ba = hash[hash[xi] + yi + 1];
            int bb = hash[hash[xi + 1] + yi + 1];

            float vy0 = Mathf.SmootherStep(Lattice(aa, fx0, fy0), Lattice(ab, fx1, fy0), fx0);
            float vy1 = Mathf.SmootherStep(Lattice(ba, fx0, fy1), Lattice(bb, fx1, fy1), fx0);

            float vy2 = Mathf.SmootherStep(vy0, vy1, fy0);
            return (vy2 + 1) / 2;
        }

        private float Lattice(int index, float fx, float fy)
        {
            return gradients[index * 3] * fx + gradients[index * 3 + 1] * fy;
        }
    }
}