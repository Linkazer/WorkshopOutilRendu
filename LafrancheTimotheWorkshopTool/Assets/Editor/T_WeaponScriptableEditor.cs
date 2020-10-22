using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;

[CustomEditor(typeof(T_WeaponScriptable))]
public class T_WeaponScriptableEditor : Editor
{
    SerializedProperty lazerName;
    SerializedProperty lazerSpriteProperty;
    SerializedProperty attackSpeed, lazerSpeed, damage, bonusDamageByDifficulty;
    SerializedProperty burstNumber, burstDelay, lazerByBurst, angleBeetwenLazers;

    Sprite lazerSprite;

    T_ShipVisualisationWindow window;

    private void OnEnable()
    {
        lazerName = serializedObject.FindProperty(nameof(T_WeaponScriptable.nom));

        lazerSpriteProperty = serializedObject.FindProperty(nameof(T_WeaponScriptable.lazerSprite));
        
        lazerSpeed = serializedObject.FindProperty(nameof(T_WeaponScriptable.lazerSpeed));
        attackSpeed = serializedObject.FindProperty(nameof(T_WeaponScriptable.recoveryTime));
        damage = serializedObject.FindProperty(nameof(T_WeaponScriptable.damage));
        bonusDamageByDifficulty = serializedObject.FindProperty(nameof(T_WeaponScriptable.damageByDifficulty));
        
        burstDelay = serializedObject.FindProperty(nameof(T_WeaponScriptable.burstDelay));
        burstNumber = serializedObject.FindProperty(nameof(T_WeaponScriptable.burstNumber));
        lazerByBurst = serializedObject.FindProperty(nameof(T_WeaponScriptable.lazerByBurst));
        angleBeetwenLazers = serializedObject.FindProperty(nameof(T_WeaponScriptable.angleBeetwenLazers));

        lazerSprite = (target as T_WeaponScriptable).lazerSprite;
    }

     public override void OnInspectorGUI()
     {
        //base.OnInspectorGUI();

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        GUILayout.BeginVertical();

        float labelBaseWidth = EditorGUIUtility.labelWidth;

        var myLayout = new GUILayoutOption[] {
            
            };

        if (lazerSprite != null)
        {
            myLayout = new GUILayoutOption[] {
            GUILayout.Height(lazerSprite.rect.height)
            };
        }

        EditorGUILayout.PropertyField(lazerName);

        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(lazerSpriteProperty, myLayout);

        if(EditorGUI.EndChangeCheck())
        {
            lazerSprite = (target as T_WeaponScriptable).lazerSprite;
        }
        if (lazerSprite != null)
        {
            DrawOnGUISprite(lazerSprite);
        }

        GUILayout.EndHorizontal();

        #region Comportement de base du Lazer
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Comportement du lazer", EditorStyles.boldLabel);
        EditorGUIUtility.labelWidth /= 2;
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(lazerSpeed);
        EditorGUILayout.PropertyField(attackSpeed);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(damage);
        EditorGUILayout.PropertyField(bonusDamageByDifficulty);
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = labelBaseWidth;
        #endregion

        #region Tir en rafalle
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Tir en rafale", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(burstNumber);
        if(burstNumber.intValue < 1)
        {
            burstNumber.intValue = 1;
        }
        if(burstNumber.intValue > 1)
        {
            EditorGUILayout.PropertyField(burstDelay);
        }
        #endregion

        #region Multiples projectiles
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Projectiles multiples", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(lazerByBurst);
        if (lazerByBurst.intValue < 1)
        {
            lazerByBurst.intValue = 1;

        }
        else if (lazerByBurst.intValue > 1)
        {
            EditorGUILayout.PropertyField(angleBeetwenLazers);
            if (angleBeetwenLazers.intValue < 5)
            {
                angleBeetwenLazers.intValue = 5;
            }
        }
        else
        {
            angleBeetwenLazers.intValue = 0;
        }
        #endregion
        serializedObject.ApplyModifiedProperties();

        #region Gestion de la fenêtre

        if (GUILayout.Button("Show win"))
        {
            T_ShipVisualisationWindow.Create(target as T_WeaponScriptable, lazerSprite);
            window = (T_ShipVisualisationWindow)EditorWindow.GetWindow(typeof(T_ShipVisualisationWindow));
        }
        sw.Stop();

        if (window != null)
        {
            window.Repaint();
        }

        GUILayout.EndVertical();
        #endregion

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
}
