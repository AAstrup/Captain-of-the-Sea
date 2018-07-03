using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for spawning and tracking the enemies alive
/// </summary>
public class AISpawnComponent : MonoBehaviour {
    // Spawn info
    public Vector2 spawnAreaRange;
    public AISpawnDefinition[] shipBase;
    public delegate void ShipSpawned(HealthComponent healthComponent);
    public ShipSpawned shipSpawnedEvent;
    [HideInInspector]
    public List<Transform> shipsAlive;

    // Wave info
    public int difficultyNr;
    public int waveNr;
    int difficultyAccumilated;
    public DifficultyIncrease[] difficultyIncreases;
    public delegate void NewWave(int difficulty, DifficultyIncrease difficultyIncrease);
    public NewWave newWaveEvent;
    private PlayerIdentifierComponent playerIdentifierComponent;
    private CameraDirectorComponent cameraDirectorComponent;
    private static readonly float heightOffset = 11f;

    private void Start()
    {
        shipsAlive = new List<Transform>();
        ComponentLocator.instance.GetDependency<MenuStartComponent>().gameStartedEvent += SpawnWave;
        playerIdentifierComponent = ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>();
        cameraDirectorComponent = ComponentLocator.instance.GetDependency<CameraDirectorComponent>();
    }

    void SpawnWave()
    {
        transform.position = playerIdentifierComponent.playerGameObject.transform.position + new Vector3(0, heightOffset, 0) + cameraDirectorComponent.playerDistanceToCameraCenter;

        waveNr++;
        var difficultyIncrease = difficultyIncreases[waveNr % difficultyIncreases.Length];
        difficultyAccumilated += difficultyIncrease.increaseAmount;
        int difficultyThisWave = difficultyAccumilated + difficultyIncrease.currentWaveOnlyIncrease;
        if (difficultyIncreases[waveNr % difficultyIncreases.Length].finalLevel)
            difficultyNr++;

        if (newWaveEvent != null)
            newWaveEvent(difficultyThisWave, difficultyIncrease);

        for (int i = shipBase.Length - 1; i >= 0; i--)
        {
            while(shipBase[i].difficultyCost <= difficultyThisWave)
            {
                SpawnEnemy(shipBase[i].prefab);
                difficultyThisWave -= shipBase[i].difficultyCost;
            }
        }
    }

    private float cheapestEnemyCost()
    {
        return shipBase[0].difficultyCost;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        var pos = new Vector2(transform.position.x + spawnAreaRange.x * UnityEngine.Random.Range(-1f, 1f), transform.position.y + spawnAreaRange.y * UnityEngine.Random.Range(-1f, 1f));
        var spawn = Instantiate(prefab, pos, Quaternion.identity);
        var healthComponent = spawn.GetComponent<HealthComponent>();
        healthComponent.dieEvent += AIShipDead;
        shipsAlive.Add(healthComponent.transform);
        if (shipSpawnedEvent != null)
            shipSpawnedEvent(healthComponent);
    }

    private void AIShipDead(HealthComponent victim)
    {
        shipsAlive.Remove(victim.transform);
        if (shipsAlive.Count == 0)
            SpawnWave();
    }
}
