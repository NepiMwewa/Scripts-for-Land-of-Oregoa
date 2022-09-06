using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

[System.Serializable]
public class BaseItem {

	//[XmlAttribute("attack")]
	public string ItemName{ get; set; }
	public int ItemID{ get; set; }
	public string ItemDescription{ get; set; }
	public string ItemSlug{ get; set; }
	public int ItemValue{ get; set; }
	public ItemTypes ItemType{ get; set; }
	public EquipmentSlotTypes EquipmentType{ get; set; }
	public int EquipmentID{ get; set; }

	//for weapons
	public int ItemWeaponDamage { get; set; }
	public int ItemWeaponPen{ get; set; }

	//for potions
	public int ItemPotionHpAmount{ get; set; }
	public int ItemPotionMPAmount{ get; set; }

	//for armor
	public int ItemArmor{ get; set; }

	public int ItemCurrentDura{ get; set; }
	public int ItemMaxDura{ get; set; }

	public bool ItemStackable{ get; set; }
	public int ItemCurrentAmount{ get; set; }
	public int ItemMaxAmount{ get; set; }

	public enum EquipmentSlotTypes
	{
		Helmet,
		Clothes,
		Boots,
		Weapon,
		Shield,
		Misc,
		Consumable,
		NonEquipment

	}

	public enum ItemTypes
	{
		SHIELD,
		SWORD,
		AXE,
		BOW,
		ARMOR,
		DAGGER,
		ITEM,
		DROP,
		QUEST
	}
}