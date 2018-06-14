using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for enabling/disabling the objects that changes the state of the game from Playing to Game Over
/// </summary>
public class GameOverComponent : MonoBehaviour {

    public GameObject[] disableGameObjectsOnGameOver;
    public Text scoreText;
    public Text highscoreText;
    private IAnimationObject[] animationObjects;
    private static readonly string HighscoreKey = "HighScore";

    private void Awake()
    {
        animationObjects = GetComponentsInChildren<IAnimationObject>();
    }

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

        scoreText.text = SingleComponentInstanceLocator.instance.playerScoreComponent.score.ToString();
        if (SingleComponentInstanceLocator.instance.playerScoreComponent.score > GetHighScore())
        {
            SaveHighScore(SingleComponentInstanceLocator.instance.playerScoreComponent.score);
        }
        highscoreText.text = "Highscore: " + GetHighScore();

        foreach (var item in animationObjects)
        {
            item.StartAnimation();
        }
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
