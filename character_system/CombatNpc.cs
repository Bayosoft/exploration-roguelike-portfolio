using ExplorationRoguelike.Combat;
using Godot;

namespace ExplorationRoguelike.Characters.NonPlayerCharacters
{
    public partial class CombatNpc : Character, ICombatant
    {
        [Export]
        private HealthComponent healthComponent;
        public HealthComponent HealthComponent { get => healthComponent; }
        [Export]
        private NpcTurnComponent turnComponent;
        public TurnComponent TurnComponent => turnComponent;
        
        [Export]
        private NpcCombatComponent npcCombatComponent;
        public NpcCombatComponent NpcCombatComponent => npcCombatComponent;
    }
}
