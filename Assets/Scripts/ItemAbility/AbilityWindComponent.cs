using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ItemAbility for speed boost
/// </summary>
public class AbilityWindComponent : MonoBehaviour, IItemAbilityComponent, ISubstitudeValueCandidate {
    private ShopItemModel model;
    private float timeActivated;
    private TimeScalesComponent timeScalesComponent;
    private static readonly float windDuration = .6f;
    private static readonly float windEffect = 6f;

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(dependency);
    }

    private void dependency(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
    }

    public ShopItemModel GetModel()
    {
        return model;
    }

    public float GetSubstitudeValue()
    {
        //return Mathf.Max(1f, ((timeActivated + windDuration) - timeScalesComponent.gamePlayTimeTime) * windEffect);
        return (timeActivated + windDuration) > timeScalesComponent.gamePlayTimeTime ? windEffect : 1f;
    }

    public void Initialize(GameObject shipGameObject, ShopItemModel model)
    {
        shipGameObject.GetComponent<ShipConfigurationComponent>().AddMaxSpeedMultiplier(this);
        shipGameObject.GetComponent<ShipConfigurationComponent>().AddAcelerationMultiplier(this);
        this.model = model;
    }

    public void Trigger()
    {
        timeActivated = timeScalesComponent.gamePlayTimeTime;
    }

    public float GetRotation()
    {
        return transform.localEulerAngles.z;
    }
}
