using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    public abstract class Monster : Character, IBackpackable
    {
        public static int MonsterCount { get; set; }

        public Monster(int health, int damage, int x, int y) : base(health, damage, 'M', x, y)
        {
            MonsterCount++;
        }

        //public int Health { get; set; }
    }
}
