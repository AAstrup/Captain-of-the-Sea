using UnityEngine;

/// <summary>
/// Implemented by components that respresents item abilities
/// </summary>
public interface IItemAbilityComponent
{
    void Initialize(GameObject shipGameObject, ShopItemModel model);
    void Trigger();
    ShopItemModel GetModel();
    float GetRotation();
}