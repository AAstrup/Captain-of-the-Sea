using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains the references to the UI parts of a item in the shop
/// ShopItemStatComponent contains references to the stats
/// </summary>
public class ShopItemPanelComponent : MonoBehaviour {

    public Image itemImage;
    public ShopItemStatComponent[] stats;
    public Text costAmountText;
    public Image costAmountCoin;
    public Button upgradeButton;
    private PlayerItemInventory inventory;
    private PlayerCurrency playerCurrency;

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    internal bool IsSetup()
    {
        return inventory != null && playerCurrency != null;
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        inventory = locator.objectReferences.playerItemInventory;
        playerCurrency = locator.objectReferences.playerCurrency;
    }

    public void PresentItem(IShopItemModel shopItemModel)
    {
        gameObject.SetActive(true);
        itemImage.sprite = shopItemModel.GetItemSprite();
        var itemStatModel = shopItemModel.GetItemStats();
        int itemLevel = inventory.GetItemLevel(shopItemModel);

        for (int i = 0; i < stats.Length; i++)
        {
            if(i < itemStatModel.Length)
            {
                stats[i].enabled = true;
                stats[i].PresentStat(shopItemModel, itemStatModel[i], itemLevel);
            }
            else
            {
                stats[i].enabled = false;
            }
        }
        var cost = shopItemModel.GetGoldCost(itemLevel);
        costAmountText.text = "Upgrade " + cost.ToString();
        /// Move costAmountCoin depending on size of gold cost
        Debug.Log("Move costAmountCoin depending on size of gold cost");

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.interactable = playerCurrency.CanAfford(PlayerCurrency.CurrencyType.Gold, cost);
        if (upgradeButton.interactable)
        {
            upgradeButton.onClick.AddListener(delegate
            {
                BuyItem(shopItemModel);
            });
        }
    }

    private void BuyItem(IShopItemModel shopItemModel)
    {
        int itemLevel = inventory.GetItemLevel(shopItemModel);
        playerCurrency.Spend(PlayerCurrency.CurrencyType.Gold, shopItemModel.GetGoldCost(itemLevel));
        Debug.Log("Item " + shopItemModel.GetID() + " was bought");
    }
}
