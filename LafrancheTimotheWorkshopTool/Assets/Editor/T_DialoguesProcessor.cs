using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class T_DialoguesProcessor : AssetPostprocessor
{
    public static string[] dialogues;

    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets == null || importedAssets.Length == 0)
        {
            return;
        }

        for (int i = 0; i < importedAssets.Length; i++)
        {
            if (importedAssets[i].EndsWith(".tsv"))
            {
                string str = importedAssets[i];
                str = str.Substring(0, str.Length - 4);
                str += ".csv";
                File.Move(importedAssets[i], str);
                File.Move(importedAssets[i] + ".meta", str + ".meta");

                char oper = ';';
                string content = File.ReadAllText(str);
                content = content.Replace('\t', oper);
                File.WriteAllText(str, content);

                string[] lineContent = content.Split('\n');
                List<string> dialoguesList = new List<string>();

                for (int j = 1; j < lineContent.Length; j++)
                {
                    string[] cont = lineContent[j].Split(';');
                    dialoguesList.Add(cont[0]);
                }

                dialogues = dialoguesList.ToArray();
            }
        }

    }
}
