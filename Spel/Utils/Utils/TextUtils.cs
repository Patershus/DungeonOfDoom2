using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils.Utils

{
    public static class TextUtils
    {
        /// <summary>
        /// Print animation for a given string
        /// </summary>
        /// <param name="value"></param>
        public static void Animate(string value)
        {
            foreach (char c in value)
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
            Console.WriteLine();
        }
    }
}
