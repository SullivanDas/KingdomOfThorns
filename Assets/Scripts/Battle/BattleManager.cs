using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    #region fields

    [SerializeField]
    private List<ActionList> enemyActionBacklog;

    [SerializeField]
    private PlayerTeamController playerTeamController;

    [SerializeField]
    private EnemyGridController enemyGridController;

    [SerializeField]
    private Dictionary<string, BattleStateDefault> battleStates;

    #endregion


    #region properties

    [field: SerializeField]
    public BattleManager battleManager { get; private set; }

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
