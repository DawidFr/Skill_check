using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Inventory inventory;
    public DraggableSlot itemSlotPF;
    public string itemInSlotID;
    public DraggableSlot dSlot;
    public bool isItemStackable;
    public int amount;
    public int maxStack;

    public bool isEmpty;

    public void Setup(Inventory inv)
    {
        inventory = inv;
        isEmpty = true;
    }
    public void AddItem(Item item)
    {
        dSlot = Instantiate(itemSlotPF, this.transform).GetComponent<DraggableSlot>();
        dSlot.Setup(item, this);
        itemInSlotID = item.ID;
        isItemStackable = item.itemBase.isStackable;
        maxStack = item.maxStack;

    }
    public void AddItemAmount(int a, out int overStackAmount)
    {
        if (!isItemStackable)
        {
            overStackAmount = a;
            return;
        }

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
        inventory.GetItemList().Find(dSlot.currentItem).amount = amount;

        dSlot.UpdateAmount();

    }
    public void RemoveItemAmount(int a, out int additionalAmount)
    {
        int control = amount - a;
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
        }
    }

    private void RemoveItem()
    {
        //TODO remove Item;
    }

    public void RefreshItemAmount(int amount)
    {
        //TODO refresh inventory amount
        throw new NotImplementedException();
    }
}
//              For SEBASTIAN


// public static class ConsumableItemFunctions{

// 	public enum ItemType{
// 		healthPotion,
// 		manaPotion,
// 		staminaPotion
// 	}
//     public static void UseItem(ItemType type)
//     {
//         switch (type) { 

//             case ItemType.healthPotion : {
//                 UseHealthPotion();
//                 break;
//             }
//             case ItemType.manaPotion : {
//                 UseManaPotion();
//                 break;
//             }
//             case ItemType.staminaPotion : {
//                 UseStaminaPotion();
//                 break;
//             }
//             default : break;

//         }

//     }
// 	public static void UseHealthPotion(){
// 		//code
// 	}
// 	public static void UseManaPotion(){
// 		//code
// 	}
// 	public static void UseStaminaPotion(){
// 		//code
// 	}	
// }
// class Example
// {
//     int x;
//     int twiceX;
//     public int Sqrt(int v)
//     {
//         return v * v;
//     }
//     public void Sqrtt(int v, out int value)
//     {
//         value = v * v;
//     }
//     public void Sqrtt(int v, out int value, out int twiceValue)
//     {
//         value = v * v;
//         twiceValue = value * 2;
//     }

//     void Test()
//     {
//         x = Sqrt(x);
//         Sqrtt(x, out x);
//         Sqrtt(x, out x, out twiceX);
//     }
// }
