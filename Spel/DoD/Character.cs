using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoD
{
    public abstract class Character : MapObject, IBackpackable
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

        /// <summary>
        /// Two characters fight to the death!
        /// </summary>
        /// <param name="opponent">Character to fight</param>
        /// <returns>Returns a string to print</returns>
        public virtual string Attack(Character opponent)
        {
            opponent.Health -= this.Damage;

            return $"{this} damaged {opponent} for {Damage}";                
            
        }
        
    }
}
