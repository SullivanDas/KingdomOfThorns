using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    #region fields

    [SerializeField]
    private Dictionary<int, Actions.ActionNames> levelUpAbillities;

    #endregion

    #region properties

    public enum FighterTypes { DPS, Tank, Support}

    [field: SerializeField]
    public int MaxHealth { get; set; }

    [field: SerializeField]
    public FighterTypes fighterType;

    [field: SerializeField]
    public List<Actions.ActionNames> AvailableActions { get; private set; }

    public int CurrentHealth { get; set; }
    public int Shields { get; set; }

    #endregion
    #region Unity

    private void Start()
    {

    }

    #endregion

    #region methods

    public void LevelUp()
    {

    }

    protected void AddAction(Actions.ActionNames name)
    {
        AvailableActions.Add(name);
    }
    #endregion
}
