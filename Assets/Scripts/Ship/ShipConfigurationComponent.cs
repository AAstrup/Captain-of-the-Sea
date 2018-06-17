using System;
using UnityEngine;

/// <summary>
/// Contains configuration for any ship
/// Does not contain the health, as there is a component dedicated to this task, HealthComponent
/// </summary>
[RequireComponent(typeof(HealthComponent))]
public class ShipConfigurationComponent : MonoBehaviour
{
    public float maxSpeed = 0.02f;
    public float accelerateSpeed = 0.01f;
    public float fireSpeed = 2f;
    [HideInInspector]
    public HealthComponent healthComponent;
    public SpriteRenderer[] flags;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }
}