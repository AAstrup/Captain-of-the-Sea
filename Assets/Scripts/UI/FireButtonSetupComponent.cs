using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonSetupComponent : MonoBehaviour {

    public Button button;

	void Start () {
        button.onClick.AddListener(delegate () { PlayerIdentifierComponent.playerGameObject.GetComponent<PlayerShootComponent>().Fire(); });
    }
}
