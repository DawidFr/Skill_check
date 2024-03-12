using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemBaseSO", menuName = "Items/ItemBaseSO")]
public class ItemBaseSO : ScriptableObject
{
    public enum ItemType{
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

    /// <summary>
    /// 0 - headSlot
    /// 1 - chest slot
    /// 2 - leg slot
    /// 3 - bag slot
    /// 4 - belt slot  
    /// 5 - rune slot
    /// 6 - special book slot
    /// 7 - food slot 
    /// 8 - health potion slot
    /// 9 - mana potion slot
    /// </summary>
    public int specialSlotIndex; 


    [Header("Handable item setting")]
    public bool isHandable;
    
    
    [Header("Stackable item setting")]
    public bool isStackable = true;
    public int stackSize = 1;


}
