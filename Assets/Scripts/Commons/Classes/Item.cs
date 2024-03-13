using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
	public ItemBaseSO itemBase;
	public Sprite itemSprite;
	public int amount;
	public int maxStack;
	public string ID;
	public Item(ItemBaseSO itemB, int a = 1)
	{
		itemSprite = itemB.itemSprite;
		ID = itemB.itemID;
		itemBase = itemB;
		maxStack = itemBase.stackSize;
		amount = a;
		if (amount > maxStack) amount = maxStack;
	}

	public void AddItemAmount(int a, out int overStackAmount)
	{
		int controlAmount = 0;
		controlAmount = amount + a;
		if (controlAmount == maxStack)
		{
			amount = maxStack;
			overStackAmount = 0;
		}
		else if (controlAmount < maxStack)
		{
			amount += a;
			overStackAmount = 0;
		}
		else
		{
			overStackAmount = maxStack - (amount + a);
			amount = maxStack;
		}
	}
}
