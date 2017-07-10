using DoD.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DoD
{
    public class Wizard : Monster
    {
        //array av spell strings, lambda, skulle kunna vara referens till namngivna metoder
        Func<Character, Character, string>[] spells =
            {
            (spellCaster, target) => {target.Health -= 10; return $"{spellCaster}s spell damaged {target} for 10"; },
            (spellCaster, target) => {spellCaster.Health += 10; return $"{spellCaster} healed for 10"; },
            (spellCaster, target) => {target.Health -= 5;spellCaster.Health+=5; return $"{spellCaster} drain 5 health from {target}"; },
        };

        public Wizard(int x, int y) : base(40, 0, x, y)
        {

        }

        public override string Attack(Character opponent)
        {
            //Väljer en random spell
            int spellNumber = RandomUtils.RandomNumber(0, spells.Length-1);
            Func<Character, Character, string> spell = spells[spellNumber];

            //Använder spell
             return spell.Invoke(this, opponent);
        }

        public override string ToString()
        {
            return "A Wizard";
        }

    }
}
