using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    public abstract class MapObject
    {
        public char MapChar { get; private set; }

        public MapObject(char mapChar)
        {
            MapChar = mapChar;
        }
    }
}
