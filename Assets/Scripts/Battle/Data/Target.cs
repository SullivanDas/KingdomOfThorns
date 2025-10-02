using UnityEngine;

/// <summary>
/// Wrapper to easily handle targeting of actions in a specific fighter's queue.
/// </summary>
public class Target
{
    public int TargetCol { get; private set; }
    public int TargetRow { get; private set; }

    public Target(int col, int row)
    {
        TargetCol = col;
        TargetRow = row;
    }
}
