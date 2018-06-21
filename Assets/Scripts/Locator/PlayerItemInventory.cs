using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Responsible for storing and exposing the items (and levels of items) that the player has bought
/// </summary>
public class PlayerItemInventory
{
    // The key is the id of an item
    public Dictionary<string, PlayerItem> items;
    public static readonly int maxActiveItems = 3;

    public PlayerItemInventory()
    {
        items = new Dictionary<string, PlayerItem>();

        AddItem(new PlayerItem() { itemLevel = 1, uniqueItemID = "Cannon", isActiveItem = true });
    }

    private void AddItem(PlayerItem playerItem)
    {
        items.Add(playerItem.uniqueItemID, playerItem);
    }

    internal void DeactivateItem(PlayerItem item)
    {
        item.isActiveItem = false;
    }

    internal void ActivateItem(PlayerItem item)
    {
        var activeItems = items.Where(x => x.Value.isActiveItem == true).ToArray();
        if((activeItems.Length + 1) >= maxActiveItems)
        {
            activeItems[0].Value.isActiveItem = false;
        }
        item.isActiveItem = true;
    }

    /// <summary>
    /// Returns item level
    /// </summary>
    /// <param name="shopItemModel"></param>
    /// <returns>Returns 0 if item is not unlocked</returns>
    internal int GetItemLevel(IShopItemModel shopItemModel)
    {
        if (items.ContainsKey(shopItemModel.GetID()))
            return items[shopItemModel.GetID()].itemLevel;
        else
            return 0;
    }

    public KeyValuePair<string, PlayerItem>[] GetAllActiveItems()
    {
        return items.Where(x => x.Value.isActiveItem == true).ToArray();
    }
}