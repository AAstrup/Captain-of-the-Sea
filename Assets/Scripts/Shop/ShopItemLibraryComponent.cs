using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the models for ItemsAbilities
/// </summary>
public class ShopItemLibraryComponent : MonoBehaviour {

    public List<ShopItemModel> itemModels;
    private ShopItemPanelComponent shop;
    private bool triggered;

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        shop = locator.componentReferences.shopItemPanelComponent;
    }

    private void Update()
    {
        if (!triggered && shop && shop.IsSetup()) {
            shop.PresentItem(itemModels[0]);
            triggered = true;
        }
    }

    /// <summary>
    /// Looking up in a list could be optimized, is only a list to be able to show in inspector
    /// Ideally it generated a dictionary on start
    /// </summary>
    /// <param name="uniqueNameID"></param>
    public ShopItemModel GetItem(ShopItemModel.ItemID uniqueNameID)
    {
        return itemModels.Find(x => x.GetID() == uniqueNameID);
    }
}
