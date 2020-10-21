using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(T_Weapon),true)]
public class T_WeaponScriptableDrawer : PropertyDrawer
{
    SerializedProperty lazerName;
    SerializedProperty lazerSpriteProperty;
    SerializedProperty attackSpeed, lazerSpeed, damage, bonusDamageByDifficulty;
    SerializedProperty zoneDamage, BonusByDifficulty, zoneRadius;
    SerializedProperty burstNumber, burstDelay, lazerByBurst, angularAngleBeetwenLazers;

    Sprite lazerSprite;

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

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log("Allo");
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);
        Debug.Log("Allo");
        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, 30, position.height);
        var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
        var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("amount"));
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    /*public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log(property);
        Rect colorRect = new Rect(position.x, position.y, position.width / 2, EditorGUIUtility.singleLineHeight);

        SerializedProperty colorProper = property.FindPropertyRelative(nameof(T_WeaponScriptable.lazerSprite));
        EditorGUI.PropertyField(colorRect, colorProper);
        //DrawOnGUISprite(lazerSprite);
    }*/
}
