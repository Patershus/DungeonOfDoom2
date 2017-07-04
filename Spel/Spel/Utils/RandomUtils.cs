using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Utils
{
    static class RandomUtils
    {
        static Random random = new Random();

        public static bool TestPercentage(double percent)
        {
            if (random.Next(0, 1000) < percent*10)
            {
                return true;
            }
            else return false;
        }

        public static int RandomNumber(int first, int second)
        {
            return random.Next(first, second + 1);
        }
    }
}
