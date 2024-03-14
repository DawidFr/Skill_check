using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Inventory inventory;
    public DraggableSlot itemSlotPF;
    public Item item;
    public DraggableSlot dSlot;

    public bool isEmpty;
    public bool isFull;

    public void Setup(Inventory inv) {
        inventory = inv;
        isEmpty = true;
        isFull = false;
    }
    public void AddItem(Item it) {
        dSlot = Instantiate(itemSlotPF, this.transform).GetComponent<DraggableSlot>();
        dSlot.Setup(item, this);
        isEmpty = false;
        isFull = (item.amount == item.maxStack);

    }
    public void RemoveItem() {
        //TODO remove Item;
    }

    public void RefreshItemAmount(int amount) {
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
