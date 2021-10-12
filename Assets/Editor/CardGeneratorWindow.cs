using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardGeneratorWindow : EditorWindow
{
    GUIStyle titleText;
    GUIStyle outlineStyle;
    GUIStyle nameStyle;
    GUIStyle typeStyle;
    GUIStyle numberStyle;
        
    string cardName;
    int cost;
    int life;
    int attack;
    Texture image;
    CreatureType creatureType;


[MenuItem("CustomWindow/CardGenerator")]
    static void Init()
    {
        CardGeneratorWindow window = (CardGeneratorWindow)EditorWindow.GetWindow(typeof(CardGeneratorWindow));
        
        window.minSize = new Vector2(400, 500);
        window.maxSize = new Vector2(400, 500);

        window.Show();
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

        EditorGUILayout.LabelField("Card generator", titleText);
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

        cardName = EditorGUILayout.TextField("Name", cardName);
        creatureType = (CreatureType)EditorGUILayout.EnumPopup("Creature type", creatureType);
        image = (Texture)EditorGUILayout.ObjectField("Image", image, typeof(Texture2D), false);

        GUILayout.BeginArea(cardRect);
        Rect curr = new Rect(0,0, cardRect.width, cardRect.height);
        Rect imageRect = new Rect(cardRect.width * 0.2f, cardRect.height * 0.12f , cardRect.width * 0.6f , cardRect.height * 0.6f);

        if (image != null)
            GUI.DrawTexture(imageRect, (Texture)Resources.Load("Cards/" + image.name));
        GUI.DrawTexture(curr, (Texture)Resources.Load("EmptyCard"));

        nameStyle = new GUIStyle(EditorStyles.boldLabel);
        //nameStyle.normal.textColor = Color.white;
        nameStyle.alignment = TextAnchor.MiddleCenter;
        nameStyle.fontSize = 15;

        typeStyle = new GUIStyle(EditorStyles.boldLabel);
        typeStyle.alignment = TextAnchor.MiddleCenter;
        typeStyle.fontSize = 13;

        GUI.Label(new Rect(0, cardRect.height * 0.45f, cardRect.width, cardRect.height * 0.2f), cardName, nameStyle);

        GUI.Label(new Rect(0, cardRect.height * 0.6f, cardRect.width, cardRect.height * 0.2f), creatureType.ToString(), typeStyle);

        numberStyle = new GUIStyle(EditorStyles.label);
        numberStyle.alignment = TextAnchor.MiddleCenter;
        numberStyle.fontStyle = FontStyle.Bold;
        numberStyle.fontSize = Mathf.RoundToInt(Screen.width * 0.07f);
        numberStyle.normal.textColor = Color.white;


        cost = ModifyStat(cost, new Rect(0,cardRect.height * 0.1f,cardRect.width * 0.3f, cardRect.height * 0.3f));
        
        attack = ModifyStat(attack, new Rect(cardRect.width * 0.01f, cardRect.height * 0.86f, cardRect.width * 0.3f, cardRect.height * 0.3f));

        life = ModifyStat(life, new Rect(cardRect.width * 0.72f, cardRect.height * 0.86f, cardRect.width * 0.3f, cardRect.height * 0.3f));

        GUILayout.EndArea();

        GUILayout.Space(cardRect.height);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("GenerateCard", GUILayout.MinWidth(150)))
        {
            TryCreateCard();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
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

    void TryCreateCard()
    {
        var cards = AssetDatabase.FindAssets("t:CardScriptable");

        foreach (var item in cards)
        {
            if (AssetDatabase.GUIDToAssetPath(item) == "Assets/Scriptables/" + cardName + ".asset")
            {
                PopUpWindow.OpenWindow(cardName, CreateCard);
                return;
            }
        }
        CreateCard();
    }

    void CreateCard()
    {
        CardScriptable newCard = (CardScriptable)CreateInstance("CardScriptable");

        newCard.cardName = cardName;
        newCard.cost = cost;
        newCard.attack = attack;
        newCard.life = life;
        newCard.image = image;
        newCard.creatureType = creatureType;

        AssetDatabase.CreateAsset(newCard, "Assets/Scriptables/" + cardName + ".asset");
    }

}
