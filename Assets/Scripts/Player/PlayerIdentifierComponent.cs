using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifierComponent : MonoBehaviour {
    [HideInInspector]
    public GameObject playerGameObject;

    void Awake () {
        playerGameObject = gameObject;	
	}
}
