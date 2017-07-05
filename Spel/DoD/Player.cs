using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    public class Player : Character
    {
        public Player(int health, int x, int y, int damage) : base(health, damage, 'P', x , y)
        {
            //X = x;
            //Y = y;
            
        }

        //public int Health { get; set; }
        //public int X { get; set; }
        //public int Y { get; set; }
        //public List<IBackpackable> Backpack { get; set; }

        public override string ToString()
        {
            return "You";
        }
    }
}
