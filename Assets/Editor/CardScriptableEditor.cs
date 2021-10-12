using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardScriptable))]
public class CardScriptableEditor : Editor
{
    CardScriptable card;
    GUIStyle numberStyle;
    private void OnEnable()
    {
        card = (CardScriptable)target;

    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Edit"))
        {
            CardWindow.OpenWindow(card);
        }
    }

}
