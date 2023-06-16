using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExplorationRoguelike.Assets.Scripts.Combat
{
    public class NpcTurnComponent
    {
        public ObservableCollection<AbilityData> CombatAbilities;
        public AbilityData DeclaredAbility { get; private set; }

        public NpcTurnComponent(List<AbilityData> abilities)
        {
            CombatAbilities = new(abilities);
        }

        public AbilityData DeclareIntent()
        {
           DeclaredAbility = CombatAbilities[new Random().Next(CombatAbilities.Count)];
           return DeclaredAbility;
        }
    }
}
