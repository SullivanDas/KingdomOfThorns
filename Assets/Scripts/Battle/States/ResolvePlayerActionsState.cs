using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to resolve player actions in the action list
/// </summary>
public class ResolvePlayerActionsState : BattleStateDefault
{
    /// <summary>
    /// We need to wait for the grid to resolve updating the grid after every action. We don't know how long that'll take so we'll have to wait for an event to subscribe to
    /// </summary>
    private int currentAction;

    private List<ActionList.ActionListMember> actions;

    private ActionGridController gridController;

    public override void OnEnter()
    {
        base.OnEnter();
        currentAction = 0;
        gridController = battleStateManager.battleManager.enemyGridController;

        actions = battleStateManager.battleManager.playerTeamController.GetActions(true);
        PlayNextAction();
    }

    /// <summary>
    /// Double check that we've unregistered from events
    /// </summary>
    private void OnDestroy()
    {
        if(gridController != null)
            gridController.OnGridUpdateComplete -= gridController.OnGridUpdateComplete;
    }

    /// <summary>
    /// Plays the next action in the action list. We need to wait for the action grid to update after each action is called
    /// </summary>
    private void PlayNextAction()
    {
        if (currentAction >= actions.Count)
        {
            FinishState(new EventFinishedArgs());
        }
        else
        {
            StartCoroutine(CallAndWaitForFunc());

        }

    }

    private IEnumerator CallAndWaitForFunc()
    {
        actions[currentAction].CallFunc();
        yield return new WaitForSeconds(actions[currentAction].Action.ActionDelayTime);
        currentAction++;

        
        gridController.OnGridUpdateComplete += OnGridUpdateComplete;
        gridController.AdvanceGrid();

    }

    private void OnGridUpdateComplete(object sender, EventArgs e)
    {
        gridController.OnGridUpdateComplete -= OnGridUpdateComplete;
        PlayNextAction();
    }
}
