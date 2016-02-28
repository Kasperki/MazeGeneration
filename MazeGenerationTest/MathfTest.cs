using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using MazeGeneration;

namespace MazeGenerationTest
{
    [TestFixture]
    class MathfTest
    {
        #region Clamp() Tests
        
        [Test]
        public void Clamp_should_return_minimum_value_if_less_than_min()
        {
            float expectedValue = -2;
            float actual = Mathf.Clamp(-6, expectedValue, 10);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void Clamp_should_return_same_value_if_between_min_and_max()
        {
            float expectedValue = 2;
            float actual = Mathf.Clamp(expectedValue, -2, 10);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void Clamp_should_return_maximum_value_if_greater_than_max()
        {
            float expectedValue = 10;
            float actual = Mathf.Clamp(12, -2, expectedValue);

            Assert.AreEqual(expectedValue, actual);
        }
        #endregion

        #region Lerp() Tests
        [Test]
        public void Lerp_should_lerp_value()
        {
            float expectedValue = 2;
            float actual = Mathf.Lerp(0, 10, 0.2f);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void Lerp_should_give_end_value_when_t_is_greater_than_1()
        {
            float expectedValue = 10;
            float actual = Mathf.Lerp(0, 10, 1.2f);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void Lerp_should_give_end_value_when_t_is_less_than_0()
        {
            float expectedValue = 1;
            float actual = Mathf.Lerp(1, 10, -1.2f);

            Assert.AreEqual(expectedValue, actual);
        }
        #endregion

        #region SmootherStep() Tests
        [Test]
        public void SmootherStep_should_lerp_value_smoothing_at_start()
        {
            float expectedValue = 0.579200029f; //t*t*t*(t*(t*6-15)+10);
            float actual = Mathf.SmootherStep(0, 10, 0.2f);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void SmootherStep_should_lerp_value()
        {
            float expectedValue = 5f; //t*t*t*(t*(t*6-15)+10);
            float actual = Mathf.SmootherStep(0, 10, 0.5f);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void SmootherStep_should_lerp_value_smoothing_in_end()
        {
            float expectedValue = 9.42080021f; //t*t*t*(t*(t*6-15)+10);
            float actual = Mathf.SmootherStep(0, 10, 0.8f);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void SmootherStep_should_give_end_value_when_t_is_greater_than_1()
        {
            float expectedValue = 10;
            float actual = Mathf.SmootherStep(0, 10, 1.2f);

            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void SmootherStep_should_give_end_value_when_t_is_less_than_0()
        {
            float expectedValue = 1;
            float actual = Mathf.SmootherStep(1, 10, -1.2f);

            Assert.AreEqual(expectedValue, actual);
        }
        
        #endregion
    }
}
