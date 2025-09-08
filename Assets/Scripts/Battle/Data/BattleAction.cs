using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BattleAction
{
    public enum Modifier { None, Thorns }

    public Actions.ActionNames ActionName;
    public int Health;
    public Modifier modifier;

}
