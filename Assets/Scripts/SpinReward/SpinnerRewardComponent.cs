using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for visualising the spin and reward gotten from it
/// Uses helpers to achieve this
/// </summary>
public class SpinnerRewardComponent : MonoBehaviour {

    public SpinnerComponent spinnerComponent;
    public Button respinButton;
    public Text costAmountText;
    private int cost = 1;
    private PlayerCurrency playerCurrency;
    private IAPPurchaseCanvasComponent iapStore;
    private static readonly int maxCost = 1000;

	void Awake () {
        respinButton.onClick.AddListener(respin);
        spinnerComponent.spinEndedEvent += spinFinished;
        respinButton.interactable = false;
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(dependencies);
    }

    private void dependencies(SingleObjectInstanceLocator locator)
    {
        playerCurrency = locator.objectReferences.playerProfile.playerCurrency;
        iapStore = locator.componentReferences.IAPShopCanvas;
    }

    public void InitialSpin()
    {
        Spin();
    }

    private void spinFinished()
    {
        Debug.Log("Spin finished");
        respinButton.interactable = true;
    }

    private void respin()
    {
        if (playerCurrency.CanAfford(CurrencyType.Gems, cost))
        {
            Spin();
            playerCurrency.Spend(CurrencyType.Gems, cost);
            UpdateCost(cost * 2);
            respinButton.interactable = false;
        }
        else
        {
            OpenStore();
        }
    }

    private void OpenStore()
    {
        iapStore.OpenShopAsPopUp();
    }

    private void UpdateCost(int newCost)
    {
        cost *= newCost;
        if (cost > maxCost)
            cost = maxCost;
        costAmountText.text = cost.ToString();
    }

    private void Pay(int cost)
    {
        Debug.Log("Not paying right now");
    }

    public void Spin()
    {
        var val = 180f;
        spinnerComponent.SpinDegrees(1080 + val);
    }
}
