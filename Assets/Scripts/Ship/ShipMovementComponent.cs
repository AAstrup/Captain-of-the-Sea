using System;
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
    private TimeScalesComponent timeScalesComponent;

    public void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
        shipConfiguration = GetComponent<ShipConfigurationComponent>();
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
    }

    internal void SetSpeed(float multiplier)
    {
        velocity = velocity.normalized * multiplier;
    }

    public void ApplyMovementInDirection(Vector2 direction)
    {
        var initialMagnitude = velocity.magnitude;
        var velocityToAdd = direction.normalized * Time.deltaTime * timeScalesComponent.GetGamePlayTimeScale() * shipConfiguration.GetAccelerationWithMultipliers();
        var sumVelocity = velocity + velocityToAdd;
        
        if(velocity.magnitude < shipConfiguration.GetMaxSpeedWithMultipliers())
        {
            velocity = sumVelocity;
        }
        else
        {
            velocity = (velocity.magnitude - (velocity.magnitude / shipConfiguration.brakeSpeedDivider) * timeScalesComponent.GetGamePlayTimeScale() * Time.deltaTime) * sumVelocity.normalized;
        }

        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update () {
        transform.position += new Vector3(velocity.x, velocity.y,0) * Time.deltaTime * timeScalesComponent.GetGamePlayTimeScale();
    }

    internal void Brake()
    {
        if(velocity.magnitude > 0.025f)
            ApplyMovementInDirection(-velocity);
    }
}
