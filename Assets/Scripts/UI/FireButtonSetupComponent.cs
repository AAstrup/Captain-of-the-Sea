using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component used for managing the fire button in the UI
/// </summary>
public class FireButtonSetupComponent : MonoBehaviour {

    public Image skillImage;
    public Button button;

    private void Start()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(dependencies);
    }

    private void dependencies(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.playerIdentifierComponent.GetComponent<AbilityPlayerInputComponent>().abilityTriggerEvent += triggerEvent;
    }

    private void triggerEvent(IItemAbilityComponent usedAbility, IItemAbilityComponent nextAbility)
    {
        skillImage.sprite = nextAbility.GetModel().sprite;
    }

    internal void AddDelegateToButton(Action fireAbility)
    {
        button.onClick.AddListener(delegate () { fireAbility(); });
    }
}
