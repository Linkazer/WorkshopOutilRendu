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
    SerializedProperty burstNumber, burstDelay, lazerByBurst, angularAngleBeetwenLazers;

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
        angularAngleBeetwenLazers = serializedObject.FindProperty(nameof(T_WeaponScriptable.angularAngleBeetwenLazers));

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

        //base.OnInspectorGUI();
        
        float labelBaseWidth = EditorGUIUtility.labelWidth;

        var myLayout = new GUILayoutOption[] {
      GUILayout.Height(lazerSprite.rect.height)
};
        //Rect nameRect = new Rect(EditorGUIUtility.currentViewWidth/10, 20, EditorGUIUtility.currentViewWidth/10*8, 25);
        Rect nameRect = new Rect(10, 20, 100, 25);

        EditorGUILayout.PropertyField(lazerName);

        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(lazerSpriteProperty, myLayout);

        if(EditorGUI.EndChangeCheck())
        {
            lazerSprite = (target as T_WeaponScriptable).lazerSprite;
        }
        DrawOnGUISprite(lazerSprite);
        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(zoneDamage);
        if (EditorGUILayout.Foldout((zoneDamage.floatValue>0), "Zone effect"))
        {
            EditorGUIUtility.labelWidth /= 2;
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(zoneDamage);
            EditorGUILayout.PropertyField(BonusByDifficulty);
            GUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = labelBaseWidth;
        }

        serializedObject.ApplyModifiedProperties();

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
