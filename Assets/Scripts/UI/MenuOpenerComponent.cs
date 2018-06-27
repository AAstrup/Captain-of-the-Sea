using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Opens and closes windows of the menu
/// </summary>
public class MenuOpenerComponent : MonoBehaviour {

    public GameObject[] allPanels;

	public void OpenWindow(GameObject windowToOpen)
    {
        foreach (var item in allPanels)
        {
            if (item == windowToOpen)
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }
}
