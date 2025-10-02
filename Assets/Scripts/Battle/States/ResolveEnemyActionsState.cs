using System.Collections;
using UnityEngine;

/// <summary>
/// Resolve Enemy actions
/// </summary>
public class ResolveEnemyActionsState : BattleStateDefault
{
    public override void OnEnter()
    {
        base.OnEnter();
        StartCoroutine(PlayAndWaitForAllActions());
    }

    private IEnumerator PlayAndWaitForAllActions()
    {
        foreach (var action in battleStateManager.battleManager.enemyGridController.GetActions(true))
        {
            action.CallAction(null, null);
            yield return new WaitForSeconds(action.action.ActionDelayTime);
        }

        battleStateManager.battleManager.enemyGridController.AdvanceGrid();
        FinishState(new EventFinishedArgs());
    }
}
