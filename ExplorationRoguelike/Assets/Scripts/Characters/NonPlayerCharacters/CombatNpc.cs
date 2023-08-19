using ExplorationRoguelike.Combat;
using UnityEngine;

namespace ExplorationRoguelike.Characters.NonPlayerCharacters
{
    public class CombatNpc : Character, ICombatant
    {
        [SerializeField]
        private HealthComponent _healthComponent;
        public HealthComponent HealthComponent { get => _healthComponent; }
        [SerializeField]
        private NpcTurnComponent _turnComponent;
        public TurnComponent TurnComponent{ get; private set; }
        public NpcCombatComponent NpcCombatComponent { get; private set; }
    }
}
