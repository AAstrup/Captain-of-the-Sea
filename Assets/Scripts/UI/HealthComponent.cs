using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component responsible for managing objects with health
/// Destroys object when dead
/// </summary>
[RequireComponent(typeof(OwnerComponent))]
public class HealthComponent : MonoBehaviour
{
    public float health = 10;
    public delegate void DieEvent(HealthComponent victim);
    public DieEvent dieEvent;
    public delegate void HealthChangedEvent(HealthComponent victim, float damage, float healthLeft);
    public HealthChangedEvent healthChangedEvent;
    [HideInInspector]
    public OwnerComponent ownerComponent;
    private SoundEffectPoolComponent soundEffectPoolComponent;

    private void Awake()
    {
        ownerComponent = GetComponent<OwnerComponent>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback);
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        soundEffectPoolComponent.PlaySound(SoundEffectPoolComponent.SoundsType.HitShip);
        if (healthChangedEvent != null)
            healthChangedEvent(this, dmg, health);
        if (health <= 0f)
            Death();
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        soundEffectPoolComponent = locator.componentReferences.soundEffectPoolComponent;
    }

    private void Death()
    {
        if (dieEvent != null)
            dieEvent(this);

        Destroy(gameObject);
    }
}
