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

    public Text text;
    [HideInInspector]
    public int score;

    void Start () {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
        UpdateUI();
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.aISpawnComponent.shipSpawnedEvent += ShipSpawned;
    }

    private void ShipSpawned(HealthComponent ship)
    {
        ship.dieEvent += IncreaseScore;
    }

    private void IncreaseScore(HealthComponent victim)
    {
        score++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        text.text = "Ships sunk " + score.ToString();
    }
}
