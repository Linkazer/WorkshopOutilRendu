using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_EnemyBehavior : MonoBehaviour
{
    private T_EnnemyScriptable baseStatue;

    [SerializeField]
    private T_ShipPlaytimeStatue playtimeStatue;

    private int wayIndex = 0;

    private Vector3 localPos = Vector3.zero, direction = Vector3.zero, nextTarget = Vector3.zero;

    private void Start()
    {
        SetEnnemy(baseStatue);
    }

    public void SetEnnemy(T_EnnemyScriptable newStatue)
    {
        GetComponent<SpriteRenderer>().sprite = newStatue.shipSprite;
        baseStatue = newStatue;
        playtimeStatue.baseShip = newStatue;
        wayIndex = 0;
        nextTarget = baseStatue.wayPoints[wayIndex];
        localPos = Vector3.zero;
        transform.up = new Vector3(-1,0,0);
    }

    private void Update()
    {
        if (playtimeStatue.IsCooldownReady() && transform.position.x < 8.4f)
        {
            if (Vector2.Distance(localPos, baseStatue.wayPoints[wayIndex]) < 0.1f)
            {
                wayIndex++;
                wayIndex %= baseStatue.wayPoints.Count;
                nextTarget = baseStatue.wayPoints[wayIndex];
            }

            direction = (nextTarget - localPos).normalized;

            localPos += direction * baseStatue.speed * Time.deltaTime;

            transform.position += direction * baseStatue.speed * Time.deltaTime;

            playtimeStatue.TryToShoot();
        }
    }
}
