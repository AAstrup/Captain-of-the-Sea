using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fire a particle system at a position
/// Serves only as a tool
/// </summary>
public class ParticlePoolComponent : MonoBehaviour {
    public enum ParticleSystemType { CannonFire, ShipHit, ShipDead, CannonBallInAir, Explosion }
    Dictionary<ParticleSystemType, List<ParticleSystemRegisterComponent>> particlesSystems;

    void Awake () {

        particlesSystems = new Dictionary<ParticleSystemType, List<ParticleSystemRegisterComponent>>();
        foreach (var item in GetComponentsInChildren<ParticleSystemRegisterComponent>())
        {
            if (!particlesSystems.ContainsKey(item.particleSystemType))
                particlesSystems.Add(item.particleSystemType, new List<ParticleSystemRegisterComponent>());
            particlesSystems[item.particleSystemType].Add(item);
        }
	}

    /// <summary>
    /// Fires the particles of the type with a default amount of particles at the given position and rotation
    /// </summary>
    /// <param name="systemType">Type of particle</param>
    /// <param name="position">Position to spawn particle</param>
    /// <param name="rotation">Rotation of particleSystem</param>
	public void FireParticleSystem(ParticleSystemType systemType, Vector3 position, float rotation)
    {
        var systems = particlesSystems[systemType];
        foreach (var item in systems)
        {
            FireParticlesSystem(item, position, rotation, Random.Range(item.defaultParticleAmountMin, item.defaultParticleAmountMax));
        }
    }

    /// <summary>
    /// Fires the particlesystems of the type at a position and rotation
    /// </summary>
    /// <param name="systemType">Type of particle</param>
    /// <param name="position">Position to spawn particle</param>
    /// <param name="rotation">Rotation of particleSystem</param>
    /// <param name="amount">Amount of particles to spawn</param>
    public void FireParticleSystem(ParticleSystemType systemType, Vector3 position, float rotation,int amount)
    {
        var systems = particlesSystems[systemType];
        foreach (var item in systems)
        {
            FireParticlesSystem(item, position, rotation, amount);
        }
    }

    /// <summary>
    /// Fires a ParticleSystemRegisterComponent at a position with a rotation
    /// </summary>
    /// <param name="system">ParticleSystemRegisterComponent to fire</param>
    /// <param name="position">Position to set it to</param>
    /// <param name="rotation">Rotation to set it to</param>
    /// <param name="amount">Amount to fire</param>
    private void FireParticlesSystem(ParticleSystemRegisterComponent system, Vector3 position, float rotation, int amount)
    {
        system.transform.position = position;
        system.transform.eulerAngles = (system.rotationVector * rotation) + system.rotationOffset;

        if (amount == 0)
            amount = Random.Range(system.defaultParticleAmountMin, system.defaultParticleAmountMax);

        system.particleSystemContained.Emit(amount);
    }
}
