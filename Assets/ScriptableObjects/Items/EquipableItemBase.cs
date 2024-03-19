using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "EquipableItemBase", menuName = "Items/EquipableItemBase")]
public class EquipableItemBase : ItemBaseSO {


    [Header("Stats buff/debuff")]   // Add or subtracts;
    public int armourBonus;
    public int speedBonus;
    public int jumpHeightBonus;

    [Header("Stats multiplier")]    //Multiply or divide 
    public float armourMultiplier;
    public float speedMultiplier;
    public float jumpHeightMultiplier;

    [Header("Slot config")]

    /// <variable>
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
    /// </variable>
    [Tooltip("0 - headSlot \n 1 - chest slot \n 2 - leg slot \n 3 - bag slot \n 4 - belt slot \n 5 - rune slot \n 6 - special book slot \n 7 - food slot \n  8 - health potion slot \n 9 - mana potion slot")]
    public int specialSlotIndex;

    [Header("Rarity configuration")]
    public Item.ItemRarity maxRarity;
    public Item.ItemRarity minRarity;

    public float[] multiplierPerRarityMultiplier;



}
