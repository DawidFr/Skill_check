using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpecialInventorySlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;
    //private Item specialItemType;
    public Item item;
    private Image icon;
    private bool isEmpty = true;
    //private int itAmount;
    public TextMeshProUGUI AmountTxt;
    public GameObject ImagePF;
    public GameObject image;
    private void Awake()
    {
        DesActive();
    }

    public void SetActive(Item newItem)
    {
        item = newItem;
        image = Instantiate(ImagePF, transform);
        icon = image.GetComponent<Image>();
        AmountTxt = image.GetComponentInChildren<TextMeshProUGUI>();
        image.GetComponent<DragDropHandler>().SetSpecialSlot(this);

        //itAmount = item.amount;
        icon.sprite = item.itemBase.itemSprite;
        icon.enabled = true;
        Debug.Log("Item added");
        isEmpty = false;

        if (item.amount > 1)
        {
            AmountTxt.enabled = true;
            AmountTxt.text = item.amount.ToString();
        }
    }
    public void AddItemAmount(int amount)
    {
        int itemAmount = item.amount + amount;
        item.amount = itemAmount;
        AmountTxt.text = itemAmount.ToString();
        AmountTxt.enabled = true;
    }
    public bool TakeItemAmount(int amount)
    {
        Debug.Log(GetItemInSlotAmount());
        int itemAmount = item.amount - amount;
        if (itemAmount > 0)
        {
            item.amount = itemAmount;
            AmountTxt.text = itemAmount.ToString();
            AmountTxt.enabled = true;
        }
        else if (itemAmount == 0)
        {
            DesActive();
        }
        else
        {
            return false;

        }
        return true;

    }
    public int GetItemInSlotAmount()
    {
        if (!isEmpty) return item.amount;
        else return 0;

    }
    public void DesActive()
    {
        item = null;
        Destroy(image);
        isEmpty = true;


    }
    public bool IsEmpty()
    {
        return isEmpty;
    }
    public Item GetItem()
    {
        Debug.Log("Returning " + item.itemBase.specialSlotIndex);
        return item;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {

            DragDropHandler it = eventData.pointerDrag.GetComponent<DragDropHandler>();
            if (isEmpty)
            {

                /*if(it.GetDropItem().itemType == specialItemType.itemType ){
                    SetAct(it.GetDropItem());
                    Debug.Log("Drop");
                    it.SetSlotAsDesAct();
                }
                else*/
                Debug.Log("Item is not matching");

                // eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position; 

            }
            else
            {
                if (it.GetDropItem().itemBase.specialSlotIndex == item.itemBase.specialSlotIndex)
                {
                    AddItemAmount(it.GetDropItem().amount);
                    it.SetSlotAsDesAct();
                    Debug.Log("Added");
                }
                else Debug.Log("Item is not matching");
                Debug.Log("slot isn't empty");
            }
        }
    }
}
