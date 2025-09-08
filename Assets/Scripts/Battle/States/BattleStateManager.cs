using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{
    #region fields

    /// <summary>
    /// For use in the editor, will populate statesDict on awake
    /// </summary>
    [SerializeField]
    private List<BattleStateDefault> states;

    /// <summary>
    /// Place conditional states at the begining then skip over them on loop. There's more elelgant solutions but this should work for the complexity needed here.
    /// </summary>
    [SerializeField]
    private int statesToSkipAfterLoop;

    /// <summary>
    /// A dictionary of the states controlled by the state manager
    /// </summary>
    private List<string> stateKeys;

    #endregion

    #region properties
    [field: SerializeField]
    public BattleManager battleManager { get; private set; }
    public int currentStateIndex { get; private set; }
    public int prevStateIndex { get; private set; }

    /// <summary>
    /// The next state, will usually only be initialized immediately before transferring to that state.
    /// </summary>
    public int nextState { get; private set; }

    #endregion

    #region Unity

    private void Awake()
    {
        foreach (BattleStateDefault state in states)
        {
            stateKeys.Add(state.GetStateName());
        }
        currentStateIndex = 0;
        nextState = -1;
    }

    /// <summary>
    /// Starts the state machine
    /// </summary>
    public void StartStateManager()
    {
        states[currentStateIndex].OnEnter();
        states[currentStateIndex].OnStateFinished += OnStateFinished;
    }

    /// <summary>
    /// Tries to convert the given key to it's associated index
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public int GetStateKey(string state)
    {
        try
        {
            int key = stateKeys.IndexOf(state);
            return key;
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// Called when a state completes, handle any specific transition state logic here.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnStateFinished(object sender, EventArgs e)
    {
        states[currentStateIndex].OnStateFinished -= OnStateFinished;
        prevStateIndex = currentStateIndex;
        BattleStateDefault.EventFinishedArgs args = (BattleStateDefault.EventFinishedArgs)e;

        //Wrap around if we've hit the end of the state queue
        nextState = (currentStateIndex + 1) % states.Count + statesToSkipAfterLoop;

        if (args != null)
        {
            //The state may be requesting an override for the next state
            if(args.nextState != -1)
                nextState = args.nextState;
               
        }

        currentStateIndex = nextState;
        states[currentStateIndex].OnEnter();
        states[currentStateIndex].OnStateFinished += OnStateFinished;
    }

    //Ensure all events have been unregistered 
    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void UnregisterEvents()
    {
        foreach(var state in states)
        {
            state.OnStateFinished -= OnStateFinished;
        }
    }
    #endregion
}
