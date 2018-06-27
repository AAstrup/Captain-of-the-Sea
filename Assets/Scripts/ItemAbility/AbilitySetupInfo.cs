using System;
using System.Collections.Generic;

/// <summary>
/// Contains an ability id and the spot it is used in
/// </summary>
[Serializable]
public class AbilitySetupInfo
{
    public int[] abilitySpotNumber;
    public ShopItemModel.ItemID uniqueNameID;
}