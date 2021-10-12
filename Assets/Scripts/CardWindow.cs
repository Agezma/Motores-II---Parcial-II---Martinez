using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardWindow : EditorWindow
{
    GUIStyle myStyle;
    GUIStyle titleText;
    GUIStyle nameStyle;
    GUIStyle numberStyle;
    GUIStyle typeStyle;

    CardScriptable cardScriptable;

    string cardName;
    int cost;
    int life;
    int attack;
    Texture image;
    CreatureType creatureType;


    public static void OpenWindow(CardScriptable card)
    {
        var myWindow = (CardWindow)GetWindow(typeof(CardWindow));

        var mySelf = GetWindow<CardWindow>();

        mySelf.minSize = new Vector2(400, 500);
        mySelf.maxSize = new Vector2(400, 500);
        mySelf.myStyle = new GUIStyle();
        mySelf.cardScriptable = card;

        mySelf.cardName = card.cardName;
        mySelf.cost = card.cost;
        mySelf.attack = card.attack;
        mySelf.life = card.life;
        mySelf.image = card.image;
        mySelf.creatureType = card.creatureType;

        myWindow.Show();
    }

    private void OnGUI()
    {
        WindowTitle();
        CardGenerator();
    }

    private void WindowTitle()
    {
        EditorGUILayout.Space();

        titleText = new GUIStyle();
        titleText.fontStyle = FontStyle.Bold;
        titleText.alignment = TextAnchor.MiddleCenter;
        titleText.fontSize = 20;

        EditorGUILayout.LabelField("Card editor", titleText);
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Space(25);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 3), Color.gray);
        GUILayout.Space(25);
        GUILayout.EndHorizontal();

        GUILayout.Space(3);
    }


    void CardGenerator()
    {
        Rect cardRect = new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.6f);

        cardScriptable.cardName = EditorGUILayout.TextField("Name", cardScriptable.cardName);
        cardScriptable.creatureType = (CreatureType)EditorGUILayout.EnumPopup("Creature type", cardScriptable.creatureType);
        cardScriptable.image = (Texture)EditorGUILayout.ObjectField("Image", cardScriptable.image, typeof(Texture2D), false);

        GUILayout.BeginArea(cardRect);
        Rect curr = new Rect(0, 0, cardRect.width, cardRect.height);
        Rect imageRect = new Rect(cardRect.width * 0.2f, cardRect.height * 0.12f, cardRect.width * 0.6f, cardRect.height * 0.6f);

        if (cardScriptable.image != null)
            GUI.DrawTexture(imageRect, (Texture)Resources.Load("Cards/" + cardScriptable.image.name));
        GUI.DrawTexture(curr, (Texture)Resources.Load("EmptyCard"));

        nameStyle = new GUIStyle(EditorStyles.boldLabel);
        //nameStyle.normal.textColor = Color.white;
        nameStyle.alignment = TextAnchor.MiddleCenter;
        nameStyle.fontSize = 15;

        typeStyle = new GUIStyle(EditorStyles.boldLabel);
        typeStyle.alignment = TextAnchor.MiddleCenter;
        typeStyle.fontSize = 13;

        GUI.Label(new Rect(0, cardRect.height * 0.45f, cardRect.width, cardRect.height * 0.2f), cardScriptable.cardName, nameStyle);

        GUI.Label(new Rect(0, cardRect.height * 0.6f, cardRect.width, cardRect.height * 0.2f), cardScriptable.creatureType.ToString(), typeStyle);

        numberStyle = new GUIStyle(EditorStyles.label);
        numberStyle.alignment = TextAnchor.MiddleCenter;
        numberStyle.fontStyle = FontStyle.Bold;
        numberStyle.fontSize = Mathf.RoundToInt(Screen.width * 0.07f);
        numberStyle.normal.textColor = Color.white;


        cardScriptable.cost = ModifyStat(cardScriptable.cost, new Rect(0, cardRect.height * 0.1f, cardRect.width * 0.3f, cardRect.height * 0.3f));

        cardScriptable.attack = ModifyStat(cardScriptable.attack, new Rect(cardRect.width * 0.01f, cardRect.height * 0.86f, cardRect.width * 0.3f, cardRect.height * 0.3f));

        cardScriptable.life = ModifyStat(cardScriptable.life, new Rect(cardRect.width * 0.72f, cardRect.height * 0.86f, cardRect.width * 0.3f, cardRect.height * 0.3f));

        GUILayout.EndArea();

        GUILayout.Space(cardRect.height);        
    }

    int ModifyStat(int stat, Rect parentRect)
    {
        GUILayout.BeginArea(parentRect);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("-"))
        {
            if (stat - 1 >= 0)
                stat--;
        }

        GUILayout.FlexibleSpace();
        GUILayout.Label(stat.ToString(), numberStyle);
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("+"))
        {
            stat++;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
        return stat;
    }

}
