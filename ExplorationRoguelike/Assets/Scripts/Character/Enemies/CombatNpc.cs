using ExplorationRoguelike.Assets.Scripts.Combat;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class CombatNpc : Character, INpcCombatant
    {
        [SerializeField]
        private HealthComponent _healthComponent;
        public HealthComponent HealthComponent { get => _healthComponent; }
        [SerializeField]
        private NpcTurnComponent _turnComponent;
        public TurnComponent<NpcCombatComponent> TurnComponent{ get; private set; }

        public NpcCombatComponent NpcCombatComponent { get; private set; }
    }
}
