using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Respawns a player in exchange for gems
/// </summary>
[RequireComponent(typeof(Button))]
public class RespawnPayComponent : MonoBehaviour
{
    private PlayerIdentifierComponent playerIdentifierComponent;
    private PlayerCurrency currency;
    private IAPPurchaseCanvasComponent IAP;
    private Button button;
    public Text costText;
    private int respawnCost = 2;

    private void Awake()
    {
        button = GetComponent<Button>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
        costText.text = respawnCost.ToString();
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        playerIdentifierComponent = locator.componentReferences.GetDependency<PlayerIdentifierComponent>();
        currency = locator.objectReferences.playerProfile.playerCurrency;
        IAP = locator.componentReferences.GetDependency<IAPPurchaseCanvasComponent>();
    }

    public void ButtonPressed()
    {
        if (!currency.CanAfford(CurrencyType.Gems, respawnCost))
        {
            IAP.OpenShopAsPopUp();
            return;
        }
        currency.Spend(CurrencyType.Gems, respawnCost);
        respawnCost *= 2;
        costText.text = respawnCost.ToString();
        playerIdentifierComponent.GetComponent<PlayerReviveComponent>().Revive();
    }
}
