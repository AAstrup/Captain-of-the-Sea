using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains references to type and skill points in the type of stat
/// </summary>
public class ShopItemStatComponent : MonoBehaviour {

    public Text statText;
    public Image statType;
    public ImageBlinkComponent[] statValues;

    internal void PresentStat(IShopItemModel itemModel, IShopItemStatModel shopItemStatModel, int itemLevel)
    {
        statText.text = shopItemStatModel.GetName();
        statType.sprite = shopItemStatModel.GetStatSprite();
        int currentAttributeLevel = itemModel.GetAttributeLevel(shopItemStatModel, itemLevel);
        int upgradeAttributeLevel = itemModel.GetAttributeLevel(shopItemStatModel, itemLevel + 1);

        for (int i = 0; i < statValues.Length; i++)
        {
            if(i < currentAttributeLevel)
            {
                ShowAsUnlockedAttribute(statValues[i]);
            }
            else if(i < upgradeAttributeLevel)
            {
                ShowAsUpgradeAttribute(statValues[i]);
            }
            else
            {
                ShowAsLockedAttribute(statValues[i]);
            }
        }
    }

    private void ShowAsUnlockedAttribute(ImageBlinkComponent imageBlinkComponent)
    {
        imageBlinkComponent.enabled = false;
        imageBlinkComponent.img.enabled = true;
    }

    private void ShowAsUpgradeAttribute(ImageBlinkComponent imageBlinkComponent)
    {
        imageBlinkComponent.enabled = true;
        imageBlinkComponent.img.enabled = true;
    }

    private void ShowAsLockedAttribute(ImageBlinkComponent imageBlinkComponent)
    {
        imageBlinkComponent.enabled = false;
        imageBlinkComponent.img.enabled = false;
    }
}
