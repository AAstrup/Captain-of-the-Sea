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
    List<ISubstitudeValueCandidate> maxSpeedMultiplier;
    public float accelerateSpeed = 0.01f;
    List<ISubstitudeValueCandidate> accelerationMultiplier;
    [HideInInspector]
    public HealthComponent healthComponent;
    public SpriteRenderer[] flags;
    public float brakeSpeedDivider = 1.5f;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        maxSpeedMultiplier = new List<ISubstitudeValueCandidate>();
        accelerationMultiplier = new List<ISubstitudeValueCandidate>();
    }

    public float GetMaxSpeedWithMultipliers()
    {
        float speed = maxSpeed;
        foreach (var item in maxSpeedMultiplier)
        {
            if (item.GetSubstitudeValue() > speed)
                speed = item.GetSubstitudeValue();
        }
        return speed;
    }

    internal float GetAccelerationWithMultipliers()
    {
        float speed = accelerateSpeed;
        foreach (var item in accelerationMultiplier)
        {
            if (item.GetSubstitudeValue() > speed)
                speed = item.GetSubstitudeValue();
        }
        return speed;
    }

    internal void AddAcelerationMultiplier(ISubstitudeValueCandidate multiplier)
    {
        accelerationMultiplier.Add(multiplier);
    }

    public void AddMaxSpeedMultiplier(ISubstitudeValueCandidate multiplier)
    {
        maxSpeedMultiplier.Add(multiplier);
    }
}