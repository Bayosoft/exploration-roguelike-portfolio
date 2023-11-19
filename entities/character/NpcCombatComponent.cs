using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExplorationRoguelike.Combat
{
    [GlobalClass]
    public partial class NpcCombatComponent : Node
    {
        [Export]
        private AbilitySystemComponent _abilityComponent;
        public List<GameplayAbility> abilities;
        public GameplayAbility DeclaredAbility { get; private set; }
        public event EventHandler<GameplayAbility> OnDeclaredIntent;

        public override void _Ready()
        {
            base._Ready();
            abilities = _abilityComponent.GrantedAbilities.ToList();
        }

        internal void ExecuteIntent(GameplayAbility declaredAbility, IEnumerable<AbilitySystemComponent> targets)
        {
            _abilityComponent.TryActivateAbility(declaredAbility, targets);
        }
        public GameplayAbility DeclareIntent()
        {
            if (abilities.Count > 0)
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
