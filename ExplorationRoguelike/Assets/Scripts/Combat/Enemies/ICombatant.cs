using ExplorationRoguelike;
using ExplorationRoguelike.Assets.Scripts.Combat;
using ExplorationRoguelike.Scripts.Combat;

public interface ICombatant
{
    public void EnterCombat(ActiveCombat combat);
    public void ExitCombat(ActiveCombat combat);

    public void ExecuteTurn(TakeTurnEventArgs takeTurnEventArgs);
    public void ReceiveAttack(object sender, TurnTakenEventArgs takeTurnEventArgs);
}