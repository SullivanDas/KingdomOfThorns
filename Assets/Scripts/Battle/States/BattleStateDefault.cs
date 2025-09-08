using System;
using UnityEngine;

[RequireComponent(typeof(BattleStateManager))]
public class BattleStateDefault : MonoBehaviour
{
    #region fields

    protected BattleStateManager battleStateManager;

    [field: SerializeField]
    public string NextState { get; set; }


    protected bool finished;

    #endregion

    #region events

    public event EventHandler OnStateFinished;


    #endregion

    #region Unity

    private void Awake()
    {
        if(battleStateManager == null)
            battleStateManager = GetComponent<BattleStateManager>();
    }
    #endregion

    #region methods

    /// <summary>
    /// Called to start the state
    /// </summary>
    public virtual void OnEnter()
    {
        finished = false;
    }

    protected virtual void SlamState()
    {

    }

    protected virtual void FinishState()
    {
        //Ensure we can't end this state twice.
        if (!finished)
        {
            finished = true;

            EventFinishedArgs e = new EventFinishedArgs();
            OnStateFinished?.Invoke(this, e);
        }
    }

    public virtual string GetStateName()
    {
        return "BattleStateDefault";
    }
    #endregion

    /// <summary>
    /// Args, ensure we know the next state and if it should automatically play
    /// </summary>
    public class EventFinishedArgs : EventArgs
    {

        public int nextState;

        public EventFinishedArgs()
        {
            this.nextState = -1;
        }

        public EventFinishedArgs(int nextState)
        {
            this.nextState = nextState;
        }
    }
}
