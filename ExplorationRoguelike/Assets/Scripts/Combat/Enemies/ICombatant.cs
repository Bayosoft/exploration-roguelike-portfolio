using ExplorationRoguelike;
using ExplorationRoguelike.Scripts.Combat;

public interface ICombatant
{
    public void EnterCombat(ActiveCombat combat);
    public void ExitCombat(ActiveCombat combat);

    public void TakeTurn(TakeTurnEventArgs takeTurnEventArgs);
    public void ReceiveAttack(TakeTurnEventArgs takeTurnEventArgs);
}