using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_LazerProjectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    private bool isEnnemy;
    private float damage;

    private Vector3 startPos;

    [SerializeField]
    private SpriteRenderer sprite;

    private void Start()
    {
        startPos = transform.position;
        gameObject.SetActive(false);
    }

    public void Initialise(T_WeaponScriptable newWeapon, T_ShipPlaytimeStatue shooter)
    {
        sprite.sprite = newWeapon.lazerSprite;
        direction = shooter.shotDirection;
        speed = newWeapon.lazerSpeed;
        transform.position = shooter.transform.position;
        isEnnemy = !shooter.isPlayer;

        transform.up = direction;
        gameObject.SetActive(true);
    }

    public void DestroyLazer(Vector3 position)
    {
        direction = Vector3.zero;
        speed = 0;
        transform.position = position;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<T_ShipPlaytimeStatue>()!=null)
        {
            collision.GetComponent<T_ShipPlaytimeStatue>().TakeDamage(damage, isEnnemy);
            DestroyLazer(startPos);
        }
        else if(collision.tag == "Obstacle" || collision.tag == "MapBorder")
        {
            DestroyLazer(startPos);
        }
    }
}
