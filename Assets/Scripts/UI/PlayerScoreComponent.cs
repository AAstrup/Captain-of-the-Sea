using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for updating the UI player score
/// Is a ´singleton to allow other scripts to acces its score
/// </summary>
public class PlayerScoreComponent : MonoBehaviour {

    public static PlayerScoreComponent instance;
    public Text text;
    [HideInInspector]
    public int score;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        AISpawnComponent.instance.shipSpawnedEvent += ShipSpawned;
	}

    private void ShipSpawned(HealthComponent ship)
    {
        ship.dieEvent += IncreaseScore;
    }

    private void IncreaseScore(HealthComponent victim)
    {
        score++;
        text.text = "Score: " + score;
    }
}
