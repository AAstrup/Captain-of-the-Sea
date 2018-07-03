using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component used to purchase new ItemAbilities
/// </summary>
[RequireComponent(typeof(Button))]
public class BuyNewAbilityComponent : MonoBehaviour {

    public Button button;
    public Currency cost;
    private PlayerCurrency playerCurrency;
    private PlayerItemInventory inventory;
    private ShopItemLibraryComponent itemLibrary;
    private IAPPurchaseCanvasComponent iAPShopCanvas;
    public Text costText;

    private void Awake()
    {
        playerCurrency = ComponentLocator.instance.singleObjectInstanceReferences.playerProfile.playerCurrency;
        inventory = ComponentLocator.instance.singleObjectInstanceReferences.playerProfile.playerItemInventory;
        itemLibrary = ComponentLocator.instance.GetDependency<ShopItemLibraryComponent>();
        iAPShopCanvas = ComponentLocator.instance.GetDependency<IAPPurchaseCanvasComponent>();
        if (button == null)
            button = GetComponent<Button>();
        button.onClick.AddListener(AttemptPurchase);
        costText.text = cost.amount.ToString();
    }

    private void AttemptPurchase()
    {
        if (playerCurrency.CanAfford(cost))
        {
            playerCurrency.Spend(cost);
            var itemToUnlock = itemLibrary.GetRandomItemModel();
            inventory.AddItem(itemToUnlock, 1);
            ComponentLocator.instance.singleObjectInstanceReferences.SavePlayerProfile();
            ComponentLocator.instance.GetDependency<ShopInventoryPanelComponent>().UpdateUI();
        }
        else
        {
            iAPShopCanvas.OpenShopAsPopUp();
        }
    }
}
