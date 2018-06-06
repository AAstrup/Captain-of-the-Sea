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

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        ownerComponent = GetComponentInParent<OwnerComponent>();
    }

    internal void Fire()
    {
        var gmj = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        var bullet = gmj.GetComponent<BulletComponent>();
        bullet.myOwner.owner = ownerComponent.owner;
        ParticlePoolComponent.instance.FireParticleSystem(ParticlePoolComponent.ParticleSystemType.CannonFire, firePoint.transform.position, transform.eulerAngles.z);
        audio.Play();
    }
}
