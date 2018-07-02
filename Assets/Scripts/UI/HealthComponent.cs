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
    float startHealth;
    public float health = 10;
    public delegate void DieEvent(HealthComponent victim);
    public DieEvent dieEvent;
    public delegate void HealthChangedEvent(HealthComponent victim, float damage, float healthLeft);
    public HealthChangedEvent healthChangedEvent;
    [HideInInspector]
    public OwnerComponent ownerComponent;
    private SoundEffectPoolComponent soundEffectPoolComponent;
    private ParticlePoolComponent particlePoolComponent;
    private ICustomDeathComponent customDeathComponent;

    private void Awake()
    {
        startHealth = health;
        ownerComponent = GetComponent<OwnerComponent>();
        customDeathComponent = GetComponent<ICustomDeathComponent>();
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

    public void Revive()
    {
        health = startHealth;
        if (healthChangedEvent != null)
            healthChangedEvent(this, -startHealth, health);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        soundEffectPoolComponent = locator.componentReferences.GetDependency<SoundEffectPoolComponent>();
        particlePoolComponent = locator.componentReferences.GetDependency<ParticlePoolComponent>();
    }

    private void Death()
    {
        if (dieEvent != null)
            dieEvent(this);

        particlePoolComponent.FireParticleSystem(ParticlePoolComponent.ParticleSystemType.ShipDead, transform.position, 0f);
        if (customDeathComponent != null)
            customDeathComponent.TriggerDeathEvent();
        else
            Destroy(gameObject);
    }
}
