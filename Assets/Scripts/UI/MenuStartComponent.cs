using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Starts the game when StartMap is called
/// Is a singleton to allow subscription
/// </summary>
public class MenuStartComponent : MonoBehaviour {

    public static MenuStartComponent instance;
    public GameObject[] gameObjectsToActivateOnStart;
    public Behaviour[] componentsToActivateOnStart;
    public GameObject[] gameObjectsToDisable;
    public delegate void GameStartedEvent();
    public GameStartedEvent gameStartedEvent;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (var item in gameObjectsToActivateOnStart)
        {
            item.SetActive(false);
        }
        foreach (var item in componentsToActivateOnStart)
        {
            item.enabled = false;
        }
    }

    public void StartMap()
    {
        foreach (var item in gameObjectsToActivateOnStart)
        {
            item.SetActive(true);
        }
        foreach (var item in componentsToActivateOnStart)
        {
            item.enabled = true;
        }
        foreach (var item in gameObjectsToDisable)
        {
            item.SetActive(false);
        }

        if (gameStartedEvent != null)
            gameStartedEvent();
    }
}
