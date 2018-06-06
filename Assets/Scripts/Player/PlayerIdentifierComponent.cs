using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifierComponent : MonoBehaviour {
    public static GameObject playerGameObject;

    void Awake () {
        playerGameObject = gameObject;	
	}
}
