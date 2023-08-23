using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public class NpcCombatComponent : MonoBehaviour
    {
        private AbilitySystemComponent _npc;
        public List<GameplayAbility> abilities;
        public GameplayAbility DeclaredAbility { get; private set; }
        public event EventHandler<GameplayAbility> OnDeclaredIntent;

        public void Awake()
        {
            _npc = GetComponent<AbilitySystemComponent>();
            abilities = _npc.GrantedAbilities;
        }
        internal void ExecuteIntent(GameplayAbility declaredAbility, List<AbilitySystemComponent> targets)
        {
            _npc.TryActivateAbility(declaredAbility, targets);
        }
        public GameplayAbility DeclareIntent()
        {
            if (_npc.GrantedAbilities.Count > 0)
            {
                DeclaredAbility = abilities[new System.Random().Next(abilities.Count)];
            }
            else
            {
                DeclaredAbility = null;
            }
            
            OnDeclaredIntent?.Invoke(this, DeclaredAbility);

            return DeclaredAbility;
        }
    }
}
