using System;
using UnityEngine;

/// <summary>
/// Registers a type of particlesystemtype to the ParticlePoolComponent
/// </summary>
public class ParticleSystemRegisterComponent : MonoBehaviour
{
    public ParticlePoolComponent.ParticleSystemType particleSystemType;
    public int defaultParticleAmountMin = 30;
    public int defaultParticleAmountMax = 30;
    public Vector3 rotationVector;
    [HideInInspector]
    public Vector3 rotationOffset;
    [HideInInspector]
    public ParticleSystem particleSystemContained;

    private void Awake()
    {
        rotationOffset = transform.eulerAngles;
        particleSystemContained = GetComponent<ParticleSystem>();
    }
}