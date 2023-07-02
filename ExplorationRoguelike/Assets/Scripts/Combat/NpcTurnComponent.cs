using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExplorationRoguelike.Assets.Scripts.Combat
{
    public class NpcTurnComponent : TurnComponent<NpcCombatComponent>
    {
        public override void Act(GameplayAbility action, List<AbilitySystemComponent> targets)
        {
            if (MyTurn)
            {
                CombatPlayComponent.ExecuteIntent(action, targets);
            }
        }

        public override void EndTurn()
        {
            CombatPlayComponent.DeclareIntent();
        }
    }
}
