using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Keeps references to elements that will have injected values
/// That is prices of the IAP and the buy button
/// </summary>
public class ShopIAPOfferComponent : MonoBehaviour {

    public float cost;
    public Text costAmountText;

    public CurrencyType rewardType = CurrencyType.Gems;
    public int rewardFlat;
    public Text rewardFlatAmountText;

    public int rewardBonus;
    public Text rewardBonusAmountText; // Can be null if rewardBonus is 0

    public Button payButton;
    private PlayerCurrency currencies;

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
        costAmountText.text = cost.ToString() + "$";
        rewardFlatAmountText.text = rewardFlat.ToString();
        if(rewardBonus != 0)
            rewardBonusAmountText.text = "+  " + rewardFlat.ToString() + " free";
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        currencies = locator.objectReferences.playerProfile.playerCurrency;
    }

    public void Buy()
    {
        //Pay money
        currencies.AddCurrency(rewardType, rewardFlat + rewardBonus);
    }
}
