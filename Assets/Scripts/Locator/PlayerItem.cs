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
    public int itemLevelExperience;
    public bool isActiveItem;

    public PlayerItem()
    {
    }

    public PlayerItem(ShopItemModel itemToUnlock)
    {
        uniqueItemID = itemToUnlock.uniqueNameID;
    }

    public void AddExperience(int experience)
    {
        int finalItemLevel = itemLevel + 1;
        while (GetExperienceForLevel(finalItemLevel) < experience)
        {
            experience -= GetExperienceForLevel(finalItemLevel);
            finalItemLevel++;
        }
        itemLevel = finalItemLevel;
        itemLevelExperience = experience;
    }

    int GetExperienceForLevel(int level)
    {
        return level * level;
    }
}