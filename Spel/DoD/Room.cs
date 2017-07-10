using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoD.Items;
using DoD.Characters.Monsters;

namespace DoD
{
    public class Room
    {
        public Monster Monster { get; set; }
        public Item Item { get; set; }
    }
}
