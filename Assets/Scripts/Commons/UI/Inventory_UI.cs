using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public Action onEquipmentChange;
    private Inventory inventory;
    public InventorySlot[] slots;
    public SpecialInventorySlot[] specialSlots;
    public GameObject specialSlotParent;
    public GameObject slotParent;
    private void Awake()
    {
        slots = slotParent.GetComponentsInChildren<InventorySlot>();
        specialSlots = specialSlotParent.GetComponentsInChildren<SpecialInventorySlot>();
    }
    public void SetInventory(Inventory invent)
    {
        inventory = invent;
        RefreshInventory();
    }


    private void RefreshInventory()
    {

        foreach (InventorySlot slot in slots)
        {


            slot.transform.GetComponent<Button_UI>().MouseLeftClickFunc = () =>
            {
                Debug.Log("click");
            };
            slot.gameObject.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Debug.Log("Right Click");
                /*Item item = slot.GetItem();

                if (item != null)
                {
                    item.amount = 1;
                    inventory.RemoveItem(item, slot);
                    //TODO ItemScript.DropItem(player.transform.position, item);
                }*/
            };
            slot.DesActive();


        }

        foreach (Item item in inventory.GetItemList())
        {
            foreach (InventorySlot slot in slots)
            {

                if (!slot.IsEmpty())
                {
                    if (slot.item.itemBase.itemID == item.itemBase.itemID)
                    {
                        slot.AddItemAmount(item.amount);
                        break;
                    }
                    else continue;
                }
                slot.SetActive(item);
                break;

            }

        }
    }

}
