using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires shootcomponent that is children of the gameobject
/// Fires whenever possible, starting with the cooldown on
/// </summary>
[RequireComponent(typeof(ShipConfigurationComponent), typeof(AIShipConfigurationComponent))]
public class AIShootRandomComponent : MonoBehaviour {

    ShootComponent[] shootComponents;
    public ShipConfigurationComponent config;
    public AIShipConfigurationComponent aiConfig;
    float timeLeft;
    private TimeScalesComponent timeScalesComponent;

    void Awake()
    {
        shootComponents = GetComponentsInChildren<ShootComponent>();
        if(config == null)
            config = GetComponent<ShipConfigurationComponent>();

        if (aiConfig == null)
            aiConfig = GetComponent<AIShipConfigurationComponent>();

        timeLeft = UnityEngine.Random.Range(0, aiConfig.fireSpeed);
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
            timeLeft = aiConfig.fireSpeed;
            foreach (var shootComponent in shootComponents)
            {
                shootComponent.Trigger();
            }
        }
    }
}
