using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverComponent : MonoBehaviour {

    public GameObject[] disableGameObjectsOnGameOver;
    public Text scoreText;
    public Text highscoreText;
    private static readonly string HighscoreKey = "HighScore";

    // Use this for initialization
    void Start () {
        PlayerIdentifierComponent.playerGameObject.GetComponent<HealthComponent>().dieEvent += GameOver;
        gameObject.SetActive(false);
    }

    private void GameOver(HealthComponent player)
    {
        gameObject.SetActive(true);
        foreach (var item in disableGameObjectsOnGameOver)
        {
            item.SetActive(false);
        }

        scoreText.text = PlayerScoreComponent.instance.score.ToString();
        if (PlayerScoreComponent.instance.score > GetHighScore())
        {
            SaveHighScore(PlayerScoreComponent.instance.score);
        }
        highscoreText.text = "Highscore: " + GetHighScore();
    }

    private void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(HighscoreKey, score);
    }

    private int GetHighScore()
    {
        if (PlayerPrefs.HasKey(HighscoreKey))
            return PlayerPrefs.GetInt(HighscoreKey);
        else
            return 0;
    }
}
