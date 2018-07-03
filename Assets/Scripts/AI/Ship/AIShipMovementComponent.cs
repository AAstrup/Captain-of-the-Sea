using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the ShipMovementComponent
/// </summary>
[RequireComponent(typeof(ShipMovementComponent))]
public class AIShipMovementComponent : MonoBehaviour {

    public AbilitySetupComponent abilitySetupComponent;
    private GameObject gameObjectTarget;
    private ShipMovementComponent movementComponent;
    private float desiredWeaponAngle;

    void Awake () {
        var abilitySetupComponent = GetComponent<AbilitySetupComponent>();
        if (abilitySetupComponent != null)
            abilitySetupComponent.GetAbilitiesWhenInstantiated(SetupAbilities);
        else
            desiredWeaponAngle = 0f;

        movementComponent = GetComponent<ShipMovementComponent>();
    }

    private void SetupAbilities(List<IItemAbilityComponent> itemAbilities)
    {
        desiredWeaponAngle = itemAbilities[0].GetRotation();
    }

    public static Vector3 DegreeToVector2(float degree)
    {
        return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad),0);
    }

    private void Start()
    {
        gameObjectTarget = ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>().playerGameObject;
    }

    void Update()
    {
        if (gameObjectTarget != null)
        {
            movementComponent.ApplyMovementInDirection((gameObjectTarget.transform.position + DegreeToVector2(desiredWeaponAngle + gameObjectTarget.transform.eulerAngles.z) * -2f) - transform.position);
        }
    }
}
