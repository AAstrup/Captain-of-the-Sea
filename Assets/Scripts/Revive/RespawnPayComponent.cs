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
    private Button button;
    public Text costText;
    private int respawnCost = 5;

    private void Awake()
    {
        button = GetComponent<Button>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
        costText.text = respawnCost.ToString();
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        playerIdentifierComponent = locator.componentReferences.playerIdentifierComponent;
        currency = locator.objectReferences.playerCurrency;
        currency.OnCurrencyChange(PlayerCurrency.CurrencyType.Gems, CurrencyUpdate);
        CurrencyUpdate(currency.GetCurrencyAmount(PlayerCurrency.CurrencyType.Gems));
    }

    private void CurrencyUpdate(int currencyAmount)
    {
        button.interactable = currencyAmount >= respawnCost;
    }

    public void ButtonPressed()
    {
        if (!currency.CanAfford(PlayerCurrency.CurrencyType.Gems, respawnCost))
        {
            Debug.Log("FAILSAFE, button should not be enabled");
            return;
        }
        currency.Spend(PlayerCurrency.CurrencyType.Gems, respawnCost);
        respawnCost *= 2;
        costText.text = respawnCost.ToString();
    }
}
