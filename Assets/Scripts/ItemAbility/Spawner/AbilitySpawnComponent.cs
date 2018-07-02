using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for spawning a prefab
/// </summary>
[RequireComponent(typeof(AudioSource), typeof(OwnerComponent))]
public class AbilitySpawnComponent : MonoBehaviour, IItemAbilityComponent
{
    AudioSource audio;
    public Transform firePoint;
    public GameObject spawnPrefab;
    public ParticlePoolComponent.ParticleSystemType particleType;
    public OwnerComponent ownerComponent;

    private ParticlePoolComponent particlePoolComponent;
    private ShopItemModel model;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if(ownerComponent == null)
            ownerComponent = GetComponent<OwnerComponent>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        particlePoolComponent = locator.componentReferences.GetDependency<ParticlePoolComponent>();
    }

    public void Trigger()
    {
        var gmj = Instantiate(spawnPrefab, firePoint.position, transform.rotation);
        var spawn = gmj.GetComponent<IAbilitySpawnComponent>();
        spawn.SetOwner(ownerComponent.owner);
        particlePoolComponent.FireParticleSystem(particleType, firePoint.transform.position, transform.eulerAngles.z);
        audio.Play();
    }

    public ShopItemModel GetModel()
    {
        return model;
    }

    public void Initialize(GameObject shipGameObject, ShopItemModel model, OwnerComponent.Owner owner)
    {
        this.model = model;
        ownerComponent.owner = owner;
    }

    public float GetRotation()
    {
        return transform.localEulerAngles.z;
    }
}
