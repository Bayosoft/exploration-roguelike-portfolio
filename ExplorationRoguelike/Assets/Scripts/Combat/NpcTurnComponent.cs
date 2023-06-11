using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExplorationRoguelike.Assets.Scripts.Combat
{
    public class NpcTurnComponent
    {
        public ObservableCollection<AbilitySO> CombatAbilities;
        public AbilitySO DeclaredAbility { get; private set; }

        public NpcTurnComponent(List<AbilitySO> abilities)
        {
            CombatAbilities = new(abilities);
        }

        public AbilitySO DeclareIntent()
        {
           DeclaredAbility = CombatAbilities[new Random().Next(CombatAbilities.Count)];
           return DeclaredAbility;
        }
    }
}
