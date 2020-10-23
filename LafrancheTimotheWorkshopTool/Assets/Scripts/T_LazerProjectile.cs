using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_LazerProjectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    private bool isEnnemy;
    private float damage, bonusDamage;

    private Vector3 startPos;

    [SerializeField]
    private SpriteRenderer sprite = default;

    private void Start()
    {
        startPos = transform.position;
        gameObject.SetActive(false);
    }

    public void Initialise(T_WeaponScriptable newWeapon, T_ShipPlaytimeStatue shooter, Vector2 lazerDirection)
    {
        sprite.sprite = newWeapon.lazerSprite;
        direction = lazerDirection;
        speed = newWeapon.lazerSpeed;
        transform.position = shooter.transform.position;
        isEnnemy = !shooter.isPlayer;
        damage = newWeapon.damage;
        bonusDamage = newWeapon.damageByDifficulty;

        transform.up = direction;
        gameObject.SetActive(true);
    }

    public void DestroyLazer()
    {
        direction = Vector3.zero;
        speed = 0;
        transform.position = startPos;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;

        if(transform.position.x < -8.6f || transform.position.x > 8.6f || transform.position.y < -4.6f || transform.position.y > 4.6f)
        {
            DestroyLazer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<T_ShipPlaytimeStatue>()!=null)
        {
            if (collision.GetComponent<T_ShipPlaytimeStatue>().TakeDamage(damage, bonusDamage, isEnnemy))
            {
                DestroyLazer();
            }
        }
        else if(collision.tag == "Obstacle" || collision.tag == "MapBorder")
        {
            DestroyLazer();
        }
    }
}
