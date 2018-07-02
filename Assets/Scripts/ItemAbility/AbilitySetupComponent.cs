using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can setup abilities and their prefabs
/// </summary>
[RequireComponent(typeof(OwnerComponent))]
public class AbilitySetupComponent : MonoBehaviour {
    public AbilitySetupPositionComponent[] abilitySpots;
    private ShopItemLibraryComponent library;

    public delegate void ItemAbilitiesSetupEvent(List<IItemAbilityComponent> itemAbilities);
    public ItemAbilitiesSetupEvent itemAbilitiesSetup;
    public SingleObjectInstanceLocator.DependenciesLoadedEvent dependenciesLoaded;
    private bool dependenciesHasLoaded;
    private List<IItemAbilityComponent> itemAbilities;
    public OwnerComponent owner;

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
        if (owner == null)
            owner = GetComponent<OwnerComponent>();
        if (abilitySpots == null || abilitySpots.Length == 0)
            abilitySpots = GetComponentsInChildren<AbilitySetupPositionComponent>();
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        library = locator.componentReferences.GetDependency<ShopItemLibraryComponent>();
        dependenciesHasLoaded = true;
        if (dependenciesLoaded != null)
            dependenciesLoaded();
    }

    public void InstantiateAbilities (List<AbilitySetupInfo> abilitySetupInfos)
    {
        itemAbilities = new List<IItemAbilityComponent>();
        for (int i = 0; i < abilitySetupInfos.Count; i++)
        {
            if (i >= abilitySpots.Length)
                throw new System.Exception("Not enough ability spots to support the amount of abilities, ability nr " + i);

            var item = library.GetItem(abilitySetupInfos[i].uniqueNameID);
            for (int x = 0; x < abilitySetupInfos[i].abilitySpotNumber.Count; x++)
            {
                int spotNumber = abilitySetupInfos[i].abilitySpotNumber[x];
                var gmj = Instantiate(item.prefab, abilitySpots[spotNumber].transform, false);
                var component = gmj.GetComponent<IItemAbilityComponent>();
                component.Initialize(gameObject, item, owner.owner);
                itemAbilities.Add(component);
            }
        }
        if (itemAbilitiesSetup != null)
            itemAbilitiesSetup(itemAbilities);
    }

    internal void SubscribeDependenciesLoaded(SingleObjectInstanceLocator.DependenciesLoadedEvent callback)
    {
        if (dependenciesHasLoaded)
            callback();
        else
            dependenciesLoaded += callback;
    }

public void GetAbilitiesWhenInstantiated(ItemAbilitiesSetupEvent callback)
    {
        if (itemAbilities == null)
            itemAbilitiesSetup += callback;
        else
            callback(itemAbilities);
    }
}