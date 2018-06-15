using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the position of the camera
/// Attached to camera
/// Other components can read the center and offset, but this script holds the position
/// </summary>
public class CameraDirectorComponent : MonoBehaviour {

    [HideInInspector]
    public Vector3 cameraPosition;
    private Vector3? targetPosition = null;
    [HideInInspector]
    public Vector3 playerDistanceToCameraCenter;
    private PlayerIdentifierComponent playerIdentifierComponent;
    private AISpawnComponent aISpawnComponent;
    private TimeScalesComponent timeScalesComponent;
    public float panSpeed = 1f;
    private float enemyShipWeight = 0.666f;

    void Awake () {
        cameraPosition = transform.position;
    }

    private void Start()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        playerIdentifierComponent = locator.componentReferences.playerIdentifierComponent;
        playerDistanceToCameraCenter = transform.position - playerIdentifierComponent.playerGameObject.transform.position;
        aISpawnComponent = locator.componentReferences.aISpawnComponent;
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
    }

    private void Update()
    {
        var playerPos = new Vector3(playerIdentifierComponent.transform.position.x, playerIdentifierComponent.transform.position.y, cameraPosition.z);
        targetPosition = playerPos;

        if (aISpawnComponent.shipsAlive.Count > 0)
        {
            Vector3 accumilatedShipPositions = Vector3.zero;
            Vector3 averageShipPosition = Vector3.zero;
            foreach (var item in aISpawnComponent.shipsAlive)
            {
                accumilatedShipPositions += item.position - playerPos;
            }
            {
                averageShipPosition = (accumilatedShipPositions / aISpawnComponent.shipsAlive.Count) * enemyShipWeight;
            }
            targetPosition += new Vector3(averageShipPosition.x, averageShipPosition.y, 0f);
        }

        if (targetPosition.HasValue)
            cameraPosition = (targetPosition.Value - cameraPosition) * timeScalesComponent.GetGamePlayTimeScale() * Time.deltaTime * panSpeed + cameraPosition;
    }
}
