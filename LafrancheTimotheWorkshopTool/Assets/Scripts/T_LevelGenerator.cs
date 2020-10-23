using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_LevelGenerator : MonoBehaviour
{
    private List<Transform> activesObjects = new List<Transform>();

    [HideInInspector]
    public List<GameObject> obstacles = new List<GameObject>(), enemies = new List<GameObject>();

    [SerializeField]
    private GameObject obstaclePrefab = default, enemyPrefab = default, stockObjects = default;

    [SerializeField]
    private List<T_LevelScriptable> levels = new List<T_LevelScriptable>();

    Vector2 spawnPosition = new Vector2(10, -4.5f);

    [SerializeField]
    private float scrollingSpeed = -2;
    private float scrollSpeed = 0;
    private float newLevelTimer = 0;

    /*private void Start()
    {
        GenerateNextLevel();
    }*/

    public void NewGame()
    {
        scrollSpeed = scrollingSpeed;
        GenerateNextLevel();
    }

    private void Update()
    {
        if (scrollSpeed != 0)
        {
            newLevelTimer += Time.deltaTime;

            if (activesObjects.Count > 0)
            {
                for (int i = 0; i < activesObjects.Count; i++)
                {
                    if (activesObjects[i].position.x < -10 || !activesObjects[i].gameObject.activeSelf)
                    {
                        T_ActivableObject.DisableObject(activesObjects[i].gameObject);
                        activesObjects.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        activesObjects[i].position += new Vector3(scrollSpeed * (1+T_ScoreManager.instance.currentDifficulty/5) * Time.deltaTime, 0, 0);
                    }
                }
            }

            if (newLevelTimer > Mathf.Abs(18 / scrollSpeed))
            {
                GenerateNextLevel();
                newLevelTimer = 0;
            }
        }
    }

    void GenerateNextLevel()
    {
        int lvlIndex = Random.Range(0, levels.Count);
        for(int j = 0; j < levels[lvlIndex].levelContainer.Length; j++)
        {
            int index = -1;
            Vector2 newPosition = spawnPosition + new Vector2(j % 18, Mathf.RoundToInt(j / 18));
            switch(levels[lvlIndex].levelContainer[j])
            {
                case LevelObject.Obstacle:
                    index = SearchForUnusedObject(obstacles);
                    if(index>=0)
                    {
                        T_ActivableObject.EnableObject(obstacles[index], newPosition);
                        activesObjects.Add(obstacles[index].transform);
                    }
                    break;
                case LevelObject.Ennemy:
                    index = SearchForUnusedObject(enemies);
                    if (index >= 0)
                    {
                        T_ActivableObject.EnableObject(enemies[index], newPosition);
                        enemies[index].GetComponent<T_EnemyBehavior>().SetEnnemy(levels[lvlIndex].enemyType);
                        activesObjects.Add(enemies[index].transform);
                    }
                    break;
            }
        }
    }

    public void ResetAll()
    {
        newLevelTimer = 0;
        scrollSpeed = 0;
        for (int i = 0; i < activesObjects.Count; i++)
        {
            T_ActivableObject.DisableObject(activesObjects[i].gameObject);
            activesObjects.RemoveAt(i);
            i--;
        }
    }

    int SearchForUnusedObject(List<GameObject> listToSearch)
    {
        for(int l = 0; l < listToSearch.Count; l++)
        {
            if(!listToSearch[l].activeSelf)
            {
                return l;
            }
        }
        return -1;
    }

    public void AddObstaclesInScene()
    {
        GameObject newObject = Instantiate(obstaclePrefab, new Vector3(10, 10, 0), Quaternion.identity, stockObjects.transform);
        T_ActivableObject.DisableObject(newObject);
        obstacles.Add(newObject);
    }

    public void RemoveObstaclesInScene()
    {
        if (obstacles.Count > 0)
        {
            GameObject newObject = obstacles[obstacles.Count - 1];
            obstacles.RemoveAt(obstacles.Count - 1);
            DestroyImmediate(newObject);
        }
    }

    public void AddEnemyInScene()
    {
        GameObject newObject = Instantiate(enemyPrefab, new Vector3(10, 10, 0), Quaternion.identity, stockObjects.transform);
        T_ActivableObject.DisableObject(newObject);
        enemies.Add(newObject);
    }

    public void RemoveEnemyInScene()
    {
        if (enemies.Count > 0)
        {
            GameObject newObject = enemies[enemies.Count - 1];
            enemies.RemoveAt(enemies.Count - 1);
            DestroyImmediate(newObject);
        }
    }
}
