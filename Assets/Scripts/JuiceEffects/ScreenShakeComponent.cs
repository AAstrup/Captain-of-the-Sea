using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for screen shake which is camera movement 
/// </summary>
[RequireComponent(typeof(CameraDirectorComponent))]
public class ScreenShakeComponent : MonoBehaviour {

    // To prevent from activating on start it is by default set to 100
    float timeSinceShake = 100f;
    CameraDirectorComponent cameraDirectorComponent;
    public Vector2 shakeMaxAmount = new Vector2(1, 1);
    public AnimationCurve shakeAmountOfTime;

    private void Start()
    {
        cameraDirectorComponent = GetComponent<CameraDirectorComponent>();
        ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>().playerGameObject.GetComponent<HealthComponent>().healthChangedEvent += delegate (HealthComponent victim, float damage, float healthLeft) { StartShake(); };
        var test = ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>().playerGameObject.GetComponent<AbilityPlayerInputComponent>();
        test.abilityTriggerEvent += delegate (IItemAbilityComponent usedAbility, IItemAbilityComponent nextAbility) { StartShake(); };
        ComponentLocator.instance.GetDependency<AISpawnComponent>().shipSpawnedEvent += ShipSpawned;
    }

    private void ShipSpawned(HealthComponent healthComponent)
    {
        healthComponent.healthChangedEvent += delegate (HealthComponent victim, float damage, float healthLeft) { StartShake(); };
    }

    private void StartShake()
    {
        timeSinceShake = 0f;
    }

	void Update () {
        var timeValue = shakeAmountOfTime.Evaluate(timeSinceShake);
        transform.position = new Vector3(
            cameraDirectorComponent.cameraPosition.x + shakeMaxAmount.x * timeValue * GetRandomSign(),
            cameraDirectorComponent.cameraPosition.y + shakeMaxAmount.y * timeValue * GetRandomSign(),
            cameraDirectorComponent.cameraPosition.z);
        timeSinceShake += Time.deltaTime;
    }

    /// <summary>
    /// Returns either 1 or -1
    /// </summary>
    /// <returns></returns>
    private int GetRandomSign()
    {
        return UnityEngine.Random.Range(0f, 100f) < 50f ? 1 : -1;
    }
}
