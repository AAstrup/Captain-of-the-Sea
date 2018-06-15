using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates an health bar to match the players health
/// </summary>
public class PlayerHealthBarComponent : MonoBehaviour {
    public Image healthBar;
    private float startHealth;

    void Start () {
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
	}

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        var playerHealthComponent = locator.componentReferences.playerIdentifierComponent.playerGameObject.GetComponent<HealthComponent>();
        startHealth = playerHealthComponent.health;
        playerHealthComponent.healthChangedEvent += UpdateHealthUI;
    }

    private void UpdateHealthUI(HealthComponent victim, float damage, float healthLeft)
    {
        healthBar.fillAmount = healthLeft / startHealth;
    }
}
