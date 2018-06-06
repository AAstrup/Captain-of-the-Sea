﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component used to create a animation with a larger size decrementing to normal size
/// </summary>
public class AnimationPopComponent : MonoBehaviour {

    private float timeSincePress;
    private Vector3 normalScale;
    public AnimationCurve sizeIncrement;

    private void Awake()
    {
        normalScale = transform.localScale;
    }

    public void StartAnimation()
    {
        timeSincePress = 0f;
    }

	void Update () {
        transform.localScale = normalScale * (1f + sizeIncrement.Evaluate(timeSincePress));
        timeSincePress += Time.deltaTime * TimeScalesComponent.instance.gamePlayTimeScale;
    }
}