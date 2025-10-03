using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

[Serializable]
public class ActionList 
{
    public List<ActionListMember> actions = new List<ActionListMember>();

    /// <summary>
    /// Returns any fast actions that will resolve this round. Will be null if empty
    /// </summary>
    /// <returns></returns>
    public List<ActionListMember> GetFastActions()
    {
        var list = actions.Where(x => x.Action.modifier == BattleAction.Modifier.Fast);
        return list.ToList<ActionListMember>();

    }

    public class ActionListMember
    {
        public BattleAction Action { get; set; }
        public List<Target> Targets { get; set; }
        public Fighter Owner { get; set; }
        public ActionListMember(BattleAction action, List<Target> targets, Fighter owner) 
        {
            Action = action;
            Targets = targets;
            Owner = owner;
        }

        public void CallFunc()
        {
            Actions.CallFunc(Action.ActionName, Targets, Owner);
        }


    }
}
