using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(T_LevelGenerator))]
public class T_LevelGeneratorEditor : Editor
{
    T_LevelGenerator shootManager;

    SerializedProperty obstaclesList, enemiesList;

    private void OnEnable()
    {
        obstaclesList = serializedObject.FindProperty(nameof(T_LevelGenerator.obstacles));
        enemiesList = serializedObject.FindProperty(nameof(T_LevelGenerator.enemies));
        shootManager = target as T_LevelGenerator;

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add 10 obstacles"))
        {
            for (int i = 0; i < 10; i++)
            {
                shootManager.AddObstaclesInScene();
            }
        }

        if (GUILayout.Button("Remove 10 obstacles"))
        {
            for (int i = 0; i < 10; i++)
            {
                shootManager.RemoveObstaclesInScene();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add 10 enemies"))
        {
            for (int i = 0; i < 10; i++)
            {
                shootManager.AddEnemyInScene();
            }
        }

        if (GUILayout.Button("Remove 10 enemies"))
        {
            for (int i = 0; i < 10; i++)
            {
                shootManager.RemoveEnemyInScene();
            }
        }
        GUILayout.EndHorizontal();
    }
}
