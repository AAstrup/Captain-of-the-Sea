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
    private TimeScalesComponent timeScalesComponent;
    private ShopItemLibraryComponent library;

    Dictionary<IItemAbilityComponent, float> abilityFireTime;

    void Awake () {
        StopCooldown();
        fireButton.onClick.AddListener(delegate () { StartCooldown(); });
        abilityFireTime = new Dictionary<IItemAbilityComponent, float>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(DependencyCallback, this);
    }

    private void DependencyCallback(SingleObjectInstanceLocator locator)
    {
        timeScalesComponent = locator.componentReferences.GetDependency<TimeScalesComponent>();
        library = locator.componentReferences.GetDependency<ShopItemLibraryComponent>();
        locator.componentReferences.GetDependency<PlayerIdentifierComponent>().GetComponent<AbilityPlayerInputComponent>().abilityTriggerEvent += abilityFired;
    }

    private void abilityFired(IItemAbilityComponent usedItemAbility, IItemAbilityComponent nextItemAbility)
    {
        if (!abilityFireTime.ContainsKey(usedItemAbility))
            abilityFireTime.Add(usedItemAbility, timeScalesComponent.gamePlayTimeTime);
        else
            abilityFireTime[usedItemAbility] = timeScalesComponent.gamePlayTimeTime;

        if (abilityFireTime.ContainsKey(nextItemAbility))
            cooldownLeft = Mathf.Max(0f, abilityFireTime[nextItemAbility] + nextItemAbility.GetModel().cooldown - timeScalesComponent.gamePlayTimeTime);
        else
            cooldownLeft = 0f;
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
        cooldownLeft -= timeScalesComponent.GetGamePlayTimeScale() * Time.deltaTime;
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
