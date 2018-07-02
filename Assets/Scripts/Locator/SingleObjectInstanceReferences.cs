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
    }

    internal void SetupDependentObjects()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(setup);
        playerProfile.SetupDependentObjects();
    }

    private void setup(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.GetDependency<PlayerIdentifierComponent>().playerGameObject.GetComponent<HealthComponent>().dieEvent += delegate (HealthComponent comp) { SavePlayerProfile(); };
    }

    public void SavePlayerProfile()
    {
        SaveLoad.SavePlayerProfile(playerProfile);
    }
}