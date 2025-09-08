using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public static class Actions 
{
    public enum ActionNames { TestAction, TestAction2 }
    private static Dictionary<string, System.Action<List<Target>, Fighter>> actions = new Dictionary<string, System.Action<List<Target>, Fighter>>
    {
        { "TestAction", TestAction },
        { "TestAction2", TestAction2 }
    };

    public static Dictionary<string, string> descriptions = new Dictionary<string,string>();

    public static void CallFunc(ActionNames name, List<Target> targets, Fighter owner)
    {
        actions[name.ToString()].Invoke(targets, owner);
    }

    private static void TestAction(List<Target> targets, Fighter owner)
    {
        string output = string.Format("Targets {0}, Owner {1}", targets, owner);
        Debug.Log(output);
    }

    private static void TestAction2(List<Target> targets, Fighter owner)
    {
        string output = string.Format("A different func: Targets {0}, Owner {1}", targets, owner);
        Debug.Log(output);
    }
}
