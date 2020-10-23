using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_ActivableObject
{
    static Vector2 startPos = new Vector2(12,12);

    public static void DisableObject(GameObject toDisable)
    {
        toDisable.transform.position = startPos;
        toDisable.SetActive(false);
    }

    public static void EnableObject(GameObject toEnable, Vector2 positionToEnable)
    {
        toEnable.transform.position = positionToEnable;

        toEnable.SetActive(true);
    }
}
