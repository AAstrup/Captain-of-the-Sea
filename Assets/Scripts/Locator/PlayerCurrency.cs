using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Stores the player's currency
/// </summary>
public class PlayerCurrency
{
    Dictionary<CurrencyType, Currency> currencies;
    public delegate void CurrenyChangedEvent(int currencyAmount);
    public delegate void CurrenySpendEvent();
    public CurrenySpendEvent currencySpendEvent;

    /// <summary>
    /// Constructor used when creating a new playercurrency with no serialized data
    /// </summary>
    public PlayerCurrency()
    {
        currencies = new Dictionary<CurrencyType, Currency>();
        foreach (CurrencyType item in Enum.GetValues(typeof(CurrencyType)))
        {
            currencies.Add(item, 
                new Currency()
            {
                amount = 0,
                currencyType = item
            });
        }
        currencies[CurrencyType.Gold].AddAmount(100);
        currencies[CurrencyType.Gems].AddAmount(10);
    }

    /// <summary>
    /// Constructor used when deserializing
    /// </summary>
    /// <param name="serializedCurrencies"></param>
    public PlayerCurrency(List<Currency> serializedCurrencies)
    {
        currencies = new Dictionary<CurrencyType, Currency>();
        foreach (var item in serializedCurrencies)
        {
            currencies.Add(item.currencyType, item);
        }
    }

    public void SetupDependencies()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    internal int GetCurrencyAmount(CurrencyType currencyType)
    {
        return currencies[currencyType].amount;
    }

    internal void OnCurrencyChange(CurrencyType currencyType, CurrenyChangedEvent callback)
    {
        currencies[currencyType].Subscribe(callback);
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.GetDependency<AISpawnComponent>().shipSpawnedEvent += SetupShipDependency;
    }

    private void SetupShipDependency(HealthComponent healthComponent)
    {
        healthComponent.dieEvent += delegate (HealthComponent victim) { AddCurrency(CurrencyType.Gold, 1); };
    }

    public void AddCurrency(CurrencyType currencyType, int amountToAdd)
    {
        currencies[currencyType].AddAmount(amountToAdd);
    }

    internal void Spend(CurrencyType currencyType, int amountToReduce)
    {
        currencies[currencyType].AddAmount(-amountToReduce);
        if (currencySpendEvent != null)
            currencySpendEvent();
    }

    internal void Spend(Currency cost)
    {
        Spend(cost.currencyType, cost.amount);
    }

    internal bool CanAfford(CurrencyType currencyType, int minimumRequirement)
    {
        return currencies[currencyType].amount >= minimumRequirement;
    }

    internal bool CanAfford(Currency cost)
    {
        return CanAfford(cost.currencyType, cost.amount);
    }

    internal Dictionary<CurrencyType, Currency> GetSerializeInfo()
    {
        return currencies;
    }
}