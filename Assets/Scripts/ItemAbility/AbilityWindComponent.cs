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
    private TimeScalesComponent _timeScalesComponent;
    private TimeScalesComponent timeScalesComponent {
        get {
            if(_timeScalesComponent == null)
                _timeScalesComponent = ComponentLocator.instance.GetDependency<TimeScalesComponent>();
            return _timeScalesComponent;
        }
    }

    private static readonly float windDuration = .6f;
    private static readonly float windEffect = 6f;
    
    private void Start()
    {
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

    public void Initialize(GameObject shipGameObject, ShopItemModel model, OwnerComponent.Owner owner)
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
