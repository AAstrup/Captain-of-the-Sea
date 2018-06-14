using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps references to single instance components
/// </summary>
public class SingleComponentInstanceLocator : MonoBehaviour {
    public static SingleComponentInstanceLocator instance;

    public AISpawnComponent aISpawnComponent;
    public AudioLevelComponent audioLevelComponent;
    public SoundEffectPoolComponent soundEffectPoolComponent;
    public CameraDirectorComponent cameraDirectorComponent;
    public ParticlePoolComponent particlePoolComponent;
    public SpriteEffectsComponent spriteEffectsComponent;
    public TimeScalesComponent timeScalesComponent;
    public MenuStartComponent menuStartComponent;
    public PlayerScoreComponent playerScoreComponent;

    void Awake () {
        instance = this;
    }
}
