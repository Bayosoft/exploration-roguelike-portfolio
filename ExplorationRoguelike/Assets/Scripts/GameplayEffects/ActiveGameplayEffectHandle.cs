using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.GameplayEffects
{
    public class ActiveGameplayEffectHandle
    {
        private bool wasAppliedSuccessfully = false;
        private int handleID = -1;

        private static int globalNextValidHandle = 0;

        public ActiveGameplayEffectHandle()
        {
            handleID = -1;
            wasAppliedSuccessfully = false;
        }

        public ActiveGameplayEffectHandle(int inHandle) 
        {
            handleID = inHandle;
            wasAppliedSuccessfully = true;
        }

        public bool IsValid()
        {
            return handleID > -1;
        }

        public bool WasAppliedSuccessfully()
        {
            return wasAppliedSuccessfully;
        }

        public static ActiveGameplayEffectHandle GenerateNew()
        {
            return new ActiveGameplayEffectHandle(globalNextValidHandle++);
        }
    }
}
