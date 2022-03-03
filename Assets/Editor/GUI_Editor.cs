using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Data.SqlClient;

[CustomEditor(typeof(UISwitchSelector))]
public class GUI_Editor : Editor
{
    string[] m_BodyParts = new string[]{
        "Hair",
        "Eyebrow",
        "Head",
        "FacialHair"
    };


    public override void OnInspectorGUI()
    {
        UISwitchSelector myTarget = (UISwitchSelector)target;
        DrawDefaultInspector();

        myTarget.m_IndexOption = EditorGUILayout.Popup("Body Part", myTarget.m_IndexOption, m_BodyParts);
        // Update the selected choice in the underlying object
        myTarget.m_SelectedOption = m_BodyParts[myTarget.m_IndexOption];
    }
}
