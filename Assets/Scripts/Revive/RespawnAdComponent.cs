using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

/// <summary>
/// Respawns the player if an ad is shown succesfully
/// </summary>
[RequireComponent(typeof(Button))]
public class RespawnAdComponent : MonoBehaviour
{
    private PlayerIdentifierComponent playerIdentifierComponent;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
    }

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        playerIdentifierComponent = locator.componentReferences.GetDependency<PlayerIdentifierComponent>();
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
            button.interactable = false;
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                playerIdentifierComponent.GetComponent<PlayerReviveComponent>().Revive();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
