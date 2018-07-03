using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving the water to maintain a background
/// </summary>
public class WaterMoveComponent : MonoBehaviour {
    Transform cachedPlayerReference;
    private static readonly float maxXDistance = 0.64f * 4f;
    private static readonly float maxYDistance = 0.64f * 4f;
    int frame;
    private static readonly int checkTickRate = 30;

    void Start ()
    {
        cachedPlayerReference = ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>().playerGameObject.transform;
    }

    void Update () {
        if(cachedPlayerReference)
        { 
            frame++;
            if (frame % checkTickRate == 0)
                CheckForMovement();
        }
    }

    private void CheckForMovement()
    {
        var xDiff = cachedPlayerReference.position.x - transform.position.x;
        if (maxXDistance < Mathf.Abs(xDiff))
            MoveAlongXAxis(xDiff / Mathf.Abs(xDiff));

        var yDiff = cachedPlayerReference.position.y - transform.position.y;
        if (maxYDistance < Mathf.Abs(yDiff))
            MoveAlongYAxis(yDiff / Mathf.Abs(yDiff));
    }

    private void MoveAlongYAxis(float v)
    {
        transform.position += new Vector3(0,v * maxYDistance, 0);
    }

    private void MoveAlongXAxis(float v)
    {
        transform.position += new Vector3(v * maxXDistance, 0,0);
    }
}
