using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fire a particle system at a position
/// Serves only as a tool, and is a singleton to be more easily accessed
/// </summary>
public class ParticlePoolComponent : MonoBehaviour {
    public static ParticlePoolComponent instance;
    public enum ParticleSystemType { CannonFire, ShipHit }
    Dictionary<ParticleSystemType, ParticleSystemRegisterComponent> particlesSystems;

    void Awake () {
        instance = this;

        particlesSystems = new Dictionary<ParticleSystemType, ParticleSystemRegisterComponent>();
        foreach (var item in GetComponentsInChildren<ParticleSystemRegisterComponent>())
        {
            particlesSystems.Add(item.particleSystemType, item);
        }
	}

    /// <summary>
    /// Fires the particles default amount of particles at the given position and rotation
    /// </summary>
    /// <param name="systemType">Type of particle</param>
    /// <param name="position">Position to spawn particle</param>
    /// <param name="rotation">Rotation of particleSystem</param>
	public void FireParticleSystem(ParticleSystemType systemType, Vector3 position, float rotation)
    {
        var system = particlesSystems[systemType];
        FireParticleSystem(systemType, position, rotation, system.defaultParticleAmount);
    }

    /// <summary>
    /// Fires the particles of the type at a position and rotation
    /// </summary>
    /// <param name="systemType">Type of particle</param>
    /// <param name="position">Position to spawn particle</param>
    /// <param name="rotation">Rotation of particleSystem</param>
    /// <param name="amount">Amount of particles to spawn</param>
    public void FireParticleSystem(ParticleSystemType systemType, Vector3 position, float rotation,int amount)
    {
        var system = particlesSystems[systemType];

        system.transform.position = position;
        system.transform.eulerAngles = (system.rotationVector * rotation) + system.rotationOffset;

        if (amount == 0)
            amount = system.defaultParticleAmount;

        system.particleSystemContained.Emit(amount);
    }
}
