﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for muting or unmuting the music / sound
/// </summary>
public class AudioMuteComponent : MonoBehaviour {

    public AudioLevelComponent.AudioChannelType audioChannelType;
    bool muted = false;

    public void ToggleAudioMute()
    {
        muted = !muted;
        SetAudioLevel();
    }

    private void SetAudioLevel()
    {
        SingleComponentInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleComponentInstanceLocator locator)
    {
        locator.componentReferences.audioLevelComponent.MuteAudioLevel(audioChannelType, muted);
    }
}
