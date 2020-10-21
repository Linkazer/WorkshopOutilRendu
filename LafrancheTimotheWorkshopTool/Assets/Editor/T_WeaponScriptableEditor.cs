using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(T_WeaponScriptable))]
//[CustomPropertyDrawer(typeof(T_WeaponScriptable))]
public class T_WeaponScriptableEditor : Editor
{
    SerializedProperty lazerName;
    SerializedProperty lazerSpriteProperty;
    SerializedProperty attackSpeed, lazerSpeed, damage, bonusDamageByDifficulty;
    SerializedProperty zoneDamage, BonusByDifficulty, zoneRadius;
    SerializedProperty burstNumber, burstDelay, lazerByBurst, angleBeetwenLazers;

    Sprite lazerSprite;

    private void OnEnable()
    {
        lazerName = serializedObject.FindProperty(nameof(T_WeaponScriptable.nom));

        lazerSpriteProperty = serializedObject.FindProperty(nameof(T_WeaponScriptable.lazerSprite));
        
        lazerSpeed = serializedObject.FindProperty(nameof(T_WeaponScriptable.lazerSpeed));
        attackSpeed = serializedObject.FindProperty(nameof(T_WeaponScriptable.attackSpeed));
        damage = serializedObject.FindProperty(nameof(T_WeaponScriptable.damage));
        bonusDamageByDifficulty = serializedObject.FindProperty(nameof(T_WeaponScriptable.damageByDifficulty));
        zoneDamage = serializedObject.FindProperty(nameof(T_WeaponScriptable.zoneDamage));
        BonusByDifficulty = serializedObject.FindProperty(nameof(T_WeaponScriptable.bonusByDifficulty));
        zoneRadius = serializedObject.FindProperty(nameof(T_WeaponScriptable.zoneRadius));
        
        burstDelay = serializedObject.FindProperty(nameof(T_WeaponScriptable.burstDelay));
        burstNumber = serializedObject.FindProperty(nameof(T_WeaponScriptable.burstNumber));
        lazerByBurst = serializedObject.FindProperty(nameof(T_WeaponScriptable.lazerByBurst));
        angleBeetwenLazers = serializedObject.FindProperty(nameof(T_WeaponScriptable.angleBeetwenLazers));

        lazerSprite = (target as T_WeaponScriptable).lazerSprite;
    }

    /*public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Debug.Log("Allo");
        float neededLignes = 2;

        if (EditorGUIUtility.currentViewWidth < 332)
        {
            neededLignes++;
        }

        return 32;// neededLignes * EditorGUIUtility.singleLineHeight + 1;
    }*/

     public override void OnInspectorGUI()
     {
        base.OnInspectorGUI();

        GUILayout.BeginVertical();

        float labelBaseWidth = EditorGUIUtility.labelWidth;

        var myLayout = new GUILayoutOption[] {
            GUILayout.Height(lazerSprite.rect.height)
        };

        Rect spriteRect = new Rect(20, 20, 0.9f * EditorGUIUtility.currentViewWidth, lazerSprite.rect.height);

        EditorGUILayout.PropertyField(lazerName);

        //var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(lazerSpriteProperty, myLayout);

        if(EditorGUI.EndChangeCheck())
        {
            lazerSprite = (target as T_WeaponScriptable).lazerSprite;
        }
        DrawOnGUISprite(lazerSprite);

        GUILayout.EndHorizontal();

        //Comportement de base du Lazer
        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();

        //Dégâts de zone
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dégâts de zone", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(zoneDamage);
        if (zoneDamage.floatValue > 0)
        {
            EditorGUIUtility.labelWidth /= 2;
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(zoneDamage);
            EditorGUILayout.PropertyField(BonusByDifficulty);
            GUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = labelBaseWidth;
        }

        //Tir en rafalle
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

        //Multiples projectiles
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Projectiles multiples", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(lazerByBurst);
        if (lazerByBurst.intValue < 1)
        {
            lazerByBurst.intValue = 1;
        }
        if (lazerByBurst.intValue > 1)
        {
            EditorGUILayout.PropertyField(angleBeetwenLazers);
            if (angleBeetwenLazers.intValue < 5)
            {
                angleBeetwenLazers.intValue = 5;
            }
        }



        serializedObject.ApplyModifiedProperties();

        if(GUILayout.Button("Show win"))
        {
            T_ShipVisualisationWindow.Create(target as T_WeaponScriptable, lazerSprite);
        }

        GUILayout.EndVertical();
    }



    /*public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log(property);
        Rect colorRect = new Rect(position.x, position.y, position.width / 2, EditorGUIUtility.singleLineHeight);

        SerializedProperty colorProper = property.FindPropertyRelative(nameof(T_WeaponScriptable.lazerSprite));
        EditorGUI.PropertyField(colorRect, colorProper);
        //DrawOnGUISprite(lazerSprite);
    }*/

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
            //GUI.DrawTexture(rect, tex);
        }
    }
}
