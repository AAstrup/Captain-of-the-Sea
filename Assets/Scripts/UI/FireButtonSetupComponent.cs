using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonSetupComponent : MonoBehaviour {

    public Button button;
    private PlayerIdentifierComponent playerIdentifierComponent;

    private void Awake()
    {
        SingleComponentInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback);
    }

    private void DependencyCallback(SingleComponentInstanceLocator locator)
    {
        button.onClick.AddListener(delegate () { locator.componentReferences.playerIdentifierComponent.playerGameObject.GetComponent<PlayerShootComponent>().Fire(); });
    }
}
