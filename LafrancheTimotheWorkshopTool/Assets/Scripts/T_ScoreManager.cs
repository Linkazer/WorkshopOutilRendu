using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_ScoreManager : MonoBehaviour
{
    public static T_ScoreManager instance;
    [SerializeField]
    private T_LevelGenerator levelGenerator = default;
    [SerializeField]
    private T_ShipPlaytimeStatue playerShip = default;

    private int score = 0;

    public int currentDifficulty;

    [SerializeField]
    private Text scoreTxtInGame = default, scoreTxtEndGame = default;

    [SerializeField]
    private GameObject gameUi = default, menuUi = default;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddScore(int amount, int amountBonus)
    {
        score += amount + amountBonus*currentDifficulty;
        scoreTxtInGame.text = score.ToString();
        if (score > int.Parse(scoreTxtEndGame.text))
        {
            scoreTxtEndGame.text = score.ToString();
        }

        currentDifficulty = Mathf.RoundToInt(score / 250);
    }

    public void EndGame()
    {
        levelGenerator.ResetAll();
        gameUi.SetActive(false);
        menuUi.SetActive(true);
    }

    public void StartGame()
    {
        currentDifficulty = 0;
        playerShip.ResetValue();
        gameUi.SetActive(true);
        menuUi.SetActive(false);
        score = 0;
        levelGenerator.NewGame();
    }
}
