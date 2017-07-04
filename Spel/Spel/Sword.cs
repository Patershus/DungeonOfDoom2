using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Sword : Item
    {
        public Sword(string name, int damage) : base(name, 'I')
        {
            Damage = damage;
        }

        public int Damage { get; set; }

        public override string Use(Character player)
        {
            player.Damage += Damage;
            return $"You picked up a {this.Name}, {this.Damage} damage added!";
        }
    }
}
