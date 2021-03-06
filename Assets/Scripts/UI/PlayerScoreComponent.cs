﻿using System;
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

    void Awake() {
        animationPopComponent = GetComponent<AnimationPopComponent>();
    }

    private void Start()
    {
        ComponentLocator.instance.GetDependency<AISpawnComponent>().shipSpawnedEvent += ShipSpawned;
        UpdateUI();
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
