using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_DraggableItemFromInventory : MonoBehaviour {
    public static Mouse_DraggableItemFromInventory I;
    public DraggableSlot itemSlotToDragPF;
    private InventorySlot defaultSlot;

    public void SetUp(DraggableSlot slotToDrag, InventorySlot defSlot) {
        DraggableSlot dSLot = Instantiate(itemSlotToDragPF, this.transform);
        Inventory_UI.onInventoryClose += RestartSlot;
        defSlot = defaultSlot;
    }

    private void RestartSlot() {
        defaultSlot.AddItem(itemSlotToDragPF.currentItem);
        Destroy(this.gameObject);
    }
}
