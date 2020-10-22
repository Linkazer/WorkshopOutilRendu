using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelObject { None, Obstacle, Ennemy}

[CreateAssetMenu(fileName = "New Level", menuName = "GameScriptable/Create new Level", order = 0)]
public class T_LevelScriptable : ScriptableObject
{
    [HideInInspector]
    public LevelObject[] levelContainer;

    public T_EnnemyScriptable enemyType;
}
