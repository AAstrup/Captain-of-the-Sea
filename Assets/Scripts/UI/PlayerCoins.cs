using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the amount of gold the player has
/// </summary>
[RequireComponent(typeof(Text),typeof(AnimationPopComponent))]
public class PlayerCoins : MonoBehaviour {
    private Text textComp;
    private AnimationPopComponent animationPopComponent;

    void Awake () {
        animationPopComponent = GetComponent<AnimationPopComponent>();
        textComp = GetComponent<Text>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.objectReferences.playerCurrency.goldChangedEvent += UpdateUI;
        UpdateUI(locator.objectReferences.playerCurrency.GetGoldAmount());
    }

    private void UpdateUI(int goldAmount)
    {
        textComp.text = goldAmount.ToString();
        animationPopComponent.StartAnimation();
    }
}
