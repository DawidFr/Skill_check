using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBaseSO", menuName = "Items/ItemBaseSO")]
public class ItemBaseSO : ScriptableObject
{
    public enum ItemType
    {
        baseItem,
        equipable,
        handable,
    }


    [Header("BaseSetting")]
    public string itemName;
    public string itemShortDescription;
    [TextArea] public string itemLongDescription;
    public Sprite itemSprite;
    public ItemType itemType;
    public string itemID;

    [Header("Equipable item setting")]
    public bool isEquipable;

    [Header("Special slot equipable item setting")]




    [Header("Handable item setting")]
    public bool isHandable;


    [Header("Stackable item setting")]
    public bool isStackable = true;
    public int stackSize = 1;
}
