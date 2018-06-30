using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Responsible for storing and exposing the items (and levels of items) that the player has bought
/// </summary>
public class PlayerItemInventory
{
    public Dictionary<ShopItemModel.ItemID, PlayerItem> items;
    public static readonly int maxActiveItems = 3;
    public delegate void ItemsChangedEvent();
    public ItemsChangedEvent itemChangedEvent;

    /// <summary>
    /// Constructor used when creating a new inventory
    /// </summary>
    public PlayerItemInventory()
    {
        items = new Dictionary<ShopItemModel.ItemID, PlayerItem>();

        AddItem(
            new PlayerItem() {
                itemLevel = 1,
                uniqueItemID = ShopItemModel.ItemID.Cannon,
                isActiveItem = true,
                abilitySetupInfo = new AbilitySetupInfo()
                {
                    abilitySpotNumber = new int[] { 0 },
                    uniqueNameID = ShopItemModel.ItemID.Cannon
                }
            }
        );

        AddItem(
            new PlayerItem()
            {
                itemLevel = 1,
                uniqueItemID = ShopItemModel.ItemID.WindSail,
                isActiveItem = true,
                abilitySetupInfo = new AbilitySetupInfo()
                {
                    abilitySpotNumber = new int[] { 1 },
                    uniqueNameID = ShopItemModel.ItemID.WindSail
                }
            }
        );
    }

    /// <summary>
    /// Constructor used for deserializing
    /// </summary>
    /// <param name="serializedItems"></param>
    public PlayerItemInventory(List<PlayerItem> serializedItems)
    {
        items = new Dictionary<ShopItemModel.ItemID, PlayerItem>();
        foreach (var serializedItem in serializedItems)
        {
            items.Add(serializedItem.uniqueItemID, serializedItem);
        }
    }

    internal Dictionary<ShopItemModel.ItemID, PlayerItem> GetSerializeInfo()
    {
        return items;
    }

    private void AddItem(PlayerItem playerItem)
    {
        items.Add(playerItem.uniqueItemID, playerItem);
        if (itemChangedEvent != null)
            itemChangedEvent();
    }

    internal void AddItem(ShopItemModel itemToUnlock, int v)
    {
        if (!items.ContainsKey(itemToUnlock.uniqueNameID))
            items.Add(itemToUnlock.uniqueNameID, new PlayerItem(itemToUnlock));
        items[itemToUnlock.uniqueNameID].AddExperience(v);
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

    public KeyValuePair<ShopItemModel.ItemID, PlayerItem>[] GetAllActiveItems()
    {
        return items.Where(x => x.Value.isActiveItem == true).ToArray();
    }
}