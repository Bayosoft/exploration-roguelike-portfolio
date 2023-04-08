using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorationRoguelike.Assets.Scripts.Combat
{
    public class TurnTakenEventArgs : EventArgs
    {
        public int Damage { get; private set; }
        public ICombatant Target { get; private set; }
        public ICombatant Attacker { get; private set; }

        public TurnTakenEventArgs(ICombatant target, ICombatant attacker, int damage)
        {
            Target = target;
            Attacker = attacker;
            Damage = damage;
        }
    }
}
