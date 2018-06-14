using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Used to play effect sounds that can occour multiple times in the elapsed time it takes to play the sound (Done by pooling AudioSources)
/// Used as a singleton as one always has to be present
/// </summary>
public class SoundEffectPoolComponent : MonoBehaviour {

    public GameObject audioPrefab;
    public static SoundEffectPoolComponent instance;
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
        return Instantiate(audioPrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
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
