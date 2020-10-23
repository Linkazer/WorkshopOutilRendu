using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(T_ShowLevel))]
public class T_ShowLevelEditor : Editor
{
    private void OnEnable()
    { 

    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();


        if (EditorGUI.EndChangeCheck())
        {
            (target as T_ShowLevel).HideLevel();
            (target as T_ShowLevel).ShowLevel();
            Debug.Log("something changed");
        }

        EditorGUI.EndChangeCheck();
    }
}
