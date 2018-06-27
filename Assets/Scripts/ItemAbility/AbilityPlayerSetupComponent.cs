using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pipes the player's abilities to the AbilitySetupComponent
/// </summary>
[RequireComponent(typeof(AbilitySetupComponent))]
public class AbilityPlayerSetupComponent : MonoBehaviour {
    private AbilitySetupComponent abilitySetupComponent;
    private List<AbilitySetupInfo> models;

    void Awake () {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(setupDependencies);
        abilitySetupComponent = GetComponent<AbilitySetupComponent>();
    }

    private void setupDependencies(SingleObjectInstanceLocator locator)
    {
        var items = locator.objectReferences.playerItemInventory.GetAllActiveItems();
        models = new List<AbilitySetupInfo>();
        foreach (var item in items)
        {
            models.Add(item.Value.abilitySetupInfo);
        }
    }

    private void Update()
    {
        if (models != null)
        {
            abilitySetupComponent.InstantiateAbilities(models);
            models = null;
        }
    }
}
