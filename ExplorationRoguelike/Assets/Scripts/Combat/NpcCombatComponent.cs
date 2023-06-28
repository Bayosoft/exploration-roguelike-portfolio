using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExplorationRoguelike
{
    public class NpcCombatComponent
    {
        public ObservableCollection<GameplayAbility> CombatAbilities;
        public GameplayAbility DeclaredAbility { get; private set; }

        public NpcCombatComponent(List<GameplayAbility> combatAbilities)
        {
            CombatAbilities = combatAbilities;   
        }
        internal void ExecuteIntent()
        {
            throw new NotImplementedException();
        }
        public GameplayAbility DeclareIntent()
        {
            if (CombatAbilities.Count > 0)
            {
                DeclaredAbility = CombatAbilities[new Random().Next(CombatAbilities.Count)];
            }
            else
            {
                DeclaredAbility = null;
            }

            return DeclaredAbility;
        }
    }
}
