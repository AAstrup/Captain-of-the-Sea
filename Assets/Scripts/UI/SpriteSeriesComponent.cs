using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows one of series of sprites
/// </summary>
[RequireComponent(typeof(Image))]
public class SpriteSeriesComponent : MonoBehaviour {

    int spriteIndex;
    public Sprite[] sprites;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ShowNextImage()
    {
        spriteIndex = (spriteIndex + 1) % sprites.Length;
        image.sprite = sprites[spriteIndex];
    }
}
