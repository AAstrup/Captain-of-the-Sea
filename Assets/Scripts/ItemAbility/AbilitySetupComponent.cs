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

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(setupDependencies);
        if(abilitySpots == null || abilitySpots.Length == 0)
            abilitySpots = GetComponentsInChildren<AbilitySetupPositionComponent>();
    }

    private void setupDependencies(SingleObjectInstanceLocator locator)
    {
        library = locator.componentReferences.shopItemLibraryComponent;
    }

    public void InstantiateAbilities (List<AbilitySetupInfo> abilitySetupInfos)
    {
        var itemAbilities = new List<IItemAbilityComponent>();
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
}