using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike
{
    public class NpcCombatComponent : MonoBehaviour
    {
        private AbilitySystemComponent _npc;
        public List<GameplayAbility> Abilities;
        public GameplayAbility DeclaredAbility { get; private set; }

        public void Start()
        {
            _npc = GetComponent<AbilitySystemComponent>();
            Abilities = _npc.GrantedAbilities;
        }
        internal void ExecuteIntent(GameplayAbility declaredAbility, List<AbilitySystemComponent> targets)
        {
            _npc.TryActivateAbility(declaredAbility, targets);
        }
        public GameplayAbility DeclareIntent()
        {
            if (_npc.GrantedAbilities.Count > 0)
            {
                DeclaredAbility = Abilities[new System.Random().Next(Abilities.Count)];
            }
            else
            {
                DeclaredAbility = null;
            }

            return DeclaredAbility;
        }
    }
}
