using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionObject : MonoBehaviour
{
    #region Constants
    private const string COLOR = "_Color";

    #endregion

    #region fields

    #endregion

    #region properties

    public BattleAction action { get; set; }

    public int CurrentHealth { get; private set; }

    #endregion

    #region methods

    public string GetDescription()
    {
        //If the key isn't initialized, we don't have a description for this abillity yet.
        try
        {
            return Actions.descriptions[action.ActionName.ToString()];
        }
        catch
        {
            return "Not Initialized";
        }

    }

    public void CallFunc(List<Target> targets, Fighter owner)
    {
        Actions.CallFunc(action.ActionName, targets, owner);
        Die();
    }

    public void Initialize(BattleAction action)
    {
        this.action = action;

    }

    /// <summary>
    /// Damages this action object
    /// </summary>
    public void Damage(int amount)
    {
        if (CurrentHealth - amount > 0)
        {
            CurrentHealth -= amount;
        }
        else
        {
            Die();
        }
    }

    /// <summary>
    /// Called on death
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region Unity

    private void Start()
    {
        CurrentHealth = action.Health;
    }
    #endregion
}
