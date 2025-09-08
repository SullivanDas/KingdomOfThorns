using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActionDescriptionUpdator : EditorWindow
{

    #region fields

    private bool initialized;
    private List<string> keys = new List<string>();

    [SerializeField]
    private List<string> descriptions = new List<string>();

    private SerializedObject sO;

    public enum TestEnum { a, b, c };
    #endregion

    #region Unity

    /// <summary>
    /// Shows the window named Action Descriptions
    /// </summary>
    [MenuItem("Tools/Action Descriptions")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ActionDescriptionUpdator));
    }

    private void OnEnable()
    {
        foreach (var name in Enum.GetValues(typeof(Actions.ActionNames)))
        {
            
            keys.Add(name.ToString());
            if (Actions.descriptions.ContainsKey(name.ToString()) && Actions.descriptions[name.ToString()] != string.Empty)
            {
                descriptions.Add(Actions.descriptions[name.ToString()]);
            }
            else
            {
                descriptions.Add(name.ToString());
            }

        }

        ScriptableObject target = this;
        sO = new SerializedObject(target);
        initialized = true;
    }

    private void OnGUI()
    {
        if (!initialized)
            return;
        sO.Update();
        SerializedProperty descriptionsProperty = sO.FindProperty("descriptions");

        GUILayout.Label("Action Descriptions", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(descriptionsProperty, true);

        sO.ApplyModifiedProperties();

        if(GUILayout.Button("Update Descriptions"))
        {
            UpdateDescriptions();
        }
        if (GUILayout.Button("Clear Descriptions"))
        {
            ClearDescriptions();
        }

    }



    #endregion

    #region methods

    private void UpdateDescriptions()
    {
        for(int i = 0; i < descriptions.Count; i++) 
        {
            Actions.descriptions[keys[i]] = descriptions[i];
        }
    }

    private void ClearDescriptions()
    {
        Actions.descriptions.Clear();
    }

    #endregion
}
