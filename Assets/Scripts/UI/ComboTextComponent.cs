using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows and keeps track of the players combo
/// </summary>
[RequireComponent(typeof(Text))]
public class ComboTextComponent : MonoBehaviour {

    private Text textComp;
    private static readonly int minimumComboToShowText = 2;
    private static readonly float fadeTimeTotal = 0.75f;
    private float fadeTimeLeft;
    int currentCombo;
    private TimeScalesComponent timeScalesComponent;
    public AnimationCurve textSizeWithCombo;

    void Awake () {
        textComp = GetComponent<Text>();
        SingleObjectInstanceLocator.SubscribeToDependenciesCallback(SetupDependencies);
	}

    private void SetupDependencies(SingleObjectInstanceLocator locator)
    {
        locator.componentReferences.aISpawnComponent.shipSpawnedEvent += shipSpawnSetup;
        timeScalesComponent = locator.componentReferences.timeScalesComponent;
    }

    private void shipSpawnSetup(HealthComponent healthComponent)
    {
        healthComponent.healthChangedEvent += IncreaseCombo;
    }

    private void IncreaseCombo(HealthComponent victim, float damage, float healthLeft)
    {
        currentCombo = currentCombo + 1;
        textComp.text = "x" + currentCombo;
        textComp.transform.position = new Vector3(victim.transform.position.x, victim.transform.position.y + 1f, victim.transform.position.z);
        fadeTimeLeft = fadeTimeTotal;

        if (!textComp.enabled && currentCombo >= minimumComboToShowText)
            textComp.enabled = true;
    }

    void Update ()
    {
        fadeTimeLeft -= timeScalesComponent.GetGamePlayTimeScale() * Time.deltaTime;

        if (!textComp.enabled)
            return;

        if (fadeTimeLeft > 0)
        {
            var diff = fadeTimeLeft / fadeTimeTotal;
            textComp.transform.localScale = new Vector3(diff, diff, diff);
        }
        else
        {
            currentCombo = 0;
            textComp.enabled = false;
        }
    }
}
