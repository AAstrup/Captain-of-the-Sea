using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads the level of an type of audio and sets the AudioSource components level to that
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioSourceLevelSetterComponent : MonoBehaviour {

    public AudioLevelComponent.AudioChannelType audioChannelType;
    private AudioSource audioSource;
    private AudioLevelComponent audioLevelComponent;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start () {
        SingleComponentInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleComponentInstanceLocator locator)
    {
        audioLevelComponent = locator.componentReferences.audioLevelComponent;
        audioLevelComponent.audioLevelChangedEvent += UpdateAudioLevel;
        UpdateAudioLevel(audioChannelType);
    }

    private void UpdateAudioLevel(AudioLevelComponent.AudioChannelType audioChannelType)
    {
        if(this.audioChannelType == audioChannelType)
        {
            audioSource.volume = audioLevelComponent.GetAudioVolume(audioChannelType);
        }
    }
}
