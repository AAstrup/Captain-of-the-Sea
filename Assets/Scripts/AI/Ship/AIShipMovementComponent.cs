using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the ShipMovementComponent
/// </summary>
[RequireComponent(typeof(ShipMovementComponent))]
public class AIShipMovementComponent : MonoBehaviour {

    GameObject gameObjectTarget;

    ShipMovementComponent movementComponent;
    void Awake () {
        movementComponent = GetComponent<ShipMovementComponent>();
    }

    private void Start()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        gameObjectTarget = locator.componentReferences.playerIdentifierComponent.playerGameObject;
    }

    void Update () {
        if(gameObjectTarget != null)
            movementComponent.ApplyMovementInDirection((gameObjectTarget.transform.position + gameObjectTarget.transform.right * -2f) - transform.position);
    }
}
