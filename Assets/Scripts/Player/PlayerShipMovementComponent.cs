using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Interacts with the ship movement component to move the players ship
/// </summary>
[RequireComponent(typeof(ShipMovementComponent),typeof(PlayerInputComponent))]
public class PlayerShipMovementComponent : MonoBehaviour {

    PlayerInputComponent playerInputLibrary;
    ShipMovementComponent movementComponent;
    Vector2 targetPos;
    private int fingerID = 0;

    public AnimationPopComponent targetAnimation;

    void Awake () {
        playerInputLibrary = GetComponent<PlayerInputComponent>();
        movementComponent = GetComponent<ShipMovementComponent>();
        targetPos = transform.position;
    }

    private void Start()
    {
        SingleComponentInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback);
    }

    private void DependencyCallback(SingleComponentInstanceLocator locator)
    {
        locator.componentReferences.menuStartComponent.gameStartedEvent += UpdateTargetPosition;
    }

    void Update () {
        if (Input.GetKeyDown(playerInputLibrary.moveKey) && !EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            UpdateTargetPosition();
        }
        var targetDirection = targetPos - new Vector2(transform.position.x, transform.position.y);
        if (targetDirection.magnitude > 1)
            movementComponent.ApplyMovementInDirection(targetDirection);
        else
            movementComponent.Brake();
    }

    private void UpdateTargetPosition()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetAnimation.gameObject.transform.position = targetPos;
        targetAnimation.StartAnimation();
    }
}
