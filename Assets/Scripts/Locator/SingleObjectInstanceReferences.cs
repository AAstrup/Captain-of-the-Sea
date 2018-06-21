using System;
/// <summary>
/// Contains single object instances that does not inherit from MB
/// Is retrieved from and kept by SingleComponentInstance
/// </summary>
public class SingleObjectInstanceReferences
{
    public PlayerCurrency playerCurrency;
    public PlayerItemInventory playerItemInventory;

    public SingleObjectInstanceReferences()
    {
        playerItemInventory = new PlayerItemInventory();
    }

    /// <summary>
    /// Objects that subscribes to the callback has to be created here to prevent circulair dependency with the setup of the Instance of SingleObjectInstanceLocator
    /// </summary>
    internal void SetupDependentObjects()
    {
        playerCurrency = new PlayerCurrency();
    }
}