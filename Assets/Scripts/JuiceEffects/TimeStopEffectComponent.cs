using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stops time on damage to signify the impact
/// </summary>
public class TimeStopEffectComponent : MonoBehaviour {

    // To prevent from activating on start it is by default set to 100
    float timeSinceLastStpo = 100f;
    public AnimationCurve timeReductionAmount;
    private TimeScalesComponent timeScalesComponent;

    private void Start()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.GetDependency<TimeScalesComponent>();
        locator.componentReferences.GetDependency<PlayerIdentifierComponent>().playerGameObject.GetComponent<HealthComponent>().healthChangedEvent += delegate (HealthComponent victim, float damage, float healthLeft) { StopTime(); };
        locator.componentReferences.GetDependency<AISpawnComponent>().shipSpawnedEvent += ShipSpawned;
    }

    private void ShipSpawned(HealthComponent healthComponent)
    {
        healthComponent.healthChangedEvent += delegate (HealthComponent victim, float damage, float healthLeft) { StopTime(); };
    }

    private void StopTime()
    {
        timeSinceLastStpo = 0f;
    }

    void Update()
    {
        var timeValue = timeReductionAmount.Evaluate(timeSinceLastStpo);
        timeScalesComponent.SetGamePlayTimeScale(timeValue);
        timeSinceLastStpo += Time.deltaTime;
    }

    /// <summary>
    /// Returns either 1 or -1
    /// </summary>
    /// <returns></returns>
    private int GetRandomSign()
    {
        return UnityEngine.Random.Range(0f, 100f) < 50f ? 1 : -1;
    }
}
