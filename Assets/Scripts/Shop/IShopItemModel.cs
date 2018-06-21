using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The shop UI requires an object implementing this interface to setup all it needs
/// </summary>
public interface IShopItemModel
{
    Sprite GetItemSprite();
    IShopItemStatModel[] GetItemStats();
    int GetGoldCost(int itemLevel);
    string GetID();
    int GetAttributeLevel(IShopItemStatModel shopItemStatModel, int itemLevel);
}