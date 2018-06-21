using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Model definition of an attribute of a item model
/// </summary>
[Serializable]
public class ShopItemStatModel : IShopItemStatModel
{
    public string name;
    public Sprite sprite;

    public string GetName()
    {
        return name;
    }

    public Sprite GetStatSprite()
    {
        return sprite;
    }
}