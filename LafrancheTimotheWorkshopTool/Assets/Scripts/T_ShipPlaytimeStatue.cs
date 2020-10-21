using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_ShipPlaytimeStatue : MonoBehaviour
{
    public float health;
    public bool isPlayer;

    public Vector2 shotDirection;

    public T_WeaponScriptable weapon;

    private void Update()
    {
        shotDirection = transform.up.normalized;
    }

    public void TakeDamage(float dmg, bool isEnnemy)
    {
        if(isEnnemy == isPlayer)
        {
            health -= dmg;
        }
    }

    public void TryToShoot()
    {
        T_ShootManager.Shoot(this);
    }
}
