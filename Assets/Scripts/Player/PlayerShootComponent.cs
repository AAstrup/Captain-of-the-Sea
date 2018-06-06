using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that listens to player fire key
/// </summary>
[RequireComponent(typeof(PlayerInputComponent))]
public class PlayerShootComponent : MonoBehaviour {

    PlayerInputComponent playerInputComponent;
    ShootComponent[] shootComponents;
    public delegate void FireEvent();
    public FireEvent fireEvent;

    void Awake () {
        playerInputComponent = GetComponent<PlayerInputComponent>();
        shootComponents = GetComponentsInChildren<ShootComponent>();
    }
	
    public void Fire()
    {
        fireEvent();
        foreach (var shootComponent in shootComponents)
        {
            shootComponent.Fire();
        }
    }
}
