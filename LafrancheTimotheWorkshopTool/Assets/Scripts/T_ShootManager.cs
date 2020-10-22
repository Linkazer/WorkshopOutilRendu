using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShootingEventClass : UnityEvent<T_ShipPlaytimeStatue> { }

public class T_ShootManager : MonoBehaviour
{
    public GameObject lazerPrefab, projectileStock;

    public static List<T_LazerProjectile> allLazers;
    [HideInInspector]
    public List<T_LazerProjectile> allLazersLocal;

    private static ShootingEventClass shotEvent = new ShootingEventClass();

    private void Start()
    {
        allLazers = allLazersLocal;
        shotEvent.AddListener(ShootingEvent);
    }

    public static void Shoot(T_ShipPlaytimeStatue shooter)
    {
        shotEvent.Invoke(shooter);
    }

    void ShootingEvent(T_ShipPlaytimeStatue shooter)
    {
        StartCoroutine(ShootLazer(shooter));
    }

    public void AddLazerInScene()
    {
        T_LazerProjectile newLazer = Instantiate(lazerPrefab, new Vector3(10, 10, 0), Quaternion.identity, projectileStock.transform).GetComponent<T_LazerProjectile>();
        allLazersLocal.Add(newLazer);
    }

    public void RemoveLazerInScene()
    {
        if (allLazersLocal.Count > 0)
        {
            T_LazerProjectile newLazer = allLazersLocal[allLazersLocal.Count - 1];
            allLazersLocal.RemoveAt(allLazersLocal.Count - 1);
            DestroyImmediate(newLazer.gameObject);
        }
    }

    IEnumerator ShootLazer(T_ShipPlaytimeStatue shooter)
    {
        for (int burst = 0; burst < shooter.weapon.burstNumber; burst++)
        {
            List<Vector2> newTargets = ProjectileDirections(shooter.weapon.lazerByBurst, shooter.weapon.angleBeetwenLazers, shooter.shotDirection);

            for (int j = 0; j < newTargets.Count; j++)
            {
                for (int i = 0; i < allLazers.Count; i++)
                {
                    if (!allLazers[i].gameObject.activeSelf)
                    {
                        allLazers[i].Initialise(shooter.weapon, shooter,newTargets[j]);
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(shooter.weapon.burstDelay);
        }
    }

    List<Vector2> ProjectileDirections(int nbProjectile, int angle, Vector2 startDirection)
    {
        List<Vector2> projectiles = new List<Vector2>();

        Vector2 newTraj = startDirection;
        Vector2 vecToAdd = newTraj;

        if (nbProjectile % 2 == 0)
        {
            vecToAdd = Quaternion.Euler(0, 0, -angle / 2) * newTraj;
        }

        projectiles.Add(vecToAdd);

        for (int i = 2; i <= nbProjectile; i++)
        {

            if (i % 2 == 0)
            {
                vecToAdd = Quaternion.Euler(0, 0, angle * (int)(i / 2)) * projectiles[0];

            }
            else
            {
                vecToAdd = Quaternion.Euler(0, 0, -angle * (int)(i / 2)) * projectiles[0];
            }
            projectiles.Add(vecToAdd);
        }

        return new List<Vector2>(projectiles);
    }
}
