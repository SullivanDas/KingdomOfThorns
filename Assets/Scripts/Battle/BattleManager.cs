using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    #region fields

    [field: SerializeField]
    public ActionGridController enemyGridController { get; private set; }

    [field: SerializeField]
    public PlayerTeamController playerTeamController { get; private set; }

    #endregion


    #region properties

    public ActionList PlayerActions { get; set; }
    #endregion

    #region Unity

    private void Start()
    {
        enemyGridController.PopulateGrid(); 

    }

    /// <summary>
    /// Ensures everything ready. Called once at the start of the battle
    /// </summary>
    private void Initialize()
    {

    }

    #endregion
}
