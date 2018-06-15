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
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
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
