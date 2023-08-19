using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Combat.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public class NpcTurnComponent : TurnComponent
    {
        [SerializeField]
        private NpcCombatComponent _npcCombatComponent;
        public NpcCombatComponent NpcCombatComponent { get => _npcCombatComponent; }

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
            EndTurnEvent.RaiseEvent(new EndTurnEventArgs(this));
        }

    }
}
