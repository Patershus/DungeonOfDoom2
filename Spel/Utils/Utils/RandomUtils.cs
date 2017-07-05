using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class RandomUtils
    {
        static Random random = new Random();

        /// <summary>
        /// Returns true in X percentage of time, X is the inparameter
        /// </summary>
        /// <param name="percent"></param>
        /// <returns>bool</returns>
        public static bool TestPercentage(double percent)
        {
            return (RandomNumber(0, 999) < percent * 10f);
        }

        /// <summary>
        /// Returns a random number between first and second parameter
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>int</returns>
        public static int RandomNumber(int first, int second)
        {
            return random.Next(first, second + 1);
        }
        
    }
}
