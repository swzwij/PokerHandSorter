using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ManualCheck))]
public class ManualCheckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ManualCheck manualCheck = (ManualCheck)target;
        if(GUILayout.Button("Get Hand Type"))
        {
            manualCheck.CheckHand();
        }
        DrawDefaultInspector();
    }
}
