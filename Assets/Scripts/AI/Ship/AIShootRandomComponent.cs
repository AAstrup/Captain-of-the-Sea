using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires shootcomponent that is children of the gameobject
/// Fires whenever possible, starting with the cooldown on
/// </summary>
[RequireComponent(typeof(ShipConfigurationComponent))]
public class AIShootRandomComponent : MonoBehaviour {

    ShootComponent[] shootComponents;
    private ShipConfigurationComponent config;
    float timeLeft;
    private TimeScalesComponent timeScalesComponent;

    void Awake()
    {
        shootComponents = GetComponentsInChildren<ShootComponent>();
        config = GetComponent<ShipConfigurationComponent>();
        timeLeft = config.fireSpeed;
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
            timeLeft = config.fireSpeed;
            foreach (var shootComponent in shootComponents)
            {
                shootComponent.Trigger();
            }
        }
    }
}
