using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for showing the players item in the inventory UI
/// </summary>
public class ShopInventoryPanelComponent : MonoBehaviour {

    public ShopInventoryItemComponent[] items;

    void Awake () {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
	}

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        var playerInventory = locator.objectReferences.playerProfile.playerItemInventory;
        var itemnLibrary = locator.componentReferences.shopItemLibraryComponent;

        int currentUIItemIndex = 0;
        foreach (var playerItem in playerInventory.items)
        {
            items[currentUIItemIndex++].ShowItem(playerInventory, playerItem.Value, itemnLibrary.GetItem(playerItem.Value.uniqueItemID));
            if (items.Length <= currentUIItemIndex)
                throw new Exception("Too many items to support in UI, max supported " + items.Length);
        }
        for (; currentUIItemIndex < items.Length; currentUIItemIndex++)
        {
            items[currentUIItemIndex].ShowNoItem();
        }
    }
}
