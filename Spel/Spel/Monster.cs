﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Monster : Character, IBackpackable
    {
        public static int MonsterCount { get; set; }

        public Monster(int health, int damage, int x, int y) : base(health, damage, 'M', x, y)
        {
            MonsterCount++;
        }

        //public Item PutInBackpack(Monster monster)
        //{
        //    monster.ToString();

        //       Item itemToReturn= new Item("mon", 'M');
        //    return itemToReturn;
        //}



        //public int Health { get; set; }
    }
}
