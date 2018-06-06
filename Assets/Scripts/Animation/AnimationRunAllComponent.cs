using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRunAllComponent : MonoBehaviour {
    public bool repeat;
    public float repeatTime = 0f;
    float timeLeftBefore;
    private IAnimationObject[] animationComponents;

    void Start () {
        animationComponents = GetComponents<IAnimationObject>();
        foreach (var item in animationComponents)
        {
            item.StartAnimation();
        }
    }

    private void Update()
    {
        if (!repeat)
            return;

        timeLeftBefore += Time.deltaTime;
        if(timeLeftBefore >= repeatTime)
        {
            timeLeftBefore -= repeatTime;
            foreach (var item in animationComponents)
            {
                item.StartAnimation();
            }
        }
    }
}
