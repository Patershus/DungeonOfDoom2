using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Potion : Item
    {
        public Potion(int healing ,string name) : base(name, 'I')
        {
            Healing = healing;

        }

        public int Healing { get; private set; }

        public override string Use(Character player)
        {
            player.Health += 10;
            return $"You picked up a Potion, healed for 10!";
        }
    }

}
