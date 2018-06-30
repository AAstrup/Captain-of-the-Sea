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
    public Text chestLevelText;
    private IAnimationObject[] animationObjects;
    private PlayerScoreComponent playerScoreComponent;
    private AISpawnComponent aISpawnComponent;
    private static readonly string HighscoreKey = "HighScore";

    private void Awake()
    {
        animationObjects = GetComponentsInChildren<IAnimationObject>();
    }

    // Use this for initialization
    void Start () {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
        gameObject.SetActive(false);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.playerIdentifierComponent.playerGameObject.GetComponent<HealthComponent>().dieEvent += GameOver;
        playerScoreComponent = locator.componentReferences.playerScoreComponent;
        locator.componentReferences.playerIdentifierComponent.GetComponent<PlayerReviveComponent>().playerRevivedEvent += playerRevived;
        aISpawnComponent = locator.componentReferences.aISpawnComponent;
    }

    private void playerRevived()
    {
        gameObject.SetActive(false);
        foreach (var item in disableGameObjectsOnGameOver)
        {
            item.SetActive(true);
        }
    }

    private void GameOver(HealthComponent player)
    {
        gameObject.SetActive(true);
        foreach (var item in disableGameObjectsOnGameOver)
        {
            item.SetActive(false);
        }

        scoreText.text = playerScoreComponent.score.ToString();
        if (playerScoreComponent.score > GetHighScore())
        {
            SaveHighScore(playerScoreComponent.score);
        }
        highscoreText.text = "Highscore: " + GetHighScore();

        foreach (var item in animationObjects)
        {
            item.StartAnimation();
        }
        chestLevelText.text = "Chest level " + aISpawnComponent.waveNr.ToString();
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
