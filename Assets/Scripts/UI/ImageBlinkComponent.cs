using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Makes an image blink
/// Resets alpha to full when disabled
/// </summary>
[RequireComponent(typeof(Image))]
public class ImageBlinkComponent : MonoBehaviour {
    [HideInInspector]
    public Image img;
    private static readonly float blinkSpeed = 2f;

    void Awake () {
        img = GetComponent<Image>();
    }
	
	void Update ()
    {
        float alpha = (Time.time/ blinkSpeed) % 2f;

        if (alpha > 1f)
        {
            alpha = 2f - alpha;
        }

        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
    }

    private void OnDisable()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    }
}
