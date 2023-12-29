using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike.Combat
{
    public class NpcCombatComponent : MonoBehaviour
    {
        private AbilitySystemComponent _npc;
        public List<GameplayAbility> abilities;
        public GameplayAbility DeclaredAbility { get; private set; }
        public event EventHandler<IIntentfulAbility> OnDeclaredIntent;

        public void Awake()
        {
            _npc = GetComponent<AbilitySystemComponent>();
            abilities = _npc.GrantedAbilities.ToList();
        }
        internal void ExecuteIntent(GameplayAbility declaredAbility, IEnumerable<AbilitySystemComponent> targets)
        {
            _npc.TryActivateAbility(declaredAbility, targets);
        }
        public void DeclareIntent()
        {
            if (_npc.GrantedAbilities.Count > 0)
            {
                DeclaredAbility = abilities[new System.Random().Next(abilities.Count)];
            }
            else
            {
                DeclaredAbility = null;
            }

            if(DeclaredAbility is IIntentfulAbility intentfulAbility)
            {
                OnDeclaredIntent?.Invoke(this, intentfulAbility);
            }

        }
    }
}
