using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_inventory : MonoBehaviour {
    public Inventory inventory;
    public Inventory_UI inventory_UI;
    public int inventorySize;
    public ItemBaseSO testItem;
    public ItemBaseSO secondTestItem;
    private void Start() {
        inventory = new(inventory_UI, inventorySize);
        inventory_UI.SetInventory(inventory);
        inventory_UI.onEquipmentChange += RefreshStat;
        Test();
    }

    private void Test() {
        Item item = new(testItem, 3);
        inventory.AddItem(item);
        inventory.AddItem(item);
        inventory.AddItem(item);
        item = new(secondTestItem, 1);
        inventory.AddItem(item);
    }

    private void RefreshStat() {
        //TODO refresh inventory 
        throw new System.NotImplementedException();
    }
}
