﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ship movement component
/// Does not listen to input
/// </summary>
[RequireComponent(typeof(ShipConfigurationComponent))]
public class ShipMovementComponent : MonoBehaviour
{
    Vector2 velocity = Vector2.zero;
    private ShipConfigurationComponent shipConfiguration;

    public void Awake()
    {
        shipConfiguration = GetComponent<ShipConfigurationComponent>();
    }

    public void ApplyMovementInDirection(Vector2 direction)
    {
        velocity += direction.normalized * Time.deltaTime * TimeScalesComponent.instance.gamePlayTimeScale * shipConfiguration.accelerateSpeed;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(velocity.magnitude > shipConfiguration.maxSpeed)
        {
            velocity = velocity.normalized * shipConfiguration.maxSpeed;
        }
    }

	void Update () {
        transform.position += new Vector3(velocity.x, velocity.y,0) * Time.deltaTime * TimeScalesComponent.instance.gamePlayTimeScale;
    }

    internal void Brake()
    {
        if(velocity.magnitude > 0.025f)
            ApplyMovementInDirection(-velocity);
    }
}