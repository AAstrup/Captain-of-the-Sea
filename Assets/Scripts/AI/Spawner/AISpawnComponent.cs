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
    public AISpawnDefinition[] enemyDefinitions;
    public delegate void ShipSpawned(HealthComponent healthComponent);
    public ShipSpawned shipSpawnedEvent;
    int shipsAlive;
    
    // Wave info
    int waveNr;
    int difficultyAccumilated;
    public DifficultyIncrease[] difficultyIncreases;
    public delegate void NewWave(int difficulty, DifficultyIncrease difficultyIncrease);
    public NewWave newWaveEvent;
    private PlayerIdentifierComponent playerIdentifierComponent;
    private CameraDirectorComponent cameraDirectorComponent;
    private static readonly float heightOffset = 11f;

    private void Awake()
    {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.menuStartComponent.gameStartedEvent += SpawnWave;
        playerIdentifierComponent = locator.componentReferences.playerIdentifierComponent;
        cameraDirectorComponent = locator.componentReferences.cameraDirectorComponent;
    }

    void SpawnWave()
    {
        transform.position = playerIdentifierComponent.playerGameObject.transform.position + new Vector3(0, heightOffset, 0) + cameraDirectorComponent.playerDistanceToCameraCenter;

        waveNr++;
        var difficultyIncrease = difficultyIncreases[waveNr % difficultyIncreases.Length];
        difficultyAccumilated += difficultyIncrease.increaseAmount;
        int difficultyThisWave = difficultyAccumilated + difficultyIncrease.currentWaveOnlyIncrease;
        if (newWaveEvent != null)
            newWaveEvent(difficultyThisWave, difficultyIncrease);

        for (int i = enemyDefinitions.Length - 1; i >= 0; i--)
        {
            while(enemyDefinitions[i].difficultyCost <= difficultyThisWave)
            {
                SpawnEnemy(enemyDefinitions[i].prefab);
                difficultyThisWave -= enemyDefinitions[i].difficultyCost;
            }
        }
    }

    private float cheapestEnemyCost()
    {
        return enemyDefinitions[0].difficultyCost;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        var pos = new Vector2(transform.position.x + spawnAreaRange.x * UnityEngine.Random.Range(-1f, 1f), transform.position.y + spawnAreaRange.y * UnityEngine.Random.Range(-1f, 1f));
        var spawn = Instantiate(prefab, pos, Quaternion.identity);
        var healthComponent = spawn.GetComponent<HealthComponent>();
        healthComponent.dieEvent += AIShipDead;
        shipsAlive++;
        if(shipSpawnedEvent != null)
            shipSpawnedEvent(healthComponent);
    }

    private void AIShipDead(HealthComponent victim)
    {
        shipsAlive--;
        if (shipsAlive == 0)
            SpawnWave();
    }
}
