using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_ScoreManager : MonoBehaviour
{
    public static T_ScoreManager instance;

    private int score = 0;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Animator menuAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log(instance);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreTxt.text = score.ToString();
    }

    public void EndGame()
    {
        menuAnimation.Play("EndGame");
    }
}
