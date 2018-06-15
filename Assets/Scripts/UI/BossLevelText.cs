using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows a image with text saying "Boss Level" using an animation component
/// </summary>
[RequireComponent(typeof(AnimationPopComponent))]
public class BossLevelText : MonoBehaviour {
    private AnimationPopComponent animationPopComponent;

    // Use this for initialization
    void Start () {
        animationPopComponent = GetComponent<AnimationPopComponent>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
	}

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.aISpawnComponent.newWaveEvent += WaveSpawned;
    }

    private void WaveSpawned(int difficulty, DifficultyIncrease difficultyIncrease)
    {
        if (difficultyIncrease.bossLevel)
        {
            animationPopComponent.StartAnimation();
        }
    }
}
