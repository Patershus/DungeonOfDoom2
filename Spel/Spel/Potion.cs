using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Potion : Item
    {
        public Potion(int healing) : base("Potion", 'I')
        {
            Healing = healing;

        }

        public int Healing { get; private set; }

        public override void Use(Character player)
        {
            player.Health += 10;   
        }
    }

}
