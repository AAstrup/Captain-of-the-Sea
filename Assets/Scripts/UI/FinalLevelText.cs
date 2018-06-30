using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows a image with text saying "Final wave" using an animation component
/// </summary>
[RequireComponent(typeof(AnimationPopComponent))]
public class FinalLevelText : MonoBehaviour {
    private AnimationPopComponent animationPopComponent;

    void Awake () {
        animationPopComponent = GetComponent<AnimationPopComponent>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
	}

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.aISpawnComponent.newWaveEvent += WaveSpawned;
    }

    private void WaveSpawned(int difficulty, DifficultyIncrease difficultyIncrease)
    {
        if (difficultyIncrease.finalLevel)
        {
            animationPopComponent.StartAnimation();
        }
    }
}
