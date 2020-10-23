using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class T_EnemyBehavior : MonoBehaviour
{
    private T_EnnemyScriptable baseStatue;

    [SerializeField]
    private T_ShipPlaytimeStatue playtimeStatue = default;

    [SerializeField]
    private int wayIndex = 0;

    [SerializeField]
    private Vector3 localPos = Vector3.zero, direction = Vector3.zero, nextTarget = Vector3.zero;

    public void SetEnnemy(T_EnnemyScriptable newStatue)
    {
        GetComponent<SpriteRenderer>().sprite = newStatue.shipSprite;
        baseStatue = newStatue;
        playtimeStatue.baseShip = newStatue;
        ShipReset();
        transform.up = new Vector3(-1,0,0);
    }

    public void ShipReset()
    {
        playtimeStatue.ResetValue();
        wayIndex = 0;
        localPos = Vector3.zero;
        direction = Vector3.zero;
        nextTarget = Vector3.zero;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            playtimeStatue.TakeDamage(99, 99, false);
        }

        if (transform.position.x < 8.5f)
        {
            if (Vector2.Distance(localPos, nextTarget) < 0.1f)
            {
                wayIndex++;
                wayIndex %= baseStatue.wayPoints.Count;
                nextTarget = baseStatue.wayPoints[wayIndex];
            }

            Debug.Log("Tarace");


            direction = (nextTarget - localPos).normalized;

            localPos += direction * baseStatue.speed * Time.deltaTime;

            transform.position += direction * baseStatue.speed * Time.deltaTime;
            if (playtimeStatue.IsCooldownReady())
            {
                playtimeStatue.TryToShoot();
            }
        }
    }
}
