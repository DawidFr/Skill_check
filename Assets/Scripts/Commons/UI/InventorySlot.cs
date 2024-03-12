using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Item item;
    private Image icon;
    private bool isEmpty;
    //private int itAmount;
    public TextMeshProUGUI AmountTxt;
    public GameObject ImagePF;
    public GameObject image;

    public void SetActive(Item newItem)
    {
        item = newItem;
        image = Instantiate(ImagePF, transform);
        icon = image.GetComponent<Image>();
        AmountTxt = image.GetComponentInChildren<TextMeshProUGUI>();
        image.GetComponent<DragDropHandler>().SetSlot(this);

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
        return item.amount;

    }
    public void DesActive()
    {
        item = null;
        Destroy(image);
        //icon.sprite = null;
        //Debug.Log(transform.name + " was desalted");
        //icon.enabled = false;
        isEmpty = true;
        //AmountTxt.enabled = false;


    }
    public bool IsEmpty()
    {
        return isEmpty;
    }
    public Item GetItem()
    {
        return item;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {


            if (isEmpty)
            {
                DragDropHandler it = eventData.pointerDrag.GetComponent<DragDropHandler>();
                SetActive(it.GetDropItem());
                Debug.Log("Drop");
                it.SetSlotAsDesAct();
                // eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position; 

            }
        }
    }
    
}
