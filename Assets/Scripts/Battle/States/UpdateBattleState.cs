using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Update any other info at the end of the loop
/// </summary>
public class UpdateBattleState : BattleStateDefault
{
    public override void OnEnter()
    {
        base.OnEnter();
        FinishState(new EventFinishedArgs());
    }
}
