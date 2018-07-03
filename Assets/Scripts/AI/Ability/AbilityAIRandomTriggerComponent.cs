using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires the next ability with range given by the ship config
/// </summary>
[RequireComponent(typeof(AbilitySetupComponent), typeof(AIShipConfigurationComponent))]
public class AbilityAIRandomTriggerComponent : MonoBehaviour {
    private List<IItemAbilityComponent> itemAbilities;
    public AbilitySetupComponent abilitySetupComponent;
    public AIShipConfigurationComponent shipConfigurationComponent;
    float timeLeftBeforeFire;
    private TimeScalesComponent timeScalesComponent;

    void Awake () {
        timeLeftBeforeFire = shipConfigurationComponent.fireSpeed;

        if (abilitySetupComponent == null)
            abilitySetupComponent = GetComponent<AbilitySetupComponent>();
         abilitySetupComponent.GetAbilitiesWhenInstantiated(SetupDependencies);

        if (shipConfigurationComponent == null)
            shipConfigurationComponent = GetComponent<AIShipConfigurationComponent>();

        timeScalesComponent = ComponentLocator.instance.GetDependency<TimeScalesComponent>();
    }

    private void SetupDependencies(List<IItemAbilityComponent> itemAbilities)
    {
        this.itemAbilities = itemAbilities;
    }

    private void Update()
    {
        if(itemAbilities != null)
        {
            timeLeftBeforeFire -= timeScalesComponent.GetGamePlayTimeScale() * Time.deltaTime;
            if (timeLeftBeforeFire < 0f)
                Fire();
        }
    }

    private void Fire()
    {
        timeLeftBeforeFire = shipConfigurationComponent.fireSpeed;

        var item = itemAbilities[0];
        item.Trigger();
        itemAbilities.RemoveAt(0);
        itemAbilities.Add(item);
    }
}
