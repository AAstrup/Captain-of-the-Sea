using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRunAllComponent : MonoBehaviour {
    public bool repeat;
    public float repeatTime = 0f;
    public float delayTime = 0f;
    float triggerTimeLeft;
    private IAnimationObject[] animationComponents;

    void Start () {
        animationComponents = GetComponents<IAnimationObject>();
        triggerTimeLeft = repeatTime + delayTime;
    }

    private void Update()
    {
        triggerTimeLeft -= Time.deltaTime;
        if(triggerTimeLeft < 0f)
        {
            triggerTimeLeft = repeatTime;
            foreach (var item in animationComponents)
            {
                item.StartAnimation();
            }

            if (!repeat)
                enabled = false;
        }
    }
}
