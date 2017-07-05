using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    public class Sword : Item, IBackpackable
    {
        //Ctor
        public Sword(string name, int damage) : base(name, 'I')
        {
            Damage = damage;
        }

        //Prop
        public int Damage { get; set; }

        //Effekt av att använda svärd.
        public override string Use(Character character)
        {
            character.Damage += Damage;
            character.Backpack.Add(this);

            return $"You picked up a {this.Name}, {this.Damage} damage added!";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
