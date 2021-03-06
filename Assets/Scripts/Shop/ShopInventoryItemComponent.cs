﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for showing an Item in the inventory UI
/// </summary>
[RequireComponent(typeof(Button))]
public class ShopInventoryItemComponent : MonoBehaviour {
    public Button button;
    public Image image;
    public Text levelText;
    private bool isPicked;
    private PlayerItemInventory inventory;
    private PlayerItem item;
    private static readonly Color pickedColor = new Color(1f,1f,1f);
    private static readonly Color notPickedColor = new Color(0.25f, 0.25f, 0.25f);

    private void Awake()
    {
        button.onClick.AddListener(ItemActivated);
    }

    private void ItemActivated()
    {
        if(item.isActiveItem)
            inventory.DeactivateItem(item);
        else
            inventory.ActivateItem(item);
        SetIsPicked(!isPicked);
        ComponentLocator.instance.singleObjectInstanceReferences.SavePlayerProfile();
    }

    internal void ShowNoItem()
    {
        gameObject.SetActive(false);
    }

    internal void ShowItem(PlayerItemInventory inventory, PlayerItem playerItem, IShopItemModel itemModel)
    {
        this.inventory = inventory;
        item = playerItem;
        SetIsPicked(playerItem.isActiveItem);
        gameObject.SetActive(true);
        image.sprite = itemModel.GetItemSprite();
        levelText.text = playerItem.itemLevel.ToString();
    }
     
    private void SetIsPicked(bool isPicked)
    {
        this.isPicked = isPicked;
        if (isPicked)
        {
            image.color = pickedColor;
        }
        else
        {
            image.color = notPickedColor;
        }
    }
}
