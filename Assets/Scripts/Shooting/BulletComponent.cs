using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving a bullet and detecting collision
/// </summary>
[RequireComponent(typeof(OwnerComponent))]
internal class BulletComponent : MonoBehaviour
{
    public OwnerComponent myOwner;
    public float lifeSpanTotal = 3f;
    public AnimationCurve sizeFalling;
    public AnimationCurve speed;
    public float speedMultiplier = 1f;
    public float damage;
    float lifeSpanLeft;
    Vector3 startScale;
    private TimeScalesComponent timeScalesComponent;
    private ParticlePoolComponent particlePoolComponent;
    private List<HealthComponent> victimsHit;

    private void Awake()
    {
        startScale = transform.localScale;
        lifeSpanLeft = lifeSpanTotal;
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
        victimsHit = new List<HealthComponent>();
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
        particlePoolComponent = locator.componentReferences.particlePoolComponent;
    }

    private void Update()
    {
        lifeSpanLeft -= Time.deltaTime * timeScalesComponent.gamePlayTimeScale;

        if (lifeSpanLeft < 0f)
            Destroy(gameObject);
        
        var value = 1f - (lifeSpanLeft / lifeSpanTotal);
        transform.localScale = Vector3.Lerp(Vector3.zero, startScale, sizeFalling.Evaluate(value));
        transform.position += transform.right * Time.deltaTime * timeScalesComponent.gamePlayTimeScale * speed.Evaluate(value) * speedMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent victim = collision.gameObject.GetComponent<HealthComponent>();
        if (victim && victim.ownerComponent.owner != myOwner.owner && !victimsHit.Contains(victim))
        {
            victimsHit.Add(victim);
            victim.Damage(damage);
            particlePoolComponent.FireParticleSystem(ParticlePoolComponent.ParticleSystemType.ShipHit, transform.position, transform.eulerAngles.z + 180f);
        }
    }
}