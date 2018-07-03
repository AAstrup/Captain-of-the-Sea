using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for loading scenes
/// </summary>
public class SceneLoadComponent : MonoBehaviour {

    public void ReloadScene()
    {
        ComponentLocator.instance.ReloadScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
