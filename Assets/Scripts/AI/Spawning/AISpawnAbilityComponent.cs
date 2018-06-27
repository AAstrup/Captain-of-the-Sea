using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds abilities to ship spawned by the AISpawnComponent
/// </summary>
[RequireComponent(typeof(AISpawnComponent))]
public class AISpawnAbilityComponent : MonoBehaviour
{
    public ShopItemModel.ItemID[] itemAbilities;
    List<AbilitySetupComponent> toSetup;

    private void Start()
    {
        GetComponent<AISpawnComponent>().shipSpawnedEvent += AddAbilities;
    }

    private void AddAbilities(HealthComponent healthComponent)
    {
        var abilitySetupComp = healthComponent.GetComponent<AbilitySetupComponent>();
        if (abilitySetupComp == null)
            return;

        List<AbilitySetupInfo> abilities = new List<AbilitySetupInfo>();
        for (int i = 0; i < abilitySetupComp.abilitySpots.Length; i++)
        {
            abilities.Add(
                new AbilitySetupInfo()
                {
                    abilitySpotNumber = new int[] { i },
                    uniqueNameID = itemAbilities[UnityEngine.Random.Range(0, itemAbilities.Length)]
                }
            );
        }

        abilitySetupComp.dependenciesLoaded += delegate () { abilitySetupComp.InstantiateAbilities(abilities); };
    }
}
