using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the amount of gold the player has
/// </summary>
[RequireComponent(typeof(Text),typeof(AnimationPopComponent))]
public class PlayerCurrencyTextUpdaterComponent : MonoBehaviour {
    private Text textComp;
    private AnimationPopComponent animationPopComponent;
    public CurrencyType currencyType;

    void Awake () {
        animationPopComponent = GetComponent<AnimationPopComponent>();
        textComp = GetComponent<Text>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.objectReferences.playerProfile.playerCurrency.OnCurrencyChange(currencyType, UpdateUI);
        UpdateUI(locator.objectReferences.playerProfile.playerCurrency.GetCurrencyAmount(currencyType));
    }

    private void UpdateUI(int amount)
    {
        textComp.text = amount.ToString();
        animationPopComponent.StartAnimation();
    }
}
