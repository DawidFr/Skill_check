using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestController : MonoBehaviour {
    public ItemBaseSO item1, item2;
    public int item1amount, item2amount;
    private void Awake() {
        Button_UI button_UI = GetComponent<Button_UI>();
        button_UI.MouseLeftClickFunc += AddItemToInventory1;
        button_UI.MouseRightClickFunc += AddItemToInventory2;
    }
    public void AddItemToInventory1() {
        Item item = new(item1, item1amount);
        Player_inventory.I.inventory.AddItem(item);
    }
    public void AddItemToInventory2() {
        Item item = new(item2, item2amount);
        Player_inventory.I.inventory.AddItem(item);
    }
}
