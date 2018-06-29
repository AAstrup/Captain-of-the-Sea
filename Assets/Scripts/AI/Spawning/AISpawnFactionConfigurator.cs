using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes configuration/stats on a ship spawned by the AISpawnComponent attached to the gmj
/// </summary>
[RequireComponent(typeof(AISpawnComponent))]
public class AISpawnFactionConfigurator : MonoBehaviour {

    public List<FactionConfiguration> factionConfigurations;

    private void Awake()
    {
        var spawner = GetComponent<AISpawnComponent>();
        spawner.shipSpawnedEvent += SetupShipFaction;
    }

    private void SetupShipFaction(HealthComponent healthComponent)
    {
        if (factionConfigurations.Count > 0) {
            FactionConfiguration randomFaction = factionConfigurations[UnityEngine.Random.Range(0, factionConfigurations.Count)];
            var shipConfig = healthComponent.GetComponent<ShipConfigurationComponent>();
            shipConfig.healthComponent.health += shipConfig.healthComponent.health * randomFaction.healthMultiplierIncrease;
            shipConfig.accelerateSpeed += shipConfig.accelerateSpeed * randomFaction.accelerateSpeedMultiplierIncrease;
            shipConfig.maxSpeed += shipConfig.maxSpeed * randomFaction.maxSpeedMultiplierIncrease;
            // to prevent firing each second, a small safety check is put in places, though this is still a dirty implementation
            var aiShipConfig = healthComponent.GetComponent<AIShipConfigurationComponent>();
            aiShipConfig.fireSpeed = Mathf.Max(0.25f, aiShipConfig.fireSpeed / (1 + randomFaction.fireSpeedMultiplierIncrease));
            foreach (var item in shipConfig.flags)
            {
                item.sprite = randomFaction.flagSprite;
            }
        }
    }

    /// <summary>
    /// Only used by this class, public however to show up in inspector
    /// Represents a faction and the configuration/stat changes it applies to a boat
    /// </summary>
    [Serializable]
    public class FactionConfiguration
    {
        public Sprite flagSprite;
        public float healthMultiplierIncrease = 0f;
        public float fireSpeedMultiplierIncrease = 0f;
        public float accelerateSpeedMultiplierIncrease = 0f;
        public float maxSpeedMultiplierIncrease = 0f;
    };
}
