using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_DialogueSimulation : MonoBehaviour
{
    public static T_DialogueSimulation instance;

    public TextAsset dialogueAsset;

    [SerializeField]
    private Text dialogBox;

    private string[] dialogList;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        dialogList = dialogueAsset.ToString().Split('\n');
    }

    public void ShowMessage()
    {
        dialogBox.text = dialogList[Random.Range(1, dialogList.Length)]; //On saute la première ligne car elle est là pour le nom des catégories
    }
}
