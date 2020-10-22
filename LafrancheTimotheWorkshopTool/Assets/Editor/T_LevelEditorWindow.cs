using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class T_LevelEditorWindow : EditorWindow
{
    T_LevelScriptable currentLevel;

    static T_LevelEditorWindow wind;

    static int windowSizeX = 1010, windowSizeY = 660;

    Color currFeedbackColor = Color.white;

    LevelObject currentObject = LevelObject.None;

    public static void Init()
    {
        wind = (T_LevelEditorWindow)GetWindow(typeof(T_LevelEditorWindow), false, "Level Editor");

        // Initialise window
        wind.maxSize = new Vector2(windowSizeX, windowSizeY);
        wind.minSize = wind.maxSize;

        wind.Show();
    }

    public static void InitWithContent(T_LevelScriptable profile)
    {
        wind = (T_LevelEditorWindow)GetWindow(typeof(T_LevelEditorWindow), false, "Level Editor");

        // Initialise window
        wind.maxSize = new Vector2(windowSizeX, windowSizeY);
        wind.minSize = wind.maxSize;

        wind.currentLevel = profile;

        wind.Show();
    }

    private void OnGUI()
    {
        if (currentLevel == null)
        {
            EditorGUILayout.LabelField("That's not how it works here");
            return;
        }

        Event currentEvt = Event.current;

        Rect noneButton = new Rect(20, 20, windowSizeX / 3.2f, 50);
        Rect obstacleButton = new Rect(30 + windowSizeX / 3.2f, 20, windowSizeX / 3.2f, 50);
        Rect ennemyButton = new Rect(40 + 2* windowSizeX / 3.2f, 20, windowSizeX / 3.2f, 50);



        if(GUI.Button(noneButton, "Nothing"))
        {
            currFeedbackColor = Color.white;
            currentObject = LevelObject.None;
        }
        if(GUI.Button(obstacleButton, "Obstacles"))
        {
            currFeedbackColor = Color.blue;
            currentObject = LevelObject.Obstacle;
        }
        if(GUI.Button(ennemyButton, "Ennemies"))
        {
            currFeedbackColor = Color.red;
            currentObject = LevelObject.Ennemy;
        }


        if(currentLevel.levelContainer.Length < 180)
        {
            currentLevel.levelContainer = new LevelObject[180];
        }

        float placementX = 50, placementY = 50, placementSpace = 3;

        for (int i = 0; i < currentLevel.levelContainer.Length; i++)
        {
            Rect placementRect = new Rect(20 + (placementSpace + placementX) * (i % 18), 100 + (placementSpace + placementY) * (Mathf.RoundToInt(i / 18)), placementX, placementY);

            if (placementRect.Contains(currentEvt.mousePosition))
            {
                EditorGUI.DrawRect(placementRect, currFeedbackColor);
                if (currentEvt.type == EventType.MouseDown && currentEvt.button == 0)
                {
                    currentLevel.levelContainer[i] = currentObject;
                }
            }
            else
            {
                switch(currentLevel.levelContainer[i])
                {
                    case LevelObject.None:
                        EditorGUI.DrawRect(placementRect, Color.white); 
                        break;
                    case LevelObject.Obstacle:
                        EditorGUI.DrawRect(placementRect, Color.blue); 
                        break;
                    case LevelObject.Ennemy:
                        EditorGUI.DrawRect(placementRect, Color.red); 
                        break;
                }
            }
            Repaint();
        }
    }
}
