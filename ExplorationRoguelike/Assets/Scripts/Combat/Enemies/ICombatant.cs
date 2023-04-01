using ExplorationRoguelike.Scripts.Combat;

internal interface ICombatant
{
    public void EnterCombat(ActiveCombat combat);
    public void ExitCombat(ActiveCombat combat);

    //public void TakeTurn();
}