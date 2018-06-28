using System;

/// <summary>
/// Stores everything that the player has obtained
/// </summary>
public class PlayerProfile
{
    public PlayerCurrency playerCurrency;
    public PlayerItemInventory playerItemInventory;

    public PlayerProfile()
    {
        playerItemInventory = new PlayerItemInventory();
        playerCurrency = new PlayerCurrency();
    }

    /// <summary>
    /// Objects that subscribes to the callback has to be created here to prevent circulair dependency with the setup of the Instance of SingleObjectInstanceLocator
    /// </summary>
    internal void SetupDependentObjects()
    {
        playerCurrency.SetupDependencies();
    }
}