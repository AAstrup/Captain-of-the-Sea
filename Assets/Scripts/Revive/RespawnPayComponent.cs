﻿using System;
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
    private Button button;
    public Text costText;
    private int respawnCost = 1;

    private void Awake()
    {
        button = GetComponent<Button>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
        costText.text = respawnCost.ToString();
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        playerIdentifierComponent = locator.componentReferences.playerIdentifierComponent;
        currency = locator.objectReferences.playerProfile.playerCurrency;
        currency.OnCurrencyChange(CurrencyType.Gems, CurrencyUpdate);
        CurrencyUpdate(currency.GetCurrencyAmount(CurrencyType.Gems));
    }

    private void CurrencyUpdate(int currencyAmount)
    {
        button.interactable = currencyAmount >= respawnCost;
    }

    public void ButtonPressed()
    {
        if (!currency.CanAfford(CurrencyType.Gems, respawnCost))
        {
            Debug.Log("FAILSAFE, button should not be enabled");
            return;
        }
        currency.Spend(CurrencyType.Gems, respawnCost);
        respawnCost *= 2;
        costText.text = respawnCost.ToString();
        playerIdentifierComponent.GetComponent<PlayerReviveComponent>().Revive();
    }
}
