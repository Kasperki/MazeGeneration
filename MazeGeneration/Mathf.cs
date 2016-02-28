namespace MazeGeneration
{
    public class Mathf
    {
        /// <summary>
        /// Clamps value between min and max
        /// </summary>
        /// <param name="value">value to be clamped</param>
        /// <param name="min">minimum allowed value</param>
        /// <param name="max">maximum allowed value</param>
        /// <returns></returns>
        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value; 
        }

        /// <summary>
        /// Lerp values between start and end
        /// </summary>
        /// <param name="start">when t = 0</param>
        /// <param name="end">when t = 1</param>
        /// <param name="t">0.0 - 1.0</param>
        /// <returns></returns>
        public static float Lerp(float start, float end, float t)
        {
            t = Clamp(t, 0, 1);
            return start * (1 - t) + end * t;
        }

        /// <summary>
        /// Lerp with smoothed on start and end
        /// Ken Perlins improved version of SmoothStep with (x) = 6x^5 - 15x^4 + 10^3
        /// </summary>
        /// <param name="start">when t = 0</param>
        /// <param name="end">when t = 1</param>
        /// <param name="x">0.0 - 1.0</param>
        /// <returns></returns>
        public static float SmootherStep(float start, float end, float x)
        {
            return Lerp(start, end, x * x * x * (x * (x * 6 - 15) + 10));
        }
    }
}
