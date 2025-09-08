using UnityEngine;

public class ActionObject : MonoBehaviour
{
    #region Constants
    private const string COLOR = "_Color";

    #endregion

    #region fields

    

    #endregion

    #region properties

    public BattleAction action { get; set; }
    #endregion

    #region methods

    public string GetDescription()
    {
        //If the key isn't initialized, we don't have a description for this abillity yet.
        try
        {
            return Actions.descriptions[action.ActionName.ToString()];
        }
        catch
        {
            return string.Empty;
        }

    }

    public void Initialize()
    {

    }

    #endregion

}
