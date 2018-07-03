using System;
/// <summary>
/// Contains single object instances that does not inherit from MB
/// Is retrieved from and kept by SingleComponentInstance
/// </summary>
public class SingleObjectInstanceReferences
{
    public PlayerProfile playerProfile;

    public SingleObjectInstanceReferences()
    {
        if (SaveLoad.PlayerProfileExist())
            playerProfile = SaveLoad.LoadPlayerProfile();
        else
            playerProfile = new PlayerProfile();

        playerProfile.playerCurrency.currencySpendEvent += SavePlayerProfile;
        playerProfile.playerItemInventory.itemChangedEvent += SavePlayerProfile;
        SetupDependentObjects();
    }

    internal void SetupDependentObjects()
    {
        ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>().playerGameObject.GetComponent<HealthComponent>().dieEvent += delegate (HealthComponent comp) { SavePlayerProfile(); };
        playerProfile.SetupDependentObjects();
    }

    public void SavePlayerProfile()
    {
        SaveLoad.SavePlayerProfile(playerProfile);
    }
}