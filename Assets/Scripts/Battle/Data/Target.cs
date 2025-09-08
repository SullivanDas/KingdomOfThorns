using UnityEngine;

/// <summary>
/// Wrapper to easily handle targeting of actions in a specific fighter's queue.
/// </summary>
public class Target
{
    public ActionList TargetList { get; private set; }
    public int TargetIndex { get; private set; }

    public Target(ActionList targetList, int index)
    {
        TargetList = targetList; 
        TargetIndex = index;
    }
}
