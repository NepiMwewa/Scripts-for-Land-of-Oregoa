using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickDragItems : MonoBehaviour
{
	public string ItemName;
	public int ItemID;
	public string ItemDescription;
	public string ItemSlug;
	public int ItemValue;
	public BaseItem.ItemTypes ItemType;
	public BaseItem.EquipmentSlotTypes EquipmentType;
	public int EquipmentID;

	//for weapons
	public int ItemWeaponDamage;
	public int ItemWeaponPen;

	//for potions
	public int ItemPotionHpAmount;
	public int ItemPotionMPAmount;

	//for armor
	public int ItemArmor;

	public int ItemCurrentDura;
	public int ItemMaxDura;

	public bool ItemStackable;
	public int ItemCurrentAmount;
	public int ItemMaxAmount;
}
