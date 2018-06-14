using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootRandomComponent : MonoBehaviour {

    ShootComponent[] shootComponents;
    public float timeBetweenShotsMin = 1f;
    public float timeBetweenShotsMax = 1.5f;
    float timeLeft;

    void Awake()
    {
        shootComponents = GetComponentsInChildren<ShootComponent>();
        timeLeft = Random.Range(timeBetweenShotsMin, timeBetweenShotsMax);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime * SingleComponentInstanceLocator.instance.timeScalesComponent.gamePlayTimeScale; ;
        if (timeLeft < 0)
        {
            timeLeft = Random.Range(timeBetweenShotsMin, timeBetweenShotsMax);
            foreach (var shootComponent in shootComponents)
            {
                shootComponent.Fire();
            }
        }
    }
}
