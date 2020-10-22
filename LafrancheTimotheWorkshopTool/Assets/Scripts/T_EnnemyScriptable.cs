using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ennemy", menuName = "GameScriptable/Create new Ennemy")]
public class T_EnnemyScriptable : ScriptableObject
{
    public string nom;
    public Sprite shipSprite;
    public int hitPoints;
    public T_WeaponScriptable weapon;
    public float speed;
    public int score, difficultyScore;

    public List<Vector2> wayPoints;
}
