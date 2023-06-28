using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExplorationRoguelike.Assets.Scripts.Combat
{
    public class NpcTurnComponent : TurnComponent<NpcCombatComponent>
    {
        public NpcTurnComponent()
        {
        }

        public override void TakeAction()
        {
            CombatPlayComponent.ExecuteIntent();
            throw new NotImplementedException();
        }

        public override void EndTurn()
        {
            CombatPlayComponent.DeclareIntent();
            throw new NotImplementedException();
        }
    }
}
