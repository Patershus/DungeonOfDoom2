using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Sword : Item
    {
        //Ctor
        public Sword(string name, int damage) : base(name, 'I')
        {
            Damage = damage;
        }

        //Prop
        public int Damage { get; set; }

        //Effekt av att använda svärd.
        public override string Use(Character player)
        {
            player.Damage += Damage;
            return $"You picked up a {this.Name}, {this.Damage} damage added!";
        }
    }
}
