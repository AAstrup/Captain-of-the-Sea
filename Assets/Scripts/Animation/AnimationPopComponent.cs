using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component used to create a animation with a larger size decrementing to normal size
/// </summary>
public class AnimationPopComponent : MonoBehaviour, IAnimationObject {

    private float timeSinceTriggered;
    private Vector3 normalScale;
    public AnimationCurve sizeIncrement;
    private TimeScalesComponent timeScalesComponent;

    private void Awake()
    {
        timeSinceTriggered = sizeIncrement.length;
        normalScale = transform.localScale;
        timeScalesComponent = ComponentLocator.instance.GetDependency<TimeScalesComponent>();
    }

    public void StartAnimation()
    {
        timeSinceTriggered = 0f;
    }

	void Update () {
        if (timeSinceTriggered > sizeIncrement.length)
            return;
        transform.localScale = normalScale * (1f + sizeIncrement.Evaluate(timeSinceTriggered));
        timeSinceTriggered += Time.deltaTime;
    }
}
