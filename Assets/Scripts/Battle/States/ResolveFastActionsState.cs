using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Used to resolve any fast actions before normal actions are resolved. 
/// </summary>
public class ResolveFastActionsState : BattleStateDefault
{
    private List<ActionObject> enemyFastActions;
    private List<ActionObject> playerFastActions;

    public override void OnEnter()
    {
        base.OnEnter();
        enemyFastActions = battleStateManager.battleManager.enemyGridController.GetFastActions();
        playerFastActions = battleStateManager.battleManager.PlayerActions.GetFastActions();

        if (enemyFastActions.Any() || playerFastActions.Any())
        {
            StartCoroutine(PlayAndWaitForAllActions());
        }
        else
        {
            FinishState(new EventFinishedArgs());
        }
    }

    protected IEnumerator PlayAndWaitForAllActions()
    {
        foreach (var action in playerFastActions)
        {
            action.CallAction(null, null);
            yield return new WaitForSeconds(action.action.ActionDelayTime);
        }

        foreach (var action in enemyFastActions)
        {
            action.CallAction(null, null);
            yield return new WaitForSeconds(action.action.ActionDelayTime);
        }

        yield return null;
        FinishState(new EventFinishedArgs());
    }
}
