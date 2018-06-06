using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A library over the player input
/// </summary>
public class PlayerInputComponent : MonoBehaviour {
    [HideInInspector]
    public KeyCode moveKey;

    void Awake () {
        SetupDefaultKeys();
    }

    private void SetupDefaultKeys()
    {
        moveKey = KeyCode.Mouse0;
    }
}
