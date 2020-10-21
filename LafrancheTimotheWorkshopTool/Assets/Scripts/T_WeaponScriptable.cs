﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "GameScriptable/Create new Weapon", order = 0)]
public class T_WeaponScriptable : ScriptableObject
{
    public string nom;
    public Sprite lazerSprite;

    public float attackSpeed, lazerSpeed, damage, damageByDifficulty, zoneDamage, bonusByDifficulty, zoneRadius, burstDelay;
    public int burstNumber, lazerByBurst, angularAngleBeetwenLazers;
}
