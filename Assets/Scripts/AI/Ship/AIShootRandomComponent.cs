﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootRandomComponent : MonoBehaviour {

    ShootComponent[] shootComponents;
    public float timeBetweenShotsMin = 1f;
    public float timeBetweenShotsMax = 1.5f;
    float timeLeft;
    private TimeScalesComponent timeScalesComponent;

    void Awake()
    {
        shootComponents = GetComponentsInChildren<ShootComponent>();
        timeLeft = UnityEngine.Random.Range(timeBetweenShotsMin, timeBetweenShotsMax);
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime * timeScalesComponent.GetGamePlayTimeScale();
        if (timeLeft < 0)
        {
            timeLeft = UnityEngine.Random.Range(timeBetweenShotsMin, timeBetweenShotsMax);
            foreach (var shootComponent in shootComponents)
            {
                shootComponent.Fire();
            }
        }
    }
}
