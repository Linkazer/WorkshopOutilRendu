using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "GameScriptable/Create weapon", order = 0)]
public class T_Weapon : ScriptableObject
{
    public string name = "";
    public int amount = 1;
    //public IngredientUnit unit;
}
