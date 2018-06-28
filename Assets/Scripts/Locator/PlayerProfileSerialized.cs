using System;
using System.Collections.Generic;

/// <summary>
/// Represents a serialized player profile. 
/// This is needed because of the use of dictionary in currency and inventory
/// </summary>
[Serializable]
public class PlayerProfileSerialized
{
    public List<Currency> currencies;
    public List<PlayerItem> items;

    /// <summary>
    /// Used by serializer
    /// </summary>
    public PlayerProfileSerialized()
    {
    }

    /// <summary>
    /// Serializes a PlayerProfile
    /// </summary>
    /// <param name="profile">Profile to serialize</param>
    public PlayerProfileSerialized(PlayerProfile profile)
    {
        var currencyData = profile.playerCurrency.GetSerializeInfo();
        currencies = new List<Currency>();
        foreach (var item in currencyData)
        {
            currencies.Add(item.Value);
        }

        var itemData = profile.playerItemInventory.GetSerializeInfo();
        items = new List<PlayerItem>();
        foreach (var item in itemData)
        {
            items.Add(item.Value);
        }
    }

    internal PlayerProfile Deserialize()
    {
        PlayerProfile profile = new PlayerProfile();
        profile.playerCurrency = new PlayerCurrency(currencies);
        profile.playerItemInventory = new PlayerItemInventory(items);
        return profile;
    }
}