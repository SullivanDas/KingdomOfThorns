using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ActionGrid
{
    public List<ActionList> Grid;

    public bool IsEmpty()
    {
        return Grid.Where(x => x.actions.Count != 0).Any();
    }
}
