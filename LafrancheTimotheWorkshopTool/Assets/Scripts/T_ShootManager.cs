using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_ShootManager : MonoBehaviour
{
    public GameObject lazerPrefab, projectileStock;

    public static List<T_LazerProjectile> allLazers;
    public  List<T_LazerProjectile> allLazersLocal;

    private void Start()
    {
        allLazers = allLazersLocal;
    }

    public static void Shoot(T_ShipPlaytimeStatue shooter)
    {
        for(int i = 0; i < allLazers.Count; i++)
        {
            if(!allLazers[i].gameObject.activeSelf)
            {
                allLazers[i].Initialise(shooter.weapon, shooter);
                break;
            }
        }
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
}
