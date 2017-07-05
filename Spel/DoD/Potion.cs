using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    public class Potion : Item, IBackpackable
    {
        public Potion(int healing ,string name) : base(name, 'I')
        {
            Healing = healing;

        }

        public int Healing { get; private set; }

        public override string Use(Character character)
        {
            character.Health += Healing;
            character.Backpack.Add(this);
            return $"You picked up a Potion, healed for 10!";

        }

        public override string ToString()
        {
            return Name;
        }
    }

}
