using System.Collections;
using System.Collections.Generic;

namespace ExplorationRoguelike.GameplayEffects
{
    public class ActiveGameplayEffectHandle
    {
        private bool _wasAppliedSuccessfully = false;
        private int _handleID = -1;

        private static int _globalNextValidHandle = 0;

        public ActiveGameplayEffectHandle()
        {
            _handleID = -1;
            _wasAppliedSuccessfully = false;
        }

        public ActiveGameplayEffectHandle(int inHandle) 
        {
            _handleID = inHandle;
            _wasAppliedSuccessfully = true;
        }

        public bool IsValid()
        {
            return _handleID > -1;
        }

        public bool WasAppliedSuccessfully()
        {
            return _wasAppliedSuccessfully;
        }

        public static ActiveGameplayEffectHandle GenerateNew()
        {
            return new ActiveGameplayEffectHandle(_globalNextValidHandle++);
        }
    }
}
