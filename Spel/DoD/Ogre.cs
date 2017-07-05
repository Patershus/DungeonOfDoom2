using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    public class Ogre : Monster, IBackpackable
    {
        public Ogre(int x, int y) : base(30, 5, x, y)
        {

        }
        public override string ToString()
        {
            return "An Ogre";
        }
    }
}
