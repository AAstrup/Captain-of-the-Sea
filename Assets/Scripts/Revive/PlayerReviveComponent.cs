using System;
using UnityEngine;

internal class PlayerReviveComponent : MonoBehaviour
{
    private TimeScalesComponent timeScalesComponent;
    private PlayerIdentifierComponent playerIdentifierComponent;

    public delegate void PlayerRevivedEvent();
    public PlayerRevivedEvent playerRevivedEvent;

    private void Awake()
    {
        timeScalesComponent = ComponentLocator.instance.GetDependency<TimeScalesComponent>();
        playerIdentifierComponent = ComponentLocator.instance.GetDependency<PlayerIdentifierComponent>();
    }

    internal void Revive()
    {
        timeScalesComponent.gamePlayStopped = false;
        playerIdentifierComponent.GetComponent<HealthComponent>().Revive();
        if (playerRevivedEvent != null)
            playerRevivedEvent();
    }
}