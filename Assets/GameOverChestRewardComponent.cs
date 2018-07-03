using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverChestRewardComponent : MonoBehaviour {

    public GameOverComponent gameOverComponent;
    public Text chestLevelText;
    private AISpawnComponent aISpawnComponent;

    // Use this for initialization
    void Awake () {
        gameOverComponent.gameOverEvent += GameOverReward;
    }

    private void Start()
    {
        aISpawnComponent = ComponentLocator.instance.GetDependency<AISpawnComponent>();
    }

    private void GameOverReward()
    {
        if (aISpawnComponent.difficultyNr == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        chestLevelText.text = "Chest level " + aISpawnComponent.difficultyNr.ToString();
    }
}
