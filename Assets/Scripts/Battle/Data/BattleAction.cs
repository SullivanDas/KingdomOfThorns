using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BattleAction
{
    public enum Modifier { None, Thorns, Fast }

    public Actions.ActionNames ActionName;
    public int Health;
    public Modifier modifier;
    public float ActionDelayTime;

    public BattleAction(Actions.ActionNames actionName, int health, Modifier modifier)
    {
        this.ActionName = actionName;
        this.Health = health;
        this.modifier = modifier;
    }

    /// <summary>
    /// Lets us serialize a list of battleactions to the editor
    /// </summary>
    [Serializable]
    public class BattleActionWrapper
    {
        public List<BattleAction> actions = new List<BattleAction>();
    }
}
