using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for shooting, that is spawning a bullet
/// </summary>
public class ShootComponent : MonoBehaviour, IItemAbilityComponent {

    OwnerComponent ownerComponent;
    AudioSource audio;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private ParticlePoolComponent particlePoolComponent;
    private ShopItemModel model;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        ownerComponent = GetComponentInParent<OwnerComponent>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        particlePoolComponent = locator.componentReferences.particlePoolComponent;
    }

    public void Trigger()
    {
        var gmj = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        var bullet = gmj.GetComponent<BulletComponent>();
        bullet.myOwner.owner = ownerComponent.owner;
        particlePoolComponent.FireParticleSystem(ParticlePoolComponent.ParticleSystemType.CannonFire, firePoint.transform.position, transform.eulerAngles.z);
        audio.Play();
    }

    public ShopItemModel GetModel()
    {
        return model;
    }

    public void Initialize(GameObject shipGameObject, ShopItemModel model)
    {
        this.model = model;
    }
}
