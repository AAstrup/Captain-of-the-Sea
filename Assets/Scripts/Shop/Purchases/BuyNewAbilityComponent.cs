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
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependency);
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
        }
        else
        {
            iAPShopCanvas.OpenShopAsPopUp();
        }
    }

    private void SetupDependency(SingleObjectInstanceLocator locator)
    {
        playerCurrency = locator.objectReferences.playerProfile.playerCurrency;
        inventory = locator.objectReferences.playerProfile.playerItemInventory;
        itemLibrary = locator.componentReferences.shopItemLibraryComponent;
        iAPShopCanvas = locator.componentReferences.IAPShopCanvas;
    }
}
