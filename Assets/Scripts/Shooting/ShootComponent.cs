using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for shooting, that is spawning a bullet
/// </summary>
public class ShootComponent : MonoBehaviour {

    OwnerComponent ownerComponent;
    AudioSource audio;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private ParticlePoolComponent particlePoolComponent;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        ownerComponent = GetComponentInParent<OwnerComponent>();
        SingleComponentInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleComponentInstanceLocator locator)
    {
        particlePoolComponent = locator.componentReferences.particlePoolComponent;
    }

    internal void Fire()
    {
        var gmj = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        var bullet = gmj.GetComponent<BulletComponent>();
        bullet.myOwner.owner = ownerComponent.owner;
        particlePoolComponent.FireParticleSystem(ParticlePoolComponent.ParticleSystemType.CannonFire, firePoint.transform.position, transform.eulerAngles.z);
        audio.Play();
    }
}
