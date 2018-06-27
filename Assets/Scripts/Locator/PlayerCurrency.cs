using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the player's currency
/// </summary>
public class PlayerCurrency
{
    /// <summary>
    /// Represents a single currency
    /// </summary>
    public class Currency
    {
        public CurrencyType CurrencyType;
        public int amount;
        public CurrenyChangedEvent valueChanged;
    }

    Dictionary<CurrencyType, Currency> currencies;
    public enum CurrencyType { Gold, Gems };
    public delegate void CurrenyChangedEvent(int goldAmount);

    public PlayerCurrency()
    {
        currencies = new Dictionary<CurrencyType, Currency>();
        foreach (CurrencyType item in Enum.GetValues(typeof(CurrencyType)))
        {
            currencies.Add(item, 
                new Currency()
            {
                amount = PlayerPrefs.GetInt(item.ToString(), 0),
                CurrencyType = item
            });
        }

        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    internal int GetCurrencyAmount(CurrencyType currencyType)
    {
        return currencies[currencyType].amount;
    }

    internal void OnCurrencyChange(CurrencyType currencyType, CurrenyChangedEvent callback)
    {
        currencies[currencyType].valueChanged += callback;
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.aISpawnComponent.shipSpawnedEvent += SetupShipDependency;
    }

    private void SetupShipDependency(HealthComponent healthComponent)
    {
        healthComponent.dieEvent += delegate (HealthComponent victim) { AddCurrency(CurrencyType.Gold, 1); };
    }

    public void AddCurrency(CurrencyType currencyType, int amountToAdd)
    {
        currencies[currencyType].amount += amountToAdd;
        if (currencies[currencyType].valueChanged != null)
            currencies[currencyType].valueChanged(currencies[currencyType].amount);
        Save();
    }

    internal void Spend(CurrencyType currencyType, int amountToReduce)
    {
        currencies[currencyType].amount -= amountToReduce;
        if (currencies[currencyType].valueChanged != null)
            currencies[currencyType].valueChanged(currencies[currencyType].amount);
        Save();
    }

    internal bool CanAfford(CurrencyType currencyType, int minimumRequirement)
    {
        return currencies[currencyType].amount >= minimumRequirement;
    }

    private void Save()
    {
        foreach (var currency in currencies)
        {
            PlayerPrefs.SetInt(currency.Key.ToString(), currency.Value.amount);
        }
    }
}