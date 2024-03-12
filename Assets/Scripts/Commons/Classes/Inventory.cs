using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public List<Item> itemList;
    public Inventory_UI inventoryUI;
    public Inventory(Inventory_UI inv){
        itemList ??= new();
        inventoryUI = inv;
    }
    public bool AddItem(Item item)
    {
        if (itemList != null)
        {
            if (itemList.Count == inventoryUI.slots.Length)
            {
                Debug.Log("Not enough space");
                return false;
            }
        }
        else Debug.Log("List == null");
        
        bool noSlotWithItem = false;
        
        InventorySlot slot;
        if (item.itemBase.isStackable)
        {

            try
            {
                slot = FindSlotWithItemInInventory(item);
            }
            catch
            {
                slot = FindEmptySlot();
                noSlotWithItem = true;
            }
            if (!noSlotWithItem)
            {
                slot.AddItemAmount(item.amount);
                return true;
            }
            else
            {
                itemList.Add(item);
                slot = FindEmptySlot();
                slot.SetActive(item);
                return true;
            }
        }
        else
        {
            itemList.Add(item);
            slot = FindEmptySlot();
            slot.SetActive(item);
            return true;
        }



    }
    public bool RemoveItem(Item item, InventorySlot InvSlot = null, int amount = -1)
    {
        if (!IsItemInInventory(item))
        {
            Debug.Log("Item don't find");
            return false;
        }
        bool isTaken = false;
        InventorySlot slot;
        if (amount == -1) amount = item.amount;
        if (InvSlot != null)
        {
            slot = InvSlot;
        }
        else
        {
            slot = FindSlotWithItemInInventory(item);
        }
        if (amount < item.amount)
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemBase.itemID == item.itemBase.itemID)
                {
                    isTaken = slot.TakeItemAmount(amount);
                    itemInInventory = inventoryItem;
                    continue;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0 && !isTaken)
            {
                Debug.Log("Item was removed");
                itemList.Remove(itemInInventory);
                return true;
            }
        }
        else
        {
            itemList.Remove(item);
            slot.DesActive();
            return true;
        }
        return false;
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
    public int GetItemAmount(Item item)
    {
        foreach (Item itemInInventory in itemList)
        {
            if (itemInInventory.itemBase.itemID == item.itemBase.itemID) return itemInInventory.amount;
        }
        return 0;
    }
    public InventorySlot FindSlotWithItemInInventory(Item item)
    {
        foreach (InventorySlot slot in inventoryUI.slots)
        {
            if (slot.GetItem().itemBase.itemID == item.itemBase.itemID)
            {
                return slot;
            }
        }
        return null;
    }
    public InventorySlot FindEmptySlot()
    {
        foreach (InventorySlot slot in inventoryUI.slots)
        {
            if (slot.IsEmpty())
            {
                return slot;
            }
        }
        return null;
    }
    public bool IsItemInInventory(Item item)
    {
        foreach (Item itemInList in itemList)
        {
            if (itemInList.itemBase.itemID == item.itemBase.itemID) return true;
        }
        return false;
    }
    public int GetItemInInventoryAmount(Item item)
    {
        foreach (InventorySlot slot in inventoryUI.slots)
        {
            if (slot.GetItem().itemBase.itemID == item.itemBase.itemID) return slot.GetItemInSlotAmount();
        }
        return 0;
    }
}

