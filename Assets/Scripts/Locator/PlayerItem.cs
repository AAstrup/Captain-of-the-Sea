using System;
using System.Collections.Generic;

/// <summary>
/// This represents an item that is unlocked, and what unlock/upgrade level it is
/// </summary>
[Serializable]
public class PlayerItem
{
    public ShopItemModel.ItemID uniqueItemID;
    public AbilitySetupInfo abilitySetupInfo;
    public int itemLevel;
    public bool isActiveItem;
}