using UnityEngine;

/// <summary>
/// Used to check if we've completed the battle, then exits the state manager if needed. 
/// </summary>
public class VerifyGameState : BattleStateDefault
{
    public override void OnEnter()
    {
        base.OnEnter();
        CheckPlayerLoss();
        CheckListEmpty();
        FinishState(new EventFinishedArgs());
        
    }

    protected void CheckPlayerLoss()
    {
        if (battleStateManager.battleManager.playerTeamController.IsDefeated())
        {
            //We've finished the battle go to end state
            int key = battleStateManager.GetStateKey("BattleLostState");
            EventFinishedArgs args = new EventFinishedArgs(key);
            FinishState(args);
        }
    }

    protected void CheckListEmpty()
    {
        if (battleStateManager.battleManager.enemyGridController.IsEmpty())
        {
            //We've finished the battle go to end state
            int key = battleStateManager.GetStateKey("EndBattleState");
            EventFinishedArgs args = new EventFinishedArgs(key);
            FinishState(args);
        }
    }
}
