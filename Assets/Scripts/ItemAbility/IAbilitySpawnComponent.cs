/// <summary>
/// prefabs spawned by the AbilitySpawnComponent are expected to have a component implementing this interface.
/// </summary>
internal interface IAbilitySpawnComponent
{
    void SetOwner(OwnerComponent.Owner owner);
}