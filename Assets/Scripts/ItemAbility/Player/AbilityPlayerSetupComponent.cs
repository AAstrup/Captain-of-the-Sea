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
    private PlayerItemInventory inventory;

    void Awake () {
        abilitySetupComponent = GetComponent<AbilitySetupComponent>();
        inventory = ComponentLocator.instance.singleObjectInstanceReferences.playerProfile.playerItemInventory;
        ComponentLocator.instance.GetDependency<MenuStartComponent>().gameStartedEvent += start;
    }

    private void start()
    {
        models = new List<AbilitySetupInfo>();
        var items = inventory.GetAllActiveItems();
        foreach (var item in items)
        {
            models.Add(item.Value.abilitySetupInfo);
        }
        abilitySetupComponent.InstantiateAbilities(models);
    }

    private void Update()
    {
        if (models != null)
        {
            models = null;
        }
    }
}
