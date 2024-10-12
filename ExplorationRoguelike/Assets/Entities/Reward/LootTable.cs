using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplorationRoguelike
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "LootTable")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private int minGold, maxGold;
        [SerializeField] private List<Artifact> artifacts;

        public int GetRandomGoldReward()
        {
            return Random.Range(minGold, maxGold);
        }

        public Artifact GetRandomArtifact()
        {
            //TODO: Refactor because expensive
            IEnumerable<Artifact> artifactsInPool = artifacts.Where(a => a.InPool);
            
            if (artifactsInPool.Count() == 0)
            {
                return null;
            }

            Artifact artifact = artifactsInPool.ToArray()[Random.Range(0, artifacts.Count)]; 

            artifact.InPool = false;

            return artifact;
        }

        public (int gold, List<Artifact> artifacts) GenerateLootTable(int artifactCount)
        {
            List<Artifact> artifacts = new List<Artifact>();

            for (int count = 0; count < artifactCount; count++)
            {
                Artifact artifact = GetRandomArtifact();
                if(artifact != null)
                {
                    artifacts.Add(artifact);
                }

            }

            return (GetRandomGoldReward(), artifacts);
        }

        public void ResetPool()
        {
            artifacts.ForEach(a => a.InPool = true);
        }
    }
}