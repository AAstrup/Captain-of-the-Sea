using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the level of sound of each type of audio
/// Is a singleton for components to get the level of sound
/// </summary>
public class AudioLevelComponent : MonoBehaviour {

    public static AudioLevelComponent instance;
    public enum AudioChannelType { Sound, Music }
    Dictionary<AudioChannelType, float> audioLevelPercentage;
    static readonly float maxAudioLevel = 1f;
    static readonly float minAudioLevel = 0f;

    public Action<AudioChannelType> audioLevelChangedEvent;

    void Awake ()
    {
        audioLevelPercentage = new Dictionary<AudioChannelType, float>();
        foreach (AudioChannelType item in Enum.GetValues(typeof(AudioChannelType)))
        {
            audioLevelPercentage.Add(item, maxAudioLevel);
        }
        instance = this;
    }

    internal void MuteAudioLevel(AudioChannelType audioChannelType, bool muted)
    {
        if (muted)
        {
            SetAudioLevel(audioChannelType, minAudioLevel);
        }
        else
        {
            SetAudioLevel(audioChannelType, maxAudioLevel);
        }
    }

    internal void SetAudioLevel(AudioChannelType audioChannelType, float v)
    {
        audioLevelPercentage[audioChannelType] = v;
        if (audioLevelChangedEvent != null)
            audioLevelChangedEvent(audioChannelType);
    }

    public float GetAudioVolume(AudioChannelType audioChannelType)
    {
        return audioLevelPercentage[audioChannelType];
    }
}
