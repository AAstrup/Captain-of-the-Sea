using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPPurchaseCanvasComponent : MonoBehaviour {

    public GameObject menuBackground;
    public GameObject popupBackground;
    public Button closeButton;

    public void OpenShopAsPopUp()
    {
        gameObject.SetActive(true);
        menuBackground.SetActive(false);
        popupBackground.SetActive(true);
        closeButton.gameObject.SetActive(true);
    }

    public void OpenShopAsFullScreen()
    {
        gameObject.SetActive(true);
        menuBackground.SetActive(true);
        popupBackground.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
