using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Item : MapObject
    {
        public Item(string name, char mapChar) : base(mapChar)
        {
            Name = name;
            //Hallo igen
        }

        public string Name { get; private set; }

        public abstract string Use(Character player);
    }
}
