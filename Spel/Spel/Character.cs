﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Character : MapObject
    {

        public int Health { get; set; }
        public int Damage { get; set; }

        public Character(int health, int damage, char mapChar) : base(mapChar)
        {
            Health = health;
            Damage = damage;
        }
        public virtual string Fight(Character opponent)
        {
            opponent.Health -= this.Damage;
            if (opponent.Health > 0)
            {
                return opponent.Fight(this);
            }
            else
            {
            return $"{this} killed {opponent}";
                
            }
        }
        //todo Fixa ToString för alla char
    }
}
