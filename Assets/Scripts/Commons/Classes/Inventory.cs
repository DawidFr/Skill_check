using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public int inventorySize;
    public List<Item> itemList;
    public Inventory_UI inventoryUI;
    public Inventory(Inventory_UI inv, int invSize) //Inventory setup
    {
        itemList ??= new();     //this is if(itemList == null){ itemList = new List<Item>()} but written like a pro;
        inventoryUI = inv;
        inventorySize = invSize;
    }


    public bool AddItem(Item item, bool stackWithOther = true) {
        if (itemList.Count == inventorySize) return false;
        if (!item.itemBase.isStackable && !stackWithOther) {
            FindEmptySlot().AddItem(item);
            itemList.Add(item);
            return true;
        }
        int amountToAdd = item.amount;
        int amountLeft;
        foreach (Item it in itemList) {
            if (it == item) {
                if (it.amount != it.maxStack) {

                    it.amount = CheckAmountToAdd(it, amountToAdd, out int aLeft);
                    InventorySlot slot = FindSlotWIthItem(it);
                    slot.RefreshItemAmount(it.amount);
                    amountLeft = aLeft;
                    if (amountLeft == 0) break;
                }
            }
        }
        return true;
    }
    public void RemoveItem(Item item) {
        // TODO remove item from inventory
        throw new System.NotImplementedException();
    }




    public bool IsEnoughInInventory(Item item, int amountToCheck) {
        return GetItemInInventoryAmount(item) <= amountToCheck;
    }
    public int GetItemInInventoryAmount(Item item) {
        int amount = 0;
        foreach (InventorySlot slot in FindSlotsWIthItem(item)) {
            amount += slot.item.amount;
        }
        return amount;
    }

    private List<InventorySlot> FindSlotsWIthItem(Item item) {
        List<InventorySlot> slots = new();
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (slot.item == item) {
                slots.Add(slot);
            }
        }
        return slots;
    }

    public bool AddItemAmount(Item item, out int overInventoryAmount) {
        //TODO Add Item Amount
        throw new System.NotImplementedException();
    }
    public int CheckAmountToAdd(Item item, int a, out int overStackAmount) {
        if (!item.itemBase.isStackable) {
            overStackAmount = a;
            return 1;
        }

        int amount = item.amount;
        int maxStack = item.maxStack;
        int control = amount + a;

        if (control > maxStack) {
            overStackAmount = control - maxStack;
            amount = maxStack;
        }
        else {
            amount += a;
            overStackAmount = 0;
        }
        return amount;

    }
    public void ChangeItemAmount(Item item, int a) {
        //TODO change item amount
    }
    public void RemoveItemAmount(int a, out int additionalAmount) {
        //TODO Remove item Amount
        throw new NotImplementedException();
    }

    public int TryAddItemAmountToSlots() {
        
    }


    public int GetSpaceInInventory() {
        return inventorySize - itemList.Count;
    }
    private List<InventorySlot> FindSlotsWithItemOfType(ItemBaseSO itemBase){
        List<InventorySlot> slots = new();
        foreach(InventorySlot slot in inventoryUI.slots){
            if (slot.item.itemBase == itemBase) slots.Add(slot);
        }
        return slots;
    }
    private InventorySlot FindSlotWithItem(Item it) {
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (slot.item == it) return slot;
        }
        return null;
    }
    private InventorySlot FindEmptySlot() {
        foreach (InventorySlot invSlot in inventoryUI.slots) {
            if (invSlot.isEmpty) return invSlot;
        }
        return null;
    }
    public List<InventorySlot> GetEmptySlots() {
        List<InventorySlot> slots = new();
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (slot.isEmpty) slots.Add(slot);
        }
        return slots;
    }


    public bool HaveEmptySLot() {
        throw new NotImplementedException();
    }
    public Item FindItem(Item item) {
        foreach (Item it in itemList) {
            if (it == item) return it;
        }
        return null;
    }
    public List<Item> GetItemList() {
        return itemList;
    }
}

