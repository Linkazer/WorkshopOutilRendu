using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(T_ShootManager))]
public class T_ShootManagerEditor : Editor
{
    T_ShootManager shootManager;

    SerializedProperty lazerList;

    private void OnEnable()
    {
        lazerList = serializedObject.FindProperty(nameof(T_ShootManager.allLazersLocal));
        shootManager = target as T_ShootManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add 10 lazers"))
        {
            for(int i = 0; i < 10; i ++)
            {
                shootManager.AddLazerInScene();
            }
        }

        if (GUILayout.Button("Remove 10 lazers"))
        {
            for (int i = 0; i < 10; i++)
            {
                shootManager.RemoveLazerInScene();
            }
        }
        GUILayout.EndHorizontal();
    }
}
