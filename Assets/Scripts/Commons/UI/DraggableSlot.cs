using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DraggableSlot : MonoBehaviour {
    public InventorySlot parentSlot;
    public TextMeshProUGUI amountTXT;
    public Image itemImage;
    public Item currentItem;
    public void Setup(Item item, InventorySlot slot) {
        parentSlot = slot;
        currentItem = item;
        itemImage.sprite = item.itemSprite;
        if (item.amount > 1) {
            amountTXT.enabled = true;
            amountTXT.text = item.amount.ToString();
        }
    }

    public void UpdateAmount() {
        currentItem = parentSlot.item;
        amountTXT.enabled = currentItem.amount > 1;
        amountTXT.text = currentItem.amount.ToString();
    }
}

