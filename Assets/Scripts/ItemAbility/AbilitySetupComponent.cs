using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can setup abilities and their prefabs
/// </summary>
public class AbilitySetupComponent : MonoBehaviour {
    public AbilitySetupPositionComponent[] abilitySpots;
    private ShopItemLibraryComponent library;
    public delegate void ItemAbilitiesSetupEvent(List<IItemAbilityComponent> itemAbilities);
    public ItemAbilitiesSetupEvent itemAbilitiesSetup;
    public SingleObjectInstanceLocator.DependenciesLoadedEvent dependenciesLoaded;
    private List<IItemAbilityComponent> itemAbilities;

    private void Start()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
        if(abilitySpots == null || abilitySpots.Length == 0)
            abilitySpots = GetComponentsInChildren<AbilitySetupPositionComponent>();
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        library = locator.componentReferences.shopItemLibraryComponent;
        if(dependenciesLoaded != null)
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
            for (int x = 0; x < abilitySetupInfos[i].abilitySpotNumber.Length; x++)
            {
                int spotNumber = abilitySetupInfos[i].abilitySpotNumber[x];
                var gmj = Instantiate(item.prefab, abilitySpots[spotNumber].transform, false);
                var component = gmj.GetComponent<IItemAbilityComponent>();
                component.Initialize(gameObject, item);
                itemAbilities.Add(component);
            }
        }
        if (itemAbilitiesSetup != null)
            itemAbilitiesSetup(itemAbilities);
    }

    public void GetAbilitiesWhenInstantiated(ItemAbilitiesSetupEvent callback)
    {
        if (itemAbilities == null)
            itemAbilitiesSetup += callback;
        else
            callback(itemAbilities);
    }
}