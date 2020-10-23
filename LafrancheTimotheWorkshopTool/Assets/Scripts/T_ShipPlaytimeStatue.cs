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
    [HideInInspector]
    public T_WeaponScriptable weapon;

    private float currentCooldown;

    [SerializeField]
    private Image healthBar = default;

    private void Start()
    {
        ResetValue();
    }

    public void ResetValue()
    {
        maxHealth = baseShip.hitPoints;
        health = maxHealth;
        weapon = baseShip.weapon;
        currentCooldown = weapon.recoveryTime; 
    }

    private void Update()
    {
        currentCooldown += Time.deltaTime;
        shotDirection = transform.up.normalized;
    }

    public bool TakeDamage(float dmg, float bonusDamage, bool isAlly)
    {
        if(isAlly == isPlayer)
        {
            health -= dmg + bonusDamage * T_ScoreManager.instance.currentDifficulty;
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
            T_DialogueSimulation.instance.ShowMessage();
            T_ScoreManager.instance.AddScore(baseShip.score,baseShip.difficultyScore);
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
