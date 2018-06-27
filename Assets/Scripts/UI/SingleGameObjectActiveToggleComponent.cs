using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toggles between gameobject, to ensure only one is activated
/// </summary>
public class SingleGameObjectActiveToggleComponent : MonoBehaviour {

    public GameObject[] gameObjects;

    public void ActivateGameObject(GameObject gmj)
    {
        foreach (var item in gameObjects)
        {
            if (item != gmj)
                item.SetActive(false);
            else
                item.SetActive(true);
        }
    }
}
