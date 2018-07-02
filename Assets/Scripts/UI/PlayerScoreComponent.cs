using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for updating the UI player score
/// Also runs animations
/// </summary>
[RequireComponent(typeof(AnimationPopComponent))]
public class PlayerScoreComponent : MonoBehaviour {

    public Text text;
    [HideInInspector]
    public int score;
    AnimationPopComponent animationPopComponent;

    void Awake () {
        animationPopComponent = GetComponent<AnimationPopComponent>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
        UpdateUI();
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.GetDependency<AISpawnComponent>().shipSpawnedEvent += ShipSpawned;
    }

    private void ShipSpawned(HealthComponent ship)
    {
        ship.dieEvent += IncreaseScore;
    }

    private void IncreaseScore(HealthComponent victim)
    {
        score++;
        animationPopComponent.StartAnimation();
        UpdateUI();
    }

    private void UpdateUI()
    {
        text.text = score.ToString();
    }
}
