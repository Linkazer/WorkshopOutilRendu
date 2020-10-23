using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(T_EnnemyScriptable))]
public class T_EnemyScriptableEditor : Editor
{
    SerializedProperty shipName;
    SerializedProperty shipSprite;
    SerializedProperty hitPoints, speed, score, difficultyScore;
    SerializedProperty weapon;

    Sprite shipRealSprite;

    T_WeaponScriptable currentWeapon;

    T_EnnemyScriptable shipScriptable;

    Vector2 shipSpawnPosition = new Vector2(8.5f,0), shipBasePosition = new Vector2(0, 0);

    private void OnEnable()
    {
        SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));

        shipScriptable = (target as T_EnnemyScriptable);

        shipName = serializedObject.FindProperty(nameof(T_EnnemyScriptable.nom));
        shipSprite = serializedObject.FindProperty(nameof(T_EnnemyScriptable.shipSprite));
        hitPoints = serializedObject.FindProperty(nameof(T_EnnemyScriptable.hitPoints));
        speed = serializedObject.FindProperty(nameof(T_EnnemyScriptable.speed));
        score = serializedObject.FindProperty(nameof(T_EnnemyScriptable.score));
        difficultyScore = serializedObject.FindProperty(nameof(T_EnnemyScriptable.difficultyScore));
        weapon = serializedObject.FindProperty(nameof(T_EnnemyScriptable.weapon));

        shipRealSprite = (target as T_EnnemyScriptable).shipSprite;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        float labelBaseWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth /= 2;
        EditorGUILayout.PropertyField(shipName);

        #region Gestion du Sprite du vaisseau
        var myLayout = new GUILayoutOption[] {

            };

        if (shipRealSprite != null)
        {
            myLayout = new GUILayoutOption[] {
            GUILayout.Height(shipRealSprite.rect.height)
            };
        }

        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(shipSprite, myLayout);

        if (EditorGUI.EndChangeCheck())
        {
            shipRealSprite = (target as T_EnnemyScriptable).shipSprite;
        }
        if (shipRealSprite != null)
        {
            DrawOnGUISprite(shipRealSprite);
        }
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = labelBaseWidth;
        #endregion

        #region Comportement du vaisseau
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Comportement", EditorStyles.boldLabel);
        EditorGUIUtility.labelWidth /= 2;
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(hitPoints);
        EditorGUILayout.PropertyField(speed);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(score);
        EditorGUILayout.PropertyField(difficultyScore);
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = labelBaseWidth;
        #endregion

        #region Affichage de l'arme
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Arme", EditorStyles.boldLabel);
        EditorGUIUtility.labelWidth /= 3;
        EditorGUILayout.PropertyField(weapon);
        currentWeapon = (target as T_EnnemyScriptable).weapon;
        EditorGUIUtility.labelWidth = labelBaseWidth;
        if (currentWeapon != null)
        {
            EditorGUIUtility.labelWidth /= 2;
            GUILayout.BeginHorizontal(); //Speed / Attack speed / Dégâts / Burst / Nb Projectile
            EditorGUILayout.LabelField("Lazer speed : " + currentWeapon.lazerSpeed.ToString());
            EditorGUILayout.LabelField("Recovery time : " + currentWeapon.recoveryTime.ToString());
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Damage : " + currentWeapon.damage.ToString());
            EditorGUILayout.LabelField("Burst : " + currentWeapon.burstNumber.ToString());
            EditorGUILayout.LabelField("Projectiles : " + currentWeapon.lazerByBurst.ToString());
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Ajout et retrait de Waypoint
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Waypoint"))
        {
            shipScriptable.wayPoints.Add(Vector2.zero);
        }
        if (GUILayout.Button("Remove Waypoint"))
        {
            shipScriptable.wayPoints.RemoveAt(shipScriptable.wayPoints.Count - 1);
        }
        GUILayout.EndHorizontal();

        if (shipScriptable.wayPoints.Count <= 0)
        {
            shipScriptable.wayPoints.Add(Vector2.zero);
        }
        #endregion

        serializedObject.ApplyModifiedProperties();
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= (SceneView.OnSceneFunc)Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));
    }

    void DrawOnGUISprite(Sprite aSprite)
    {
        Rect c = aSprite.rect;
        float spriteW = c.width;
        float spriteH = c.height;
        Rect rect = GUILayoutUtility.GetRect(spriteW, spriteH);
        rect.width = spriteW;
        rect.height = spriteH;
        if (Event.current.type == EventType.Repaint)
        {
            var tex = aSprite.texture;
            c.xMin /= tex.width;
            c.xMax /= tex.width;
            c.yMin /= tex.height;
            c.yMax /= tex.height;
            GUI.DrawTextureWithTexCoords(rect, tex, c);
        }
    }

    void CustomOnSceneGUI(SceneView sceneview)
    {
        for (int i = 0; i < shipScriptable.wayPoints.Count; i++)
        {
            if(i==0)
            {
                Handles.color = Color.yellow;
                shipScriptable.wayPoints[i] = Handles.FreeMoveHandle(shipScriptable.wayPoints[i]+ shipBasePosition, Quaternion.identity, HandleUtility.GetHandleSize(shipBasePosition) * 0.5f, Vector3.one, Handles.CubeHandleCap) - (Vector3)shipBasePosition;
                shipSpawnPosition = shipScriptable.wayPoints[0];
                Handles.color = Color.white;
                Handles.DrawLine(shipScriptable.wayPoints[i] + shipBasePosition, shipSpawnPosition + shipBasePosition + shipScriptable.wayPoints[(i + 1) % shipScriptable.wayPoints.Count]);
            }
            else
            {
                shipScriptable.wayPoints[i] = Handles.FreeMoveHandle(shipScriptable.wayPoints[i] + shipSpawnPosition + shipBasePosition, Quaternion.identity, HandleUtility.GetHandleSize(shipSpawnPosition + shipBasePosition) * 0.5f, Vector3.one, Handles.CubeHandleCap) - ((Vector3)shipSpawnPosition+(Vector3)shipBasePosition);
                if ((i + 1) % shipScriptable.wayPoints.Count != 0)
                {
                    Handles.DrawLine(shipScriptable.wayPoints[i] + shipSpawnPosition + shipBasePosition, shipSpawnPosition + shipBasePosition + shipScriptable.wayPoints[(i + 1) % shipScriptable.wayPoints.Count]);
                }
                else
                {
                    Handles.DrawLine(shipScriptable.wayPoints[i] + shipSpawnPosition + shipBasePosition, shipBasePosition + shipScriptable.wayPoints[(i + 1) % shipScriptable.wayPoints.Count]);
                }
            }
        }
    }

    

    void AddWayPoint()
    {
        shipScriptable.wayPoints.Add(Vector2.zero);
    }

    void RemoveWayPoint()
    {
        shipScriptable.wayPoints.RemoveAt(shipScriptable.wayPoints.Count-1);
    }
}
