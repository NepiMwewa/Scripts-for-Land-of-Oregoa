using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class RPGItemDatabase : MonoBehaviour {

	public TextAsset itemInventory;
	private List<BaseItem> inventoryItems;// use this to access the database for the inv

	private List<Dictionary<string,string>> inventoryItemsDictionary;
	private Dictionary<string,string> inventoryDictionary;

	void Awake(){
		inventoryItems = new List<BaseItem> ();

		inventoryItemsDictionary = new List<Dictionary<string,string>>();

		ReadItemsFromDatabase ();
		for (int i = 0; i < inventoryItemsDictionary.Count; i++) {
// 			inventoryItems.Add (new BaseItem (inventoryItemsDictionary [i]));
			//Debug.Log (inventoryItems [i].ItemName.ToString ());
		}

	}

	public List<BaseItem> InventoryItems()
	{
		return inventoryItems;
	}

	public void ReadItemsFromDatabase(){
		XmlDocument xmlDocument = new XmlDocument ();
		xmlDocument.LoadXml (itemInventory.text);
		XmlNodeList itemList = xmlDocument.GetElementsByTagName ("Items");//this tag is what accesses each item in the database

		foreach(XmlNode itemInfo in itemList) {
			XmlNodeList itemContent = itemInfo.ChildNodes;
			inventoryDictionary = new Dictionary<string, string> ();

			foreach (XmlNode content in itemContent) {
				switch (content.Name) {
				case "ItemID":
					{
						inventoryDictionary.Add ("ItemID", content.InnerText);
						break;
					}
				case "ItemName":
					{
						inventoryDictionary.Add ("ItemName", content.InnerText);
						break;
					}
				case "ItemValue":
					{
						inventoryDictionary.Add("ItemValue", content.InnerText);
						break;
					}
				case "ItemDescription":
					{
						inventoryDictionary.Add ("ItemDescription", content.InnerText);
						break;
					}
				case "ItemType":
					{
						inventoryDictionary.Add ("ItemType", content.InnerText);
						break;
					}
				case "ItemSlug":
					{
						inventoryDictionary.Add ("ItemSlug", content.InnerText);
						break;
					}
				case "EquipmentType":
					{
						inventoryDictionary.Add ("EquipmentType", content.InnerText);
						break;
					}
				case "ItemWeaponDamage":
					{
						inventoryDictionary.Add ("ItemWeaponDamage", content.InnerText);
						break;
					}
				case "ItemWeaponPen":
					{
						inventoryDictionary.Add ("ItemWeaponPen", content.InnerText);
						break;
					}
				case "ItemPotionHpAmount":
					{
						inventoryDictionary.Add ("ItemPotionHpAmount", content.InnerText);
						break;
					}
				case "ItemPotionMPAmount":
					{
						inventoryDictionary.Add ("ItemPotionMPAmount", content.InnerText);
						break;
					}
				case "ItemArmor":
					{
						inventoryDictionary.Add ("ItemArmor", content.InnerText);
						break;
					}
				case "ItemCurrentDura":
					{
						inventoryDictionary.Add ("ItemCurrentDura", content.InnerText);
						break;
					}
				case "ItemMaxDura":
					{
						inventoryDictionary.Add ("ItemMaxDura", content.InnerText);
						break;
					}
				case "ItemStackable":
					{
						inventoryDictionary.Add ("ItemStackable", content.InnerText);
						break;
					}
				case "ItemCurrentAmount":
					{
						inventoryDictionary.Add ("ItemCurrentAmount", content.InnerText);
						break;
					}
				case "ItemMaxAmount":
					{
						inventoryDictionary.Add ("ItemMaxAmount", content.InnerText);
						break;
					}
				}
			}

			inventoryItemsDictionary.Add (inventoryDictionary);
		}
	}
}
