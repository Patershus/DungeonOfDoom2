using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Sword : Item
    {
        public Sword() : base("Sword", 'I')
        {

        }

        public override void Use(Character player)
        {
            player.Damage += 2;
        }
    }
}
