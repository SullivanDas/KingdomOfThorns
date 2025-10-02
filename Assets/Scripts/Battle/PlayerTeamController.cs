using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Actions;

public class PlayerTeamController : MonoBehaviour
{
    #region fields

    /// <summary>
    /// The menu to select an action
    /// </summary>
    [SerializeField]
    private CanvasGroup ActionMenu;

    /// <summary>
    /// The menu to select a fighter for an action
    /// </summary>
    [SerializeField]
    private CanvasGroup FighterMenu;

    [SerializeField]
    private GameObject buttonParent;

    [SerializeField]
    private GameObject buttonPrefab;

    /// <summary>
    /// The enabled Fighters
    /// </summary>
    [SerializeField]
    private List<Fighter> FighterList;

    [SerializeField]
    private TargetSelectionController targetSelectionController;

    /// <summary>
    /// Storage variables for Selection Mode
    /// </summary>
    private List<Target> selectedTargets;
    private Actions.ActionNames actionName;
    private Fighter owner;

    #endregion

    #region properties
    
    public ActionList PlayerActions { get; private set; }

    #endregion

    #region events

    public event EventHandler OnSelectionEventStarted;

    #endregion


    #region methods

    public void QueueAction(BattleAction action, List<Target> targets, Fighter owner)
    {
        PlayerActions.actions.Add(new ActionList.ActionListMember(action, targets, owner));
    }

    /// <summary>
    /// Gets the actions that are going to resolve this turn
    /// </summary>
    /// <param name="remove">True if we should clear the items that are returned from the list</param>
    /// <returns></returns>
    public List<ActionList.ActionListMember> GetActions(bool remove)
    {
        return PlayerActions.actions;
    }

    /// <summary>
    /// Enables the Fighter Selection Menu
    /// </summary>
    public void ToggleFighterMenu(int index)
    {
        ToggleFighterMenu(true, index);
    }

    /// <summary>
    /// Enables the fighter menu - called to handle direct set of enable
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="index"></param>
    protected void ToggleFighterMenu(bool enable, int index)
    {
        if (enable)
        {
            
            FighterMenu.gameObject.SetActive(true);
        }
        else
        {
            FighterMenu.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the Action Menu
    /// </summary>
    /// <param name="enable"></param>
    public void ToggleActionMenu(int index)
    {
        ToggleActionMenu(true, index);
    }

    /// <summary>
    /// Checks if the player team has been defeated
    /// </summary>
    /// <returns></returns>
    public bool IsDefeated()
    {
        return !FighterList.Where(x => x.CurrentHealth > 0).Any();
    }
    /// <summary>
    /// Enables the action menu - called to handle direct set of enable
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="index"></param>
    protected void ToggleActionMenu(bool enable, int index)
    {
        if (enable)
        {
            ToggleFighterMenu(false, index);
            populateActionMenu(index);
            ActionMenu.gameObject.SetActive(true);

        }
        else
        {
            ActionMenu.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Fills the opened action menu with available skills
    /// </summary>
    /// <param name="index"></param>
    protected void populateActionMenu(int index)
    {
        foreach (var action in FighterList[index].AvailableActions)
        {
            GameObject obj = Instantiate(buttonPrefab, buttonParent.transform);
            Button button = obj.GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(
                    () => { AssignTargets(action, FighterList[index]); }
                    );
            }
        }
    }

    /// <summary>
    /// Starts the target assignment
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="fighter"></param>
    protected void AssignTargets(Actions.ActionNames actionName, Fighter fighter)
    {
        this.actionName = actionName;
        owner = fighter;
        ToggleActionMenu(false, 0);
        ToggleFighterMenu(false, 0);

        //Temp
        OnSelectedTargets();
    }

    public void OnSelectedTargets()
    {
        Actions.CallFunc(actionName, selectedTargets, owner); 
    }

    #endregion

}
