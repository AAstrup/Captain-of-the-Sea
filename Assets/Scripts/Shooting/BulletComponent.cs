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

    private void Awake()
    {
        startScale = transform.localScale;
        lifeSpanLeft = lifeSpanTotal;
    }

    private void Update()
    {
        lifeSpanLeft -= Time.deltaTime * TimeScalesComponent.instance.gamePlayTimeScale;

        if (lifeSpanLeft < 0f)
            Destroy(gameObject);
        
        var value = 1f - (lifeSpanLeft / lifeSpanTotal);
        transform.localScale = Vector3.Lerp(Vector3.zero, startScale, sizeFalling.Evaluate(value));
        transform.position += transform.right * Time.deltaTime * TimeScalesComponent.instance.gamePlayTimeScale * speed.Evaluate(value) * speedMultiplier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthComponent victim = collision.gameObject.GetComponent<HealthComponent>();
        if (victim && victim.ownerComponent.owner != myOwner.owner)
        {
            victim.Damage(damage);
            ParticlePoolComponent.instance.FireParticleSystem(ParticlePoolComponent.ParticleSystemType.ShipHit, transform.position, transform.eulerAngles.z + 180f);
            Destroy(gameObject);
        }
    }
}