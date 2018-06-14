using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for spawning and tracking the enemies alive
/// </summary>
public class AISpawnComponent : MonoBehaviour {
    public Vector2 spawnAreaRange;
    public AISpawnDefinition[] enemyDefinitions;
    int shipsAlive;
    int waveNr;
    int difficulty;
    public delegate void ShipSpawned(HealthComponent healthComponent);
    public ShipSpawned shipSpawnedEvent;
    public delegate void NewWave(int difficulty);
    public NewWave newWaveEvent;
    private static readonly float heightOffset = 11f;

    private void Start()
    {
        SingleComponentInstanceLocator.instance.menuStartComponent.gameStartedEvent += SpawnWave;
    }

    void SpawnWave()
    {
        transform.position = PlayerIdentifierComponent.playerGameObject.transform.position + new Vector3(0, heightOffset, 0) + SingleComponentInstanceLocator.instance.cameraDirectorComponent.playerDistanceToCameraCenter;

        difficulty = Mathf.FloorToInt(difficulty + 1 + (difficulty/10f));
        if (newWaveEvent != null)
            newWaveEvent(difficulty);

        int difficultyResourceLeft = difficulty;

        for (int i = enemyDefinitions.Length - 1; i >= 0; i--)
        {
            while(enemyDefinitions[i].difficultyCost <= difficultyResourceLeft)
            {
                SpawnEnemy(enemyDefinitions[i].prefab);
                difficultyResourceLeft -= enemyDefinitions[i].difficultyCost;
            }
        }
    }

    private float cheapestEnemyCost()
    {
        return enemyDefinitions[0].difficultyCost;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        var pos = new Vector2(transform.position.x + spawnAreaRange.x * Random.Range(-1f, 1f), transform.position.y + spawnAreaRange.y * Random.Range(-1f, 1f));
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
