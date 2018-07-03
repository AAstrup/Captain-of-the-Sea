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
    private CameraDirectorComponent cameraDirectorComponent;

    void Awake () {
        playerInputLibrary = GetComponent<PlayerInputComponent>();
        movementComponent = GetComponent<ShipMovementComponent>();
        targetPos = transform.position;
    }

    private void Start()
    {
        ComponentLocator.instance.GetDependency<MenuStartComponent>().gameStartedEvent += UpdateTargetPosition;
        cameraDirectorComponent = ComponentLocator.instance.GetDependency<CameraDirectorComponent>();
    }

    void Update () {
        if (Input.GetKey(playerInputLibrary.moveKey) && !EventSystem.current.IsPointerOverGameObject(GetCursorID()))
        {
            UpdateTargetPosition();
        }
        var targetDirection = targetPos - new Vector2(transform.position.x, transform.position.y);
        if (targetDirection.magnitude > 1)
            movementComponent.ApplyMovementInDirection(targetDirection);
        else
            movementComponent.Brake();
    }

    private int GetCursorID()
    {
        if (Application.isEditor)
            return -1;
        else
            return fingerID;
    }

    private void UpdateTargetPosition()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetAnimation.gameObject.transform.position = targetPos;
        targetAnimation.StartAnimation();
    }
}
