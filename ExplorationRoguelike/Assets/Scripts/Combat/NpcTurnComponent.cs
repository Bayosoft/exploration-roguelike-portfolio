using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExplorationRoguelike.Assets.Scripts.Combat
{
    public class NpcTurnComponent
    {
        public ObservableCollection<GameplayAbility> CombatAbilities;
        public GameplayAbility DeclaredAbility { get; private set; }

        public NpcTurnComponent(List<GameplayAbility> abilities)
        {
            CombatAbilities = new(abilities);
        }

        public GameplayAbility DeclareIntent()
        {
           DeclaredAbility = CombatAbilities[new Random().Next(CombatAbilities.Count)];
           return DeclaredAbility;
        }
    }
}
