using System.Collections;
using UnityEngine;

/// <summary>
/// Idle state used to delay between states. 
/// </summary>
public class IdleState : BattleStateDefault
{
    [SerializeField]
    private float delay = 2f;


    public override void OnEnter()
    {
        base.OnEnter();
        StartCoroutine(DelayThenEndState());
    }
    protected IEnumerator DelayThenEndState()
    {
        yield return new WaitForSeconds(delay);

        FinishState( new EventFinishedArgs());
    }
}
