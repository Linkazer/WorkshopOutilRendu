using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_ShowLevel : MonoBehaviour
{
    public T_LevelScriptable levelToShow;

    public GameObject obstacle, ship;

    private List<GameObject> currentLevel = new List<GameObject>();

    private void Awake()
    {
        HideLevelAtStart();
    }

    public void ShowLevel()
    {
        if (levelToShow != null)
        {
            for (int i = 0; i < levelToShow.levelContainer.Length; i++)
            {
                Vector2 newPosition = new Vector2(-8.5f, -4.5f) + new Vector2(i % 18, Mathf.RoundToInt(i / 18));
                switch (levelToShow.levelContainer[i])
                {
                    case LevelObject.Obstacle:
                        Instantiate(obstacle, newPosition, Quaternion.identity, transform);
                        break;
                    case LevelObject.Ennemy:
                        Instantiate(ship, newPosition, Quaternion.identity, transform);
                        break;
                }
            }
        }
    }

    public void HideLevel()
    {
        while(transform.childCount>0)
        {
            DestroyImmediate(transform.GetChild(transform.childCount-1).gameObject);
        }
    }

    public void HideLevelAtStart() //Au lieu de détruire les objets au Start, on désactive le parent. Cela permet de récupérer l'affichage après la partie.
    {
        gameObject.SetActive(false);
    }
}
