using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Call to save the player profile
/// </summary>
public class SavePlayerProfileComponent : MonoBehaviour {
	void Start () {
        ComponentLocator.instance.singleObjectInstanceReferences.SavePlayerProfile();		
	}
}
