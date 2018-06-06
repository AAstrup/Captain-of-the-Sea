using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for locking and showing the cooldown left on the fire button
/// </summary>
public class FireCooldownComponent : MonoBehaviour {

    public Text cooldownText;
    public Image cooldownOverlay;
    public Image cooldownFillOverlay;
    public Button fireButton;

    float cooldownLeft;
    public float cooldownTotal = 1f;

	void Awake () {
        cooldownLeft = 0f;
        StopCooldown();
        fireButton.onClick.AddListener(delegate () { StartCooldown(); });
    }
	
	void Update () {
        if (cooldownLeft >= 0f)
        {
            UICooldownUpdate();
            if (cooldownLeft <= 0f)
            {
                StopCooldown();
            }
        }
    }

    private void UICooldownUpdate()
    {
        cooldownLeft -= TimeScalesComponent.instance.gamePlayTimeScale * Time.deltaTime;
        cooldownFillOverlay.fillAmount = cooldownLeft / cooldownTotal;

        string cooldownToString = null;
        if (cooldownLeft > 1f)
        {
            cooldownToString = Mathf.FloorToInt(cooldownLeft).ToString();
        }
        else
        {
            cooldownToString = ((Mathf.FloorToInt(cooldownLeft * 10f)) / 10f).ToString();
        }
        cooldownText.text = cooldownToString; 
    }

    private void StopCooldown()
    {
        cooldownText.gameObject.SetActive(false);
        cooldownOverlay.gameObject.SetActive(false);
        cooldownFillOverlay.gameObject.SetActive(false);
        fireButton.interactable = true;
    }

    void StartCooldown()
    {
        cooldownText.gameObject.SetActive(true);
        cooldownOverlay.gameObject.SetActive(true);
        cooldownFillOverlay.gameObject.SetActive(true);
        fireButton.interactable = false;

        cooldownFillOverlay.fillAmount = 1f;
        cooldownLeft = cooldownTotal;
    }
}
