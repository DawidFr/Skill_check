using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	public enum ItemType {
		normal,
		Equipable,
		Consumable,

	}
	public enum ItemRarity {
		none,
		Trash,
		Normal,
		Uncommon,
		Epic,
		Legendary,
		Mythical,
		Godly,
	}
	public ItemType itemType;
	public ConsumableItemBase consumableItemBase;
	public EquipableItemBase equipableItemBase;
	public ItemBaseSO itemBase;
	public Sprite itemSprite;
	public int amount;
	public int maxStack;
	public string ID;
	public Item(ItemBaseSO itemB, int a = 1) {
		itemSprite = itemB.itemSprite;
		ID = itemB.itemID;
		itemBase = itemB;
		maxStack = itemBase.stackSize;
		amount = a;
		itemType = ItemType.normal;
		if (amount > maxStack) amount = maxStack;
	}
	public Item(EquipableItemBase itemB, int a = 1) {
		itemSprite = itemB.itemSprite;
		ID = itemB.itemID;
		itemBase = itemB;
		equipableItemBase = itemB;
		maxStack = itemBase.stackSize;
		amount = a;
		itemType = ItemType.Equipable;
		if (amount > maxStack) amount = maxStack;
	}
	public Item(ConsumableItemBase itemB, int a = 1) {
		itemSprite = itemB.itemSprite;
		ID = itemB.itemID;
		itemBase = itemB;
		consumableItemBase = itemB;
		maxStack = itemBase.stackSize;
		amount = a;
		itemType = ItemType.Consumable;
		if (amount > maxStack) amount = maxStack;
	}

	public static Item CreateNewItemFromItem(Item item) {
		switch (item.itemType) {
			case ItemType.normal: {
					Item newItem = new(item.itemBase, item.amount);
					return newItem;
				}
			case ItemType.Equipable: {
					Item newItem = new(item.equipableItemBase, item.amount);
					return newItem;
				}
			case ItemType.Consumable: {
					Item newItem = new(item.consumableItemBase, item.amount);
					return newItem;
				}
			default: return null;
		}
	}
	/// <summary>
	/// Do not abuse this function!!
	/// </summary>
	public void ChangeAmount(int newAmount) {
		amount = newAmount;
	}
	public string GetEquipableItemStatsInString() {
		string statsString = "";
		if (equipableItemBase.armourBonus != 0) statsString += "Armor: " + StatAmountStringConstructor(equipableItemBase.armourBonus);
		if (equipableItemBase.speedBonus != 0) statsString += "Speed: " + StatAmountStringConstructor(equipableItemBase.speedBonus);
		if (equipableItemBase.jumpHeightBonus != 0) statsString += "Jump Height: " + StatAmountStringConstructor(equipableItemBase.jumpHeightBonus);
		if (equipableItemBase.armourMultiplier != 0) statsString += "Armor multiplier: " + equipableItemBase.armourMultiplier.ToString() + "\n";
		if (equipableItemBase.speedMultiplier != 0) statsString += "Speed multiplier: " + equipableItemBase.speedMultiplier.ToString() + "\n";
		if (equipableItemBase.jumpHeightMultiplier != 0) statsString += "Jump height multiplier: " + equipableItemBase.jumpHeightMultiplier.ToString() + "\n";
		return statsString;
	}
	private string StatAmountStringConstructor(int aToCheck) {
		string returnS = "";
		returnS += aToCheck > 0 ? "+ " : "- ";
		returnS += aToCheck.ToString();
		returnS += "\n";
		return returnS;
	}
	// public void AddItemAmount(int a, out int overStackAmount) {
	// 	int controlAmount = 0;
	// 	controlAmount = amount + a;
	// 	if (controlAmount == maxStack) {
	// 		amount = maxStack;
	// 		overStackAmount = 0;
	// 	}
	// 	else if (controlAmount < maxStack) {
	// 		amount += a;
	// 		overStackAmount = 0;
	// 	}
	// 	else {
	// 		overStackAmount = maxStack - (amount + a);
	// 		amount = maxStack;
	// 	}
	// }
}
