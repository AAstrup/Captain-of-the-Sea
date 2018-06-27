using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains configuration for any ship
/// Does not contain the health, as there is a component dedicated to this task, HealthComponent
/// </summary>
[RequireComponent(typeof(HealthComponent))]
public class ShipConfigurationComponent : MonoBehaviour
{
    public float maxSpeed = 0.02f;
    List<IMultiplier> maxSpeedMultiplier;
    public float accelerateSpeed = 0.01f;
    List<IMultiplier> accelerationMultiplier;
    [HideInInspector]
    public HealthComponent healthComponent;
    public SpriteRenderer[] flags;
    public float brakeSpeedDivider = 2f;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        maxSpeedMultiplier = new List<IMultiplier>();
        accelerationMultiplier = new List<IMultiplier>();
    }

    public float GetMaxSpeedWithMultipliers()
    {
        float speed = maxSpeed;
        foreach (var item in maxSpeedMultiplier)
        {
            speed *= item.GetMultiplier();
        }
        return speed;
    }

    internal float GetAccelerationWithMultipliers()
    {
        float speed = accelerateSpeed;
        foreach (var item in accelerationMultiplier)
        {
            speed *= item.GetMultiplier();
        }
        return speed;
    }

    internal void AddAcelerationMultiplier(IMultiplier multiplier)
    {
        accelerationMultiplier.Add(multiplier);
    }

    public void AddMaxSpeedMultiplier(IMultiplier multiplier)
    {
        maxSpeedMultiplier.Add(multiplier);
    }
}