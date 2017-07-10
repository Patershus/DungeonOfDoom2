using DoD.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD.Characters.Monsters
{
    public class Orc : Monster, IBackpackable
    {
        public Orc(int x, int y) : base(20, 5, x, y)
        {

        }

        /// <summary>
        /// An orc fights another character
        /// </summary>
        /// <param name="opponent">The opponent to fight</param>
        /// <returns>string to print</returns>
        public override string Attack(Character opponent)
        {
            if (opponent.Damage>this.Damage*2)
            {
                this.Health = 0;
                opponent.Backpack.Add(this);
                return $"{this} died from fear";
            }
            else
            {
                opponent.Health -= this.Damage;
         
                return $"{this} damaged {opponent} for {this.Damage}";
            }
            
        }
        public override string ToString()
        {
            return "An Orc";
        }
    }
}
