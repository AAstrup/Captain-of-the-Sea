using System;
using System.Collections.Generic;

/// <summary>
/// This represents an item that is unlocked, and what unlock/upgrade level it is
/// </summary>
[Serializable]
public class PlayerItem
{
    // To identify the item
    public string uniqueItemID;
    public int itemLevel;
    public bool isActiveItem;
}