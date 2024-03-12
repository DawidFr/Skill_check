using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_inventory : MonoBehaviour
{
    public Inventory inventory;
    public Inventory_UI inventory_UI;

    public ItemBaseSO testItem;
    private void Start(){
        inventory = new(inventory_UI);
        inventory_UI.SetInventory(inventory);
        inventory_UI.onEquipmentChange += RefreshStat;
        Item item = new(testItem, 3);
        inventory.AddItem(item);
    }

    private void RefreshStat()
    {
        //TODO refresh inventory
    }
}
