using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Monster : Character
    {
        public Monster(int health, int damage) : base(health, damage, 'M')
        {

        }

        //public int Health { get; set; }
    }
}
