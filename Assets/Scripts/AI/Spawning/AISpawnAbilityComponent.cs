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
        var config = healthComponent.GetComponent<AIShipConfigurationComponent>();

        List<AbilitySetupInfo> abilities = new List<AbilitySetupInfo>();
        int randomSpotNumberOffset = UnityEngine.Random.Range(0, abilitySetupComp.abilitySpots.Length);
        for (int i = 0; i < abilitySetupComp.abilitySpots.Length && i < config.maxAbilities; i++)
        {
            abilities.Add(
                new AbilitySetupInfo()
                {
                    abilitySpotNumber = new int[] { (randomSpotNumberOffset + i) % abilitySetupComp.abilitySpots.Length },
                    uniqueNameID = itemAbilities[UnityEngine.Random.Range(0, itemAbilities.Length)]
                }
            );
        }

        abilitySetupComp.dependenciesLoaded += delegate () { abilitySetupComp.InstantiateAbilities(abilities); };
    }
}
