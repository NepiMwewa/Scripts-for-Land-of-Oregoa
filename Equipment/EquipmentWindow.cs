using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentWindow : MonoBehaviour {

	[SerializeField] private EquipmentSelectedItem[] equipmentSlots;
	[SerializeField] private EquipmentSelectedItem consumableOne, consumableTwo;
	private BaseItem[] equipmentBaseItems;
	private InventoryWindow invWindow;
	private PlayerStats pStats;
	private PlayerHealthManager pHealth;
	private int tempDmg, tempPen, tempArmor;

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		invWindow = FindObjectOfType<InventoryWindow> ();
		pStats = FindObjectOfType<PlayerStats> ();
		pHealth = FindObjectOfType<PlayerHealthManager> ();
		UpdatePlayerEquipmentAnimations ();
	}
	
	public void UpdatePlayerEquipStats()// update the player equipment stats with the dmg pen armor etc
	{
		tempDmg = 0;
		tempPen = 0;
		tempArmor = 0;
		for (int i = 0; i < equipmentSlots.Length; i++) {
			if (equipmentSlots [i].EquipmentSlotItem != null) {
				if (equipmentSlots [i].EquipmentSlotItem.ItemWeaponDamage > 0) {
					tempDmg += equipmentSlots [i].EquipmentSlotItem.ItemWeaponDamage;
				}
				if (equipmentSlots [i].EquipmentSlotItem.ItemWeaponPen > 0) {
					tempPen += equipmentSlots [i].EquipmentSlotItem.ItemWeaponPen;
				}
				if (equipmentSlots [i].EquipmentSlotItem.ItemArmor > 0 || equipmentSlots [i].EquipmentSlotItem.ItemArmor < 0) {
					tempArmor += equipmentSlots [i].EquipmentSlotItem.ItemArmor;
				}
			}
		}

		pStats.currentPlayerStats.TotalDamage = pStats.currentPlayerStats.BaseDamage + tempDmg;
		pStats.currentPlayerStats.TotalPen = pStats.currentPlayerStats.BasePen + tempPen;
		pStats.currentPlayerStats.TotalArmor = pStats.currentPlayerStats.BaseArmor + tempArmor;
		pStats.UpdateGameInformation ();
		//Debug.Log ("dmg: " + GameInformation.PlayerStats.TotalDamage.ToString ());
	}

	public void UpdatePlayerEquipmentAnimations()
	{
		for (int i = 0; i < equipmentSlots.Length; i++) {
			equipmentSlots [i].UpdateEquipAnimation ();
		}
		UpdatePlayerEquipStats ();
	}

	public void UseConsumableOne()
	{
		if (consumableOne.EquipmentSlotItem != null) {
			UseItem (consumableOne);
		}
	}
	public void UseConsumableTwo ()
	{
		if (consumableTwo.EquipmentSlotItem != null) {
			UseItem (consumableTwo);
		}
	}

	private void UseItem(EquipmentSelectedItem tempItem)
	{
		if (tempItem.EquipmentSlotItem.EquipmentType != BaseItem.EquipmentSlotTypes.Consumable ) {
				return;
			}
		pHealth.playerCurrentHealth += tempItem.EquipmentSlotItem.ItemPotionHpAmount;
		GameInformation.PlayerStats.CurrentHealth += tempItem.EquipmentSlotItem.ItemPotionHpAmount;
		GameInformation.PlayerStats.CurrentStamina += tempItem.EquipmentSlotItem.ItemPotionMPAmount;
		if (tempItem.EquipmentSlotItem.ItemCurrentAmount == 1) {
			tempItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
			tempItem.transform.GetChild (0).gameObject.SetActive (false);

			tempItem.EquipmentSlotItem = null;
		
			tempItem.transform.name = "Empty";
			} else {
			tempItem.EquipmentSlotItem.ItemCurrentAmount -= 1;
			if (tempItem.EquipmentSlotItem.ItemCurrentAmount == 1) {
				tempItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
				} else {
				tempItem.transform.GetChild (0).GetComponentInChildren<Text> ().text = tempItem.EquipmentSlotItem.ItemCurrentAmount.ToString ();
				}
			}
			if (pHealth.playerCurrentHealth > pHealth.playerMaxHealth) {
				pHealth.playerCurrentHealth = pHealth.playerMaxHealth;
			}
			if (GameInformation.PlayerStats.CurrentHealth > GameInformation.PlayerStats.MaxHealth) {
				GameInformation.PlayerStats.CurrentHealth = GameInformation.PlayerStats.MaxHealth;
			}
			if (GameInformation.PlayerStats.CurrentStamina > GameInformation.PlayerStats.MaxStamina) {
				GameInformation.PlayerStats.CurrentStamina = GameInformation.PlayerStats.MaxStamina;
			}

	}

	public void CloseEquipmentWindow()
	{
		transform.GetChild (0).gameObject.SetActive (false);
		invWindow.CloseAltMenu ();
	}

	public bool EquipItem(BaseItem item, SelectedItem selectedItem){

		for (int i = 0; i < equipmentSlots.Length; i++) {
			if (equipmentSlots [i].equipmentSlotType == item.EquipmentType && equipmentSlots[i].EquipmentSlotItem == null) {
				
				AddItemToSlot (i, item, selectedItem);

				return false;
			}
		}
		for (int i = 0; i < equipmentSlots.Length; i++) {
			if (equipmentSlots [i].equipmentSlotType == item.EquipmentType) {

				AddItemToSlot (i, item, selectedItem);

				return false;
			}
		}
		return true;
	}

	private void AddItemToSlot(int i, BaseItem item,SelectedItem selectedItem)
	{
		if (equipmentSlots [i].EquipmentSlotItem != null) {
			selectedItem.ThisItem = equipmentSlots [i].EquipmentSlotItem;
			selectedItem.removeItem = false;
		}
		equipmentSlots [i].EquipmentSlotItem = item;
		//playerInventory.Add (itemSwap.ReturnBaseItem ());
		equipmentSlots [i].transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (item);
		equipmentSlots [i].transform.name = item.EquipmentType.ToString ();
		equipmentSlots [i].transform.GetChild (0).gameObject.SetActive (true);
		//basePlayerScript.Inventory = playerInventory;

		if (item.ItemCurrentAmount > 1) {
			equipmentSlots [i].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
			equipmentSlots [i].transform.GetChild (0).GetComponentInChildren<Text> ().text = item.ItemCurrentAmount.ToString ();
		} else {
			equipmentSlots [i].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
		}

		//call to update player animations with the new equipment and to recalculate the new stats for the player
		equipmentSlots [i].UpdateEquipAnimation ();
		UpdatePlayerEquipStats ();
		//Debug.Log ("added item");
	}

	public void RefreshEquipmentItems()
	{
		equipmentBaseItems = new BaseItem[equipmentSlots.Length];
		for (int i = 0; i < equipmentSlots.Length; i++) {
			equipmentBaseItems [i] = equipmentSlots [i].EquipmentSlotItem;
		}
	}

	public BaseItem[] GetEquipmentItems()
	{
		equipmentBaseItems = new BaseItem[equipmentSlots.Length];
		for (int i = 0; i < equipmentSlots.Length; i++) {

			if (equipmentSlots [i] != null) {
				equipmentBaseItems [i] = equipmentSlots [i].EquipmentSlotItem;
			}
		}
		return equipmentBaseItems;
	}

	public void SetEquipmentItems(BaseItem[] baseItems){
		equipmentBaseItems = new BaseItem[baseItems.Length];
		for (int i = 0; i < baseItems.Length; i++) {
			if (baseItems [i] != null ) {
				equipmentBaseItems [i] = baseItems [i];
				equipmentSlots [i].EquipmentSlotItem = baseItems [i];
				equipmentSlots [i].gameObject.name = 0.ToString();

				GameObject icon = equipmentSlots [i].transform.GetChild (0).gameObject;
				icon.SetActive (true);
				icon.GetComponent<Image> ().sprite = ReturnItemIcon (equipmentSlots [i].EquipmentSlotItem);

				if (equipmentBaseItems [i].ItemCurrentAmount > 1) {
					icon.transform.GetChild (0).gameObject.SetActive (true);
					icon.GetComponentInChildren<Text> ().text = equipmentBaseItems [i].ItemCurrentAmount.ToString ();
				} else {
					icon.transform.GetChild (0).gameObject.SetActive (false);
				}
			}
		}
	}

	private Sprite ReturnItemIcon(BaseItem item)
	{
		Sprite icon;

		icon = Resources.Load<Sprite> ("ItemIcons/" + item.ItemSlug);

		return icon;
	}

	public void GetEquipmentStats()
	{
		for (int i = 0; i < equipmentBaseItems.Length; i++) {
			if (equipmentBaseItems[i]!= null) {
				//Debug.Log (equipmentBaseItems[i].ItemName.ToString ());
				}
		}
		//Debug.Log ("called");
	}
}
