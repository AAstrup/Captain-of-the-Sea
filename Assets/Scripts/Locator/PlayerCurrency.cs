using System;
using UnityEngine;

/// <summary>
/// Stores the player's currency
/// </summary>
public class PlayerCurrency
{
    int amount;
    public static readonly string GoldKey = "Gold";
    public delegate void GoldChangedEvent(int goldAmount);
    public GoldChangedEvent goldChangedEvent;

    public PlayerCurrency()
    {
        amount = PlayerPrefs.GetInt(GoldKey, 0);
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.aISpawnComponent.shipSpawnedEvent += SetupShipDependency;
    }

    private void SetupShipDependency(HealthComponent healthComponent)
    {
        healthComponent.dieEvent += delegate (HealthComponent victim) { AddGold(1); };
    }

    public int GetGoldAmount()
    {
        return amount;
    }

    public void AddGold(int amountToAdd)
    {
        amount += amountToAdd;
        Save();
    }

    internal bool CanAfford(int v)
    {
        return amount >= v;
    }

    internal void SpendGold(int v)
    {
        amount -= v;
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(GoldKey, amount);
        if (goldChangedEvent != null)
            goldChangedEvent.Invoke(amount);
    }
}