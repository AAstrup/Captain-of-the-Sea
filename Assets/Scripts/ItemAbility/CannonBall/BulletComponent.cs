using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving a bullet, detecting collision, and decreasing its size (including collider)
/// </summary>
[RequireComponent(typeof(OwnerComponent))]
internal class BulletComponent : MonoBehaviour, IAbilitySpawnComponent
{
    public OwnerComponent myOwner;
    public float lifeSpanTotal = 3f;
    public AnimationCurve sizeFalling;
    public AnimationCurve speed;
    public float speedMultiplier = 1f;
    public float damage;
    float lifeTimeElapsed;
    Vector3 startScale;
    private TimeScalesComponent timeScalesComponent;
    private ParticlePoolComponent particlePoolComponent;
    private List<HealthComponent> victimsHit;
    private float particlesAccumilated;
    public AnimationCurve particleSpawnRate;
    private static readonly float particleStopFireTime = 0.70f;

    private void Awake()
    {
        startScale = transform.localScale;
        lifeTimeElapsed = 0f;
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
        lifeTimeElapsed += Time.deltaTime * timeScalesComponent.GetGamePlayTimeScale();
        particlesAccumilated += timeScalesComponent.GetGamePlayTimeScale() * Time.deltaTime;

        if (lifeTimeElapsed > lifeSpanTotal)
            Destroy(gameObject);

        var elapsedFraction = lifeTimeElapsed / lifeSpanTotal;
        transform.localScale = sizeFalling.Evaluate(elapsedFraction) * startScale;
        transform.position += transform.right * Time.deltaTime * timeScalesComponent.GetGamePlayTimeScale() * speed.Evaluate(elapsedFraction) * speedMultiplier;

        int particlesToSpawn = Mathf.FloorToInt(particlesAccumilated * particleSpawnRate.Evaluate(elapsedFraction));
        if (particlesToSpawn > 0 && particleStopFireTime > elapsedFraction) {
            particlesAccumilated -= particlesToSpawn / particleSpawnRate.Evaluate(elapsedFraction);
            particlePoolComponent.FireParticleSystem(ParticlePoolComponent.ParticleSystemType.CannonBallInAir, transform.position, 0f, particlesToSpawn);
        }
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

    public void SetOwner(OwnerComponent.Owner owner)
    {
        myOwner.owner = owner;
    }
}