using System;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using MazeGeneration;

namespace MazeGenerationTest
{
    [TestFixture]
    public class PseudoRandomTest
    {
        [Test]
        public void Next_Should_Return_Number_Between_Min_Max()
        {
            PseudoRandom pseudoRandom = new PseudoRandom(0);

            int minCount = 0;
            int maxCount = 5;

            for (int i = 0; i < 100; i++)
            {
                int actualCount = pseudoRandom.Next(minCount, maxCount);

                Assert.IsTrue(actualCount >= minCount);
                Assert.IsTrue(actualCount < maxCount);
            }

            int minCount2 = 200;
            int maxCount2 = 9999999;

            for (int i = 0; i < 1000; i++)
            {
                int actualCount2 = pseudoRandom.Next(minCount2, maxCount2);

                Assert.IsTrue(actualCount2 >= minCount2);
                Assert.IsTrue(actualCount2 < maxCount2);
            }
        }

        [Test]
        public void Next_Should_Throw_Exception_If_Min_Or_Max_Is_Negative()
        {
            PseudoRandom pseudoRandom = new PseudoRandom(0);

            int minCount = -5;
            int maxCount = -1;

            Assert.That(() => pseudoRandom.Next(minCount, maxCount), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Next_Should_Throw_Exception_If_Min_Is_Greater_Than_Max()
        {
            PseudoRandom pseudoRandom = new PseudoRandom(0);

            int minCount = 5;
            int maxCount = 0;

            Assert.That(() => pseudoRandom.Next(minCount, maxCount), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Next_Should_Return_Same_Numbers_On_Same_Seed()
        {
            int seed = 1337;
            int count = 100;

            PseudoRandom pseudoRandom = new PseudoRandom(seed);
            int[] excpectedNumbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                excpectedNumbers[i] = pseudoRandom.Next();
            }

            PseudoRandom pseudoRandom2 = new PseudoRandom(seed);
            int[] actualNumbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                actualNumbers[i] = pseudoRandom2.Next();
            }

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(excpectedNumbers[i], actualNumbers[i]);
            }
        }
    }
}
