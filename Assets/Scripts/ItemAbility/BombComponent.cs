using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for exploding
/// </summary>
[RequireComponent(typeof(OwnerComponent), typeof(CircleCollider2D))]
internal class BombComponent : MonoBehaviour, IAbilitySpawnComponent
{
    public OwnerComponent myOwner;
    public CircleCollider2D circleCollider2D;
    [HideInInspector]
    public List<HealthComponent> victims;
    private float timeLeft;
    private static readonly float timeTotal = 2f;
    private static readonly float damage = 2f;
    private TimeScalesComponent timeScalesComponent;
    private ParticlePoolComponent particlePoolComponent;
    public ParticlePoolComponent.ParticleSystemType shipHitParticle;
    public ParticlePoolComponent.ParticleSystemType triggerParticle;

    private void Awake()
    {
        timeLeft = timeTotal;
        if (myOwner == null)
            myOwner = GetComponent<OwnerComponent>();
        if (circleCollider2D == null)
            circleCollider2D = GetComponent<CircleCollider2D>();
        victims = new List<HealthComponent>();
        timeScalesComponent = ComponentLocator.instance.GetDependency<TimeScalesComponent>();
        particlePoolComponent = ComponentLocator.instance.GetDependency<ParticlePoolComponent>();
    }

    private void Update()
    {
        timeLeft -= timeScalesComponent.GetGamePlayTimeScale() * Time.deltaTime;
        if(timeLeft < 0f)
        {
            for (int i = 0; i < victims.Count; i++)
            {
                if (victims[i] == null)
                    continue;
                particlePoolComponent.FireParticleSystem(shipHitParticle, victims[i].transform.position, 0f);
                victims[i].Damage(damage);
            }
            particlePoolComponent.FireParticleSystem(triggerParticle, transform.position, transform.eulerAngles.z);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var owner = collision.GetComponent<OwnerComponent>();
        if (owner != null && owner.owner != myOwner.owner)
            victims.Add(owner.GetComponent<HealthComponent>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var owner = collision.GetComponent<OwnerComponent>();
        if (owner != null && owner.owner != myOwner.owner)
            victims.Remove(owner.GetComponent< HealthComponent>());
    }

    public void SetOwner(OwnerComponent.Owner owner)
    {
        myOwner.owner = owner;
    }
}