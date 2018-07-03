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
        costText.text = respawnCost.ToString();
    }

    private void Start()
    {
        playerIdentifierComponent = ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>();
        currency = ComponentLocator.instance.singleObjectInstanceReferences.playerProfile.playerCurrency;
        IAP = ComponentLocator.instance.GetDependency<IAPPurchaseCanvasComponent>();
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
