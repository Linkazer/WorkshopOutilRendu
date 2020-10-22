using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_ShipPlaytimeStatue : MonoBehaviour
{
    private float health, maxHealth;
    public bool isPlayer;

    [HideInInspector]
    public Vector2 shotDirection;

    public T_EnnemyScriptable baseShip;
    public T_WeaponScriptable weapon;

    private float currentCooldown;

    [SerializeField]
    private Image healthBar;

    private void Start()
    {
        maxHealth = baseShip.hitPoints;
        health = maxHealth;
        currentCooldown = weapon.recoveryTime;
    }

    private void Update()
    {
        shotDirection = transform.up.normalized;
        currentCooldown += Time.deltaTime;
    }

    public bool TakeDamage(float dmg, bool isEnnemy)
    {
        Debug.Log(dmg);
        if(isEnnemy == isPlayer)
        {
            health -= dmg;
            if(health<=0)
            {
                Die();
            }
            if(healthBar != null)
            {
                healthBar.fillAmount = (maxHealth-health) / maxHealth;
            }
            return true;
        }
        return false;
    }

    private void Die()
    {
        if(isPlayer)
        {
            T_ScoreManager.instance.EndGame();
        }
        else
        {
            Debug.Log(T_ScoreManager.instance);
            T_ScoreManager.instance.AddScore(baseShip.score);
            gameObject.SetActive(false);
        }
    }

    public void TryToShoot()
    {
        if (currentCooldown >= weapon.recoveryTime)
        {
            T_ShootManager.Shoot(this);
            currentCooldown = 0;
        }
    }

    public bool IsCooldownReady()
    {
        return (currentCooldown >= weapon.recoveryTime);
    }
}
