using System;
using UnityEngine;

public class TargetSelectionController : MonoBehaviour
{

    #region fields

    [SerializeField]
    private PlayerTeamController playerTeamController;

    #endregion

    #region events

    /// <summary>
    /// The event triggered when selection is complete. 
    /// </summary>
    public EventHandler OnSelectionCompleted;

    #endregion

    #region Unity

    private void Awake()
    {
        playerTeamController.OnSelectionEventStarted += StartSelectionEvent;
    }

    private void StartSelectionEvent(object sender, EventArgs e)
    {
        
    }
    #endregion
}
