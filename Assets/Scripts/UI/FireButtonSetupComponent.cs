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

    private void Awake()
    {
        ComponentLocator.instance.GetDependency<MenuStartComponent>().gameStartedEvent += delegate () {
            SetImage(ComponentLocator.instance.GetDependency<AbilityPlayerInputComponent>().GetCurrectAbility().GetModel().sprite);
        };
        ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>().GetComponent<AbilityPlayerInputComponent>().abilityTriggerEvent += triggerEvent;
    }

    private void triggerEvent(IItemAbilityComponent usedAbility, IItemAbilityComponent nextAbility)
    {
        SetImage(nextAbility.GetModel().sprite);
    }

    private void SetImage(Sprite sprite)
    {
        skillImage.sprite = sprite;
    }

    internal void AddDelegateToButton(Action fireAbility)
    {
        button.onClick.AddListener(delegate () { fireAbility(); });
    }
}
