using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Used to play sound
/// Used as a singleton as one always has to be present
/// </summary>
public class SoundManagerComponent : MonoBehaviour {

    public static SoundManagerComponent instance;
    public SoundManagerModel[] soundManagerModel;
    public enum SoundsType { HitShip }
    public int audioSourcePool = 30;
    [HideInInspector]
    public List<AudioSource> pool;

    private void Awake()
    {
        instance = this;
        pool = new List<AudioSource>();
        for (int i = 0; i < audioSourcePool; i++)
        {
            var spawn = CreateAudioSource();
            pool.Add(spawn);
            spawn.transform.SetParent(transform);
            spawn.name = "AudioSource";
        }
    }

    private AudioSource CreateAudioSource()
    {
        GameObject gmj = new GameObject();
        var source = gmj.AddComponent<AudioSource>();
        return source;
    }

    public void PlaySound(SoundsType soundsType)
    {
        var model = soundManagerModel.First(x => x.soundsType == soundsType);
        var item = pool[0];
        item.clip = model.audioClip;
        item.Play();

        pool.RemoveAt(0);
        pool.Add(item);
    }
}
