using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Orc : Monster
    {
        public Orc() : base(20, 5)
        {

        }

        public override string Fight(Character opponent)
        {
            if (opponent.Damage>this.Damage*2)
            {
                this.Health = 0;
                return $"{opponent} killed {this}";
            }
            else
            {
                opponent.Health -= this.Damage;

                if (opponent.Health>0)
                {
                return opponent.Fight(this);
                }
                return $"{this} killed {opponent}";
            }
            
        }
        public override string ToString()
        {
            return "An Orc";
        }
    }
}
