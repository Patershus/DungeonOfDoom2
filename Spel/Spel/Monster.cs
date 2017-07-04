using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Monster : Character
    {
        public Monster(int health, int damage, int x, int y) : base(health, damage, 'M', x, y)
        {

        }

        //public int Health { get; set; }
    }
}
