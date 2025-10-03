using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Splines;

public class ActionGridController : MonoBehaviour
{
    #region fields 

    [SerializeField]
    protected SplineContainer splineContainer;

    [SerializeField]
    protected BattleManager battleManager;

    /// <summary>
    /// The backing info that we use to define the grid objects
    /// </summary>
    [SerializeField]
    private BattleAction.BattleActionWrapper[] Grid = new BattleAction.BattleActionWrapper[3];

    /// <summary>
    /// The prefab base for the objects for the  grid
    /// </summary>
    [SerializeField]
    private GameObject actionObject;

    [field: SerializeField]
    public int RowCount { get; set; }

    /// <summary>
    /// The Objects that are not currently displayed in the grid
    /// </summary>
    private List<ActionObject>[] GridObjectsBacklog = new List<ActionObject>[3];

    /// <summary>
    /// We will init this based on the data supplied in Grid (in the editor) only holds visible objects
    /// </summary>
    private List<ActionObject>[] GridObjects = new List<ActionObject>[3];


    #endregion

    #region events

    public EventHandler OnGridUpdateComplete;

    #endregion

    #region Unity

    private void Awake()
    {
        //Ensure the array of lists does not have null members
        for (int i = 0; i < GridObjects.Length; i++)
        {
            GridObjects[i] = new List<ActionObject>();
        }
    }

    #endregion


    #region methods

    /// <summary>
    /// Checks if we've finished the battle
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return GridObjects.Any(x => x == null || x.Count()<=0) && GridObjectsBacklog.Any(x => x == null || x.Count() <= 0);
        //GridObjectsBacklog.Where(x => x.Count != 0).Any();
    }

    /// <summary>
    /// Pops the top object of given list
    /// </summary>
    /// <param name="col">Which column we should check the list of</param>
    /// <param name="targetList">The list we want to peek</param>
    /// <returns></returns>
    public ActionObject PopActionObject(int col, List<ActionObject>[] targetList)
    {
        if (!targetList.Any(x => x != null))
            return null;
        if (targetList[col].Count > 0)
        {
            ActionObject returnValue = targetList[col][0];
            targetList[col].RemoveAt(0);
            return returnValue;
        }
        else
        {
            return null;
        }
    }

    public void Init()
    {

    }

    /// <summary>
    /// Returns any fast actions that will resolve this round. Will be null if empty
    /// </summary>
    /// <returns></returns>
    public List<ActionObject> GetFastActions()
    {
        List<ActionObject> fastActions = GetActions(false);
        var filteredActions = fastActions.Where(x => x.action.modifier == BattleAction.Modifier.Fast);
        return filteredActions.ToList<ActionObject>();

    }

    /// <summary>
    /// Gets the actions that are set to activate this turn
    /// </summary>
    /// <param name="remove"></param>
    /// <returns></returns>
    public List<ActionObject> GetActions(bool remove)
    {
        List<ActionObject> actions = new List<ActionObject>();
        for (int i = 0; i < GridObjects.Count(); i++)
        {
            //Decide if we're modifying the grid or just peaking the value
            if (remove)
            {
                actions.Add(PopActionObject(i, GridObjects));
            }
            else
            {
                actions.Add(GridObjects[i][0]);
            }
        }
        return actions;
    }

    /// <summary>
    /// Called once at the begining of the battle to populate lists of action objects
    /// </summary>
    public void PopulateGrid()
    {
        for(int i = 0; i< Grid.Length; i++)
        {
            int row = 0;
            for (int j = 0; j < Grid[i].actions.Count(); j++)
            {
                //Instantiate, Initialize, then move onto the spline
                GameObject obj = Instantiate(actionObject, splineContainer.transform, false);
                ActionObject actionObj = obj.GetComponent<ActionObject>();
                actionObj.Initialize(Grid[i].actions[j]);
                
                //Only move enough objects onto the grid as the grid allows for, also populate the gridObjects lists
                if (j < RowCount)
                {
                    GridObjects[i].Add(actionObj);
                    var node = splineContainer.Splines[i][j];
                    obj.transform.position += (Vector3)node.Position;
                }
                else
                {
                    //Fill the remaining objects into the backlog
                    GridObjectsBacklog[i].Add(actionObj);
                }

            }
        }

        //foreach (var spline in splineContainer.Splines)
        //{
        //    foreach (var node in spline.Knots)
        //    {
        //        GameObject obj = Instantiate(actionObject, splineContainer.transform, false);
        //        obj.transform.position += (Vector3)node.Position;
        //    }
        //}

    }

    /// <summary>
    /// Advances positions in the grid to ensure no empty spaces are shown
    /// </summary>
    public void AdvanceGrid()
    {
        for(int i = 0; i < 3; i++)
        {
            AdvanceColumn(i);
        }
    }

    /// <summary>
    /// Advances positions in a given column to ensure no empty spaces are shown
    /// </summary>
    private void AdvanceColumn(int col)
    {
        List<ActionObject> newList = new List<ActionObject>();

        foreach(ActionObject action in GridObjects[col])
        {
            if(action != null)
            {
                newList.Add(action);
            }
        }

        //Fill in the newly empty spaces with objects from the backlog
        for(int i = 0; i < RowCount - newList.Count(); i++)
        {
            if(GridObjectsBacklog.Any(x => x != null))
                newList.Add(PopActionObject(col, GridObjectsBacklog));
        }
        GridObjects[col] = newList;
        StartCoroutine(SlideActionObjectsToNewPosition());
    }

    private IEnumerator SlideActionObjectsToNewPosition()
    {
        for (int i = 0; i < GridObjects.Count(); i++)
        {
            //One of the lists may be empty before the others skip it
            if (GridObjects[i] == null)
                continue;

            //We may be under the row index 
            int rowIndex = GridObjects[i].Count - 1;

            List<ActionObject> col = GridObjects[i];
            foreach (var knot in splineContainer[i].Knots)
            {
                //Can occur if there are less items left than there are knots
                if (rowIndex < 0)
                    break;
                ActionObject obj = col[rowIndex];
                obj.transform.position = splineContainer.transform.position + (Vector3)knot.Position;
                rowIndex--;
            }
        }
        yield return null;
        OnGridUpdateComplete?.Invoke(this, new EventArgs());
    }

    public ActionObject GetGridItem(Target target)
    {
        return GridObjects[target.TargetCol][target.TargetRow];
    }

    public void DamageGridItem(Target target, int amount)
    {
        GridObjects[target.TargetCol][target.TargetRow].Damage(amount);
    }

    #endregion

}
