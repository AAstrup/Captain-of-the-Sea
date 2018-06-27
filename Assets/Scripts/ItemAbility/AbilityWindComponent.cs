using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ItemAbility for speed boost
/// </summary>
public class AbilityWindComponent : MonoBehaviour, IItemAbilityComponent, IMultiplier {
    private ShopItemModel model;
    private float timeActivated;
    private TimeScalesComponent timeScalesComponent;
    private static readonly float windDuration = .6f;
    private static readonly float windEffect = 30f;

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

    public float GetMultiplier()
    {
        return Mathf.Max(1f, ((timeActivated + windDuration) - timeScalesComponent.gamePlayTimeTime) * windEffect);
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
}
