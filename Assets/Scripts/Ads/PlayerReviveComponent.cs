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
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
        playerIdentifierComponent = locator.componentReferences.playerIdentifierComponent;
    }

    internal void Revive()
    {
        timeScalesComponent.gamePlayStopped = false;
        playerIdentifierComponent.GetComponent<HealthComponent>().Revive();
        if (playerRevivedEvent != null)
            playerRevivedEvent();
    }
}