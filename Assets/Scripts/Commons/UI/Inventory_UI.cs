using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory_UI : MonoBehaviour {
    public static Action onInventoryClose;
    public Action onEquipmentChange;
    private Inventory inventory;
    public List<InventorySlot> slots = new();
    public SpecialInventorySlot[] specialSlots;
    public GameObject specialSlotParent;
    public GameObject slotParent;
    public GameObject slotPF;
    private void Awake() {
        slotParent.GetComponentsInChildren<InventorySlot>().ToList().ForEach(x => slots.Add(x));
        specialSlots = specialSlotParent.GetComponentsInChildren<SpecialInventorySlot>();
    }
    public void SetInventory(Inventory invent) {
        inventory = invent;
    }

    public void AddInventorySlot(int amount) {
        inventory.inventorySize += amount;
        for (int i = 0; i < amount; i++) {
            InventorySlot slot = Instantiate(slotPF, slotParent.transform).GetComponent<InventorySlot>();
            slots.Add(slot);
        }
    }
    public void RemoveInventorySlot(int amountToRemove) {
        if (amountToRemove > inventory.inventorySize) amountToRemove = inventory.inventorySize - 1;

        int[] emptySlots = inventory.GetEmptySlotIndex();
        foreach (int i in emptySlots) {
            InventorySlot slotToRemove = slots[i];
            slots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);
            amountToRemove -= 1;
            if (amountToRemove == 0) return;
        }
        slots.Reverse();
        for (int i = 0; i < amountToRemove; i++) {
            InventorySlot slotToRemove = slots[i];
            inventory.RemoveItem(slotToRemove.item);
            TossItem(slotToRemove.item);
            //?    slotToRemove.RemoveItem();
            slots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);

        }
        slots.Reverse();
    }
    public void TossItem(Item item) {
        //TODO Toss Item 
        throw new System.NotImplementedException();
    }


}
