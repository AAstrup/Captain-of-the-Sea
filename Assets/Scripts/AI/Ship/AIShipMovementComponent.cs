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
        gameObjectTarget = PlayerIdentifierComponent.playerGameObject;
    }

    void Update () {
        if(PlayerIdentifierComponent.playerGameObject != null)
            movementComponent.ApplyMovementInDirection((gameObjectTarget.transform.position + gameObjectTarget.transform.right * -2f) - transform.position);
    }
}
