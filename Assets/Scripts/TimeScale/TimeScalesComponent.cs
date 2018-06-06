using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains custom timescales to keep the original timescale for anything depending on it.
/// </summary>
public class TimeScalesComponent : MonoBehaviour {

    public static TimeScalesComponent instance;
    [HideInInspector]
    public float gamePlayTimeScale = 1f;

    void Awake () {
        instance = this;
    }
}
