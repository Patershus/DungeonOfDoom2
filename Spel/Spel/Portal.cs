using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Portal : Item
    {
        static Random random = new Random();

        public Portal(string name) : base(name, 'O')
        {

        }
        public override string Use(Character character)
        {
            int newX = 0;
            int newY = 0;
            newX = random.Next(0, 20);
            newY = random.Next(0, 5);
           

            character.X = newX;
            character.Y = newY;

            return $"{character} teleported through a portal";
        }
    }
}
