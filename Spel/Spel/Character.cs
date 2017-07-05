using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Character : MapObject, IBackpackable
    {

        public int Health { get; set; }
        public int Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<IBackpackable> Backpack { get; set; }


        public Character(int health, int damage, char mapChar, int x, int y) : base(mapChar)
        {
            X = x;
            Y = y;
            Health = health;
            Damage = damage;
            Backpack = new List<IBackpackable>();
        }


        public virtual string Fight(Character opponent)
        {
            opponent.Health -= this.Damage;
            if (opponent.Health > 0)
            {
                return opponent.Fight(this);
            }
            else
            {
                this.Backpack.Add(opponent);
                return $"{this} killed {opponent}";
                
            }
        }
        
    }
}
