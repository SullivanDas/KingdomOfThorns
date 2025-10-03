using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ActionList;

/// <summary>
/// Used to resolve any fast actions before normal actions are resolved. 
/// </summary>
public class ResolveFastActionsState : BattleStateDefault
{
    private List<ActionObject> enemyFastActions;
    private List<ActionListMember> playerFastActions;

    public override void OnEnter()
    {
        base.OnEnter();
        enemyFastActions = battleStateManager.battleManager.enemyGridController.GetFastActions();
        playerFastActions = battleStateManager.battleManager.playerTeamController.PlayerActions.GetFastActions();

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
            action.CallFunc();
            yield return new WaitForSeconds(action.Action.ActionDelayTime);
        }

        foreach (var action in enemyFastActions)
        {
            action.CallFunc(null, null);
            yield return new WaitForSeconds(action.action.ActionDelayTime);
        }

        yield return null;
        FinishState(new EventFinishedArgs());
    }
}
