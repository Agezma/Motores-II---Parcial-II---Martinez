using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable/CardCollection", order = 0)]
public class CardScriptable: ScriptableObject 
{
    public string cardName;
    public int cost;
    public int life;
    public int attack;
    public Texture image;
    public CreatureType creatureType;
}

public enum CreatureType
{
    Human,
    Dinosaur,
    Beast,
    Robot,
    Monster
}
