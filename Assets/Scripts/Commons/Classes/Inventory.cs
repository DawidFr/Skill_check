using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    public int inventorySize;
    public List<Item> itemList;
    public Inventory_UI inventoryUI;
    public Inventory(Inventory_UI inv, int invSize) //Inventory setup
    {
        itemList ??= new();     //this is if(itemList == null){ itemList = new List<Item>()} but written like a pro;
        inventoryUI = inv;
        inventorySize = invSize;
    }
    /*
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
        foreach (Item it in itemList)
        {
            if (it.itemBase.itemID == item.itemBase.itemID) return it.amount;
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
    */

    //!! 
    //TODO You ended her, there is a lot to do
    //!!
    public bool AddItem(Item item, bool stackWithOther = true)
    {
        if (itemList.Count == inventorySize) return false;
        if (!item.itemBase.isStackable && !stackWithOther)
        {
            FindEmptySlot().AddItem(item);
            itemList.Add(item);
            return true;
        }
        int amountToAdd = item.amount;
        int amountLeft;
        foreach (Item it in itemList)
        {
            if (it == item)
            {
                if (it.amount != it.maxStack)
                {

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

    private InventorySlot FindEmptySlot()
    {
        foreach (InventorySlot invSlot in inventoryUI.slots)
        {
            if (invSlot.isEmpty) return invSlot;
        }
        return null;
    }

    public int CheckAmountToAdd(Item item, int a, out int overStackAmount)
    {
        if (!item.itemBase.isStackable)
        {
            overStackAmount = a;
            return 1;
        }

        int amount = item.amount;
        int maxStack = item.maxStack;
        int control = amount + a;

        if (control > maxStack)
        {
            overStackAmount = control - maxStack;
            amount = maxStack;
        }
        else
        {
            amount += a;
            overStackAmount = 0;
        }
        return amount;

    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void RemoveItemAmount(int a, out int additionalAmount)
    {
        //TODO Remove item Amount
        throw new NotImplementedException();
        /*int control = amount - a;
        if (control < 0)
        {
            RemoveItem();
            additionalAmount = -(amount - a);
        }
        else if (control == 0)
        {
            additionalAmount = 0;
            RemoveItem();
        }
        else
        {
            additionalAmount = 0;
            amount -= a;
            dSlot.UpdateAmount();
        }*/
    }

    private InventorySlot FindSlotWIthItem(Item it)
    {
        //TODO Find slot with Item
        throw new NotImplementedException();
    }

    public Item FindItem(Item item)
    {
        foreach (Item it in itemList)
        {
            if (it == item) return it;
        }
        return null;
    }
    public void ChangeItemAmount(Item item, int a)
    {
        //TODO change item amount
    }
}

