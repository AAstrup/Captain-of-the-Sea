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
        locator.componentReferences.aISpawnComponent.newWaveEvent += NewWave;
    }

    private void Update()
    {
        if(targetPosition.HasValue)
            cameraPosition = (targetPosition.Value - cameraPosition)/2f + cameraPosition;
    }

    private void NewWave(int difficulty, DifficultyIncrease difficultyIncrease)
    {
        SetCameraTargetPosition(playerIdentifierComponent.playerGameObject.transform.position);
    }

    void SetCameraTargetPosition(Vector3 position)
    {
        targetPosition = new Vector3(position.x, position.y, cameraPosition.z) + playerDistanceToCameraCenter;
    }
}
