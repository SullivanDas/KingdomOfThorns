using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyGridController : MonoBehaviour
{
    #region fields

    [SerializeField]
    protected SplineContainer splineContainer;

    #endregion

    #region properties

    public List<ActionList> queuedActions;

    /// <summary>
    /// The prefab base for the objects for the  grid
    /// </summary>
    public GameObject actionObject;

    #endregion

    #region methods

    public void PopulateGrid()
    {
        foreach (var spline in splineContainer.Splines)
        {
            foreach(var node in spline.Knots)
            {
                GameObject obj = Instantiate(actionObject, splineContainer.transform, false);
                obj.transform.position += (Vector3)node.Position;
            }
        }
    }

    public void GetGridItem()
    {

    }

    #endregion
}

