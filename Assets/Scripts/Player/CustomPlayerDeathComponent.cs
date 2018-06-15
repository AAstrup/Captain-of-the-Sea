using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Overrides the default die behaviour of the HealthComponent
/// </summary>
[RequireComponent(typeof(HealthComponent))]
public class CustomPlayerDeathComponent : MonoBehaviour, ICustomDeathComponent
{
    private TimeScalesComponent timeScalesComponent;

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencySetup);
    }

    private void DependencySetup(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
    }

    public void TriggerDeathEvent()
    {
        timeScalesComponent.gamePlayStopped = true;
    }
}
