using UnityEngine;

/// <summary>
/// Implemented by components that respresents item abilities
/// </summary>
public interface IItemAbilityComponent
{
    void Initialize(GameObject gameObject, ShopItemModel item, OwnerComponent.Owner owner);
    void Trigger();
    ShopItemModel GetModel();
    float GetRotation();
}