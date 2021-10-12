using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class PopUpWindow : EditorWindow
{
    Action CreateCard;

    string cardName;


    public static void OpenWindow(string _name, Action _createCard) //En nuestras funciones static podemos pedir variables por parámetro
    {
        var window = (PopUpWindow)GetWindow(typeof(PopUpWindow));

        window.minSize = new Vector2(400, 120);
        window.maxSize = new Vector2(400, 120);

        window.cardName = _name;
        window.CreateCard = _createCard;
        window.Show();
    }

    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle();
        textStyle.alignment = TextAnchor.MiddleCenter;

        GUILayout.Space(10);
        EditorGUILayout.LabelField(" The card " + cardName + " already exists. Do you want to overwrite it?");

        GUILayout.Space(25);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUI.Button(GUILayoutUtility.GetRect(120, 120, 30, 30), "No"))
            Close(); //Función para cerrar la ventana

        GUILayout.FlexibleSpace();
        if (GUI.Button(GUILayoutUtility.GetRect(120, 120, 30, 30), "Yes"))
        {
            CreateCard();
            Close(); //Función para cerrar la ventana
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
