using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains custom timescales to keep the original timescale for anything depending on it.
/// </summary>
public class TimeScalesComponent : MonoBehaviour {

    [HideInInspector]
    float gamePlayTimeScale = 1f;
    public bool gamePlayStopped;

    public float GetGamePlayTimeScale()
    {
        if (gamePlayStopped)
            return 0f;
        return gamePlayTimeScale;
    }

    public void SetGamePlayTimeScale(float v)
    {
        gamePlayTimeScale = v;
    }
}
