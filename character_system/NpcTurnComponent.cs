using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Combat.Events;
using Godot;
using System.Collections.Generic;

namespace ExplorationRoguelike.Combat
{
    public partial class NpcTurnComponent : TurnComponent
    {
        [Export]
        private NpcCombatComponent npcCombatComponent;
        public NpcCombatComponent NpcCombatComponent { get => npcCombatComponent; }

        public override void StartTurn()
        {
            MyTurn = true;
        }

        public override void Act(GameplayAbility action, List<AbilitySystemComponent> targets)
        {
            if (MyTurn)
            {
                NpcCombatComponent.ExecuteIntent(action, targets);

                EndTurn();
            }
        }

        public override void EndTurn()
        {
            NpcCombatComponent.DeclareIntent();
            MyTurn = false;
            endTurnEvent.RaiseEvent(new EndTurnEventArgs(this));
        }

    }
}
