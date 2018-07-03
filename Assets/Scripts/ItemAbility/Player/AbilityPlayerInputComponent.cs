using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for exposing the method of firing the next ability.
/// Furthremore groups abilities of the same type to fire all at the same time
/// </summary>
[RequireComponent(typeof(AbilitySetupComponent))]
public class AbilityPlayerInputComponent : MonoBehaviour {
    private AbilitySetupComponent abilitySetupComponent;
    private List<List<IItemAbilityComponent>> itemAbilitiesGrouped;
    public delegate void AbilityTriggerEvent(IItemAbilityComponent usedAbility, IItemAbilityComponent nextAbility);
    public AbilityTriggerEvent abilityTriggerEvent;

    void Start () {
        abilitySetupComponent = GetComponent<AbilitySetupComponent>();
        abilitySetupComponent.GetAbilitiesWhenInstantiated(SetupAbilities);
        var reference = ComponentLocator.instance.GetDependency<FireButtonSetupComponent>();
        reference.AddDelegateToButton(FireAbility);
    }

    private void FireAbility()
    {
        if (itemAbilitiesGrouped != null)
        {
            var itemsGroupToTrigger = itemAbilitiesGrouped[0];
            itemAbilitiesGrouped.RemoveAt(0);
            foreach (var item in itemsGroupToTrigger)
            {
                item.Trigger();
            }
            itemAbilitiesGrouped.Add(itemsGroupToTrigger);
            if (abilityTriggerEvent != null)
                abilityTriggerEvent(itemsGroupToTrigger[0], itemAbilitiesGrouped[0][0]);
        }
    }

    private void SetupAbilities(List<IItemAbilityComponent> itemAbilities)
    {
        itemAbilitiesGrouped = new List<List<IItemAbilityComponent>>();
        foreach (var itemToAdd in itemAbilities)
        {
            bool groupFoundAndAdded = false;
            foreach (var group in itemAbilitiesGrouped)
            {
                if(group[0].GetType() == itemToAdd.GetType())
                {
                    group.Add(itemToAdd);
                    groupFoundAndAdded = true;
                }
            }
            if (!groupFoundAndAdded)
                itemAbilitiesGrouped.Add(new List<IItemAbilityComponent>() { itemToAdd });
        }
    }
}
