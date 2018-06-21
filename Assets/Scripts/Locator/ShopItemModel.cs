using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a model of an item, that includes what is specified by the interface
/// </summary>
[Serializable]
public class ShopItemModel : IShopItemModel
{
    public string uniqueNameID;
    public Sprite sprite;
    public ShopItemStatModel[] shopItemStatModels;
    public int[] goldCostAtItemLevel;

    public int GetAttributeLevel(IShopItemStatModel shopItemStatModel, int itemLevel)
    {
        return itemLevel + 1;
    }

    public int GetGoldCost(int itemLevel)
    {
        return goldCostAtItemLevel[itemLevel];
    }

    public string GetID()
    {
        return uniqueNameID;
    }

    public Sprite GetItemSprite()
    {
        return sprite;
    }

    public IShopItemStatModel[] GetItemStats()
    {
        return shopItemStatModels;
    }
}