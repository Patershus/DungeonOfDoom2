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
            }
            else
            {
                opponent.Health -= this.Damage;

                if (opponent.Health>0)
                {
                opponent.Fight(this);
                }
            }
            return "";
        }
    }
}
