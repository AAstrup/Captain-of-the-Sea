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
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        button.onClick.AddListener(delegate () { locator.componentReferences.playerIdentifierComponent.playerGameObject.GetComponent<PlayerShootComponent>().Fire(); });
    }
}
