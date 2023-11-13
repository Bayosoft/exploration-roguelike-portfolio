using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using Godot;
using System;
using System.Collections.Generic;

namespace ExplorationRoguelike.Combat
{
    [GlobalClass]
    public partial class NpcCombatComponent : Node
    {
        private AbilitySystemComponent _npc;
        public List<GameplayAbility> abilities;
        public GameplayAbility DeclaredAbility { get; private set; }
        public event EventHandler<GameplayAbility> OnDeclaredIntent;

        // TODO: Refactor to Godot.
  /*      public void Awake()
        {
            _npc = GetComponent<AbilitySystemComponent>();
            abilities = _npc.GrantedAbilities;
        }*/
        internal void ExecuteIntent(GameplayAbility declaredAbility, IEnumerable<AbilitySystemComponent> targets)
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
