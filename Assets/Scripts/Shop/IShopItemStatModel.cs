using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Required by the shop UI to show stats of items
/// </summary>
public interface IShopItemStatModel
{
    Sprite GetStatSprite();
    string GetName();
}