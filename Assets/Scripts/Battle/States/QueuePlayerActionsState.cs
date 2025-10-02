using UnityEngine;

/// <summary>
/// Used to queue player actions into the action list
/// </summary>
public class QueuePlayerActionsState : BattleStateDefault
{
    public override void OnEnter()
    {
        base.OnEnter();

        //temp
        BattleAction temp = new BattleAction(Actions.ActionNames.TestAction, 1, BattleAction.Modifier.None);
        Target tempTarget = new Target(0,0);

        battleStateManager.battleManager.playerTeamController.QueueAction(temp, null, null);
        FinishState(new EventFinishedArgs());
    }
}
