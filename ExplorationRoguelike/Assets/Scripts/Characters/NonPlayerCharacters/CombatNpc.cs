using ExplorationRoguelike.Combat;
using UnityEngine;

namespace ExplorationRoguelike.Characters.NonPlayerCharacters
{
    public class CombatNpc : Character, ICombatant
    {
        [SerializeField]
        private HealthComponent healthComponent;
        public HealthComponent HealthComponent { get => healthComponent; }
        [SerializeField]
        private NpcTurnComponent turnComponent;
        public TurnComponent TurnComponent{ get; private set; }
        public NpcCombatComponent NpcCombatComponent { get; private set; }
    }
}
