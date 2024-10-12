namespace ExplorationRoguelike
{
    public class OnObtainArtifactRewardEventArgs : ConcreteEventArgs
    {
        public Artifact Artifact { get; }

        public OnObtainArtifactRewardEventArgs(Artifact artifact)
        {
            Artifact = artifact;
        }
    }
}
