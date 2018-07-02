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
        abilitySetupComponent.itemAbilitiesSetup += SetItemAbilities;

        if (shipConfigurationComponent == null)
            shipConfigurationComponent = GetComponent<AIShipConfigurationComponent>();

        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(setupDependencies);
    }

    private void setupDependencies(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.GetDependency<TimeScalesComponent>();
    }

    private void SetItemAbilities(List<IItemAbilityComponent> itemAbilities)
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
