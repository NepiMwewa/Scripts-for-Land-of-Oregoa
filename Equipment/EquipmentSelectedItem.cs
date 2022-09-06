using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSelectedItem : MonoBehaviour, IDragHandler, IPointerDownHandler{

	[SerializeField] private Text selectedItemText;
	private BaseItem equipmentSlotItem;
	public BaseItem.EquipmentSlotTypes equipmentSlotType;
	[SerializeField] private Text slotText;

	private Animator playerAnim;

	private EquipmentWindow equipWindow;

	//private BasePlayer basePlayerScript;
	private InventoryWindow inventoryWindow;
	private ItemSwap itemSwap;

	/*public enum EquipmentSlotTypes
	{
		Helmet,
		Chest,
		Gauntlet,
		Legs,
		Boots,
		Hand,
		AltHand,
		Misc,
		Ammo,
		Consumable,
		NonEquipment
	}*/

	// Use this for initialization

	void Awake(){
		slotText.text = equipmentSlotType.ToString ();
		playerAnim = FindObjectOfType<PlayerController> ().GetComponent<Animator>();
	}

	void Start () {

		//selectedItemText = GameObject.Find ("SelectedItemText").transform.GetChild (0).GetComponent<Text> ();
		//basePlayerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ();
		inventoryWindow = FindObjectOfType<InventoryWindow> ();
		itemSwap = FindObjectOfType<ItemSwap> ();
		//playerAnim = FindObjectOfType<PlayerController> ().GetComponent<Animator>();
		//UpdateEquipmentAnimation ();
		////Debug.Log ("//Debug TEST: " + equipmentSlotItem.EquipmentID.ToString() + " " + equipmentSlotItem.ItemName.ToString());

		equipWindow = FindObjectOfType<EquipmentWindow> ();
		//equipmentSlotItem = new BaseItem ();

	}

	// Update is called once per frame
	void Update () {

	}

	public BaseItem EquipmentSlotItem{
		get{ return equipmentSlotItem; }
		set{ equipmentSlotItem = value; }
	}

	public void UpdateEquipAnimation()
	{
		//Debug.Log ("function called");
		//set item type animation to corrosonding item id to game information then have player
		//controller to check and reapply the information to the animation
		switch (equipmentSlotType) {
		case BaseItem.EquipmentSlotTypes.Clothes:
			{
				if (equipmentSlotItem != null) {
					playerAnim.SetInteger ("ClothesTypeId", equipmentSlotItem.EquipmentID);
					//Debug.Log ("Armor set");
				} else {
					playerAnim.SetInteger ("ClothesTypeId", 0);
					//Debug.Log ("Armor removed");
				}
				break;
			}
		case BaseItem.EquipmentSlotTypes.Boots:
			{
				if (equipmentSlotItem != null) {
					playerAnim.SetInteger ("CapeTypeId", equipmentSlotItem.EquipmentID);
					//Debug.Log ("Cape set");
				} else {
					playerAnim.SetInteger ("CapeTypeId", 0);
					//Debug.Log ("Cape removed");
				}
				break;
			}
		case BaseItem.EquipmentSlotTypes.Consumable:
			{
				break;
			}
		case BaseItem.EquipmentSlotTypes.Weapon:
			{
				if (equipmentSlotItem != null) {
					playerAnim.SetInteger ("WeaponTypeId", equipmentSlotItem.EquipmentID);
					//Debug.Log ("Sword set");
				} else {
					playerAnim.SetInteger ("WeaponTypeId", 0);
					//Debug.Log ("Sword removed");
				}
				break;
			}
		case BaseItem.EquipmentSlotTypes.Helmet:
			{
				break;
			}
		case BaseItem.EquipmentSlotTypes.Shield:
			{
				break;
			}
			case BaseItem.EquipmentSlotTypes.Misc:
			{
				break;
			}
		default:
			//Debug.Log ("default equipment slot called");
			break;
		}
	}

	public void ShowSelectedItemText()
	{
		//if (this.gameObject.GetComponent<Toggle> ().isOn) {
			if (this.gameObject.name == "Empty") {
				selectedItemText.text = "This Slot is empty.";
			} else {

				selectedItemText.text = equipmentSlotItem.ItemName + " " + equipmentSlotItem.ItemDescription;
			}
		//}
	}

	public void OnDrag(PointerEventData eventData)
	{
		//if (!inventoryWindow.beingDragged && this.name != "Empty" && this.name != "InventoryWindow") {
		if(!itemSwap.beingDragged && Input.GetKey(KeyCode.Mouse0) && this.name != "Empty" && this.name != "EquipmentWindow" &&
			inventoryWindow.slotCnt > inventoryWindow.ReturnInventory().Count) {
			//inventoryWindow.ShowDraggedItem (this.gameObject.name);
			itemSwap.ShowDraggedItem(this.gameObject.name, equipmentSlotItem, ItemSwap.InventoryWindows.equipmentInventory);
			this.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
			this.transform.GetChild (0).gameObject.SetActive (false);

			equipmentSlotItem = null;

			this.transform.name = "Empty";

			UpdateEquipAnimation ();
			equipWindow.UpdatePlayerEquipStats ();

		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//playerInventory = basePlayerScript.ReturnPlayerInventory ();
		ShowSelectedItemText();
		if (this.name != "EquipmentWindow") {
			if (Input.GetKey (KeyCode.Mouse1) && this.name != "Empty" && !itemSwap.beingDragged) {
				inventoryWindow.OpenAltMenuByEq (this);
			} else {
				inventoryWindow.CloseAltMenu ();
			}
			if (Input.GetKey (KeyCode.LeftControl) && Input.GetKey(KeyCode.Mouse0) && this.name != "Empty") {
				if (inventoryWindow.slotCnt > inventoryWindow.ReturnInventory().Count) {
					inventoryWindow.AddItemByItem (equipmentSlotItem);
					this.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
					this.transform.GetChild (0).gameObject.SetActive (false);

					equipmentSlotItem = null;
					/*
					playerInventory.RemoveAt (int.Parse (this.name));
					basePlayerScript.Inventory = playerInventory;
					inventoryWindow.RefreshInventory (int.Parse (this.name));*/

					this.transform.name = "Empty";
				}
				UpdateEquipAnimation (); //call function to change the item id on the player animation options along with the save file.
				equipWindow.UpdatePlayerEquipStats ();
				return;
			}
			if (itemSwap.beingDragged  && Input.GetKey(KeyCode.Mouse0)) {
				if (itemSwap.ReturnBaseItem ().EquipmentType == equipmentSlotType) {
					
					if (this.name != "Empty") {
						equipmentSlotItem = itemSwap.SwapItem (this.gameObject, equipmentSlotItem);
						this.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (equipmentSlotItem);
						ShowSelectedItemText ();
					} else {

						equipmentSlotItem = itemSwap.ReturnBaseItem ();
						//playerInventory.Add (itemSwap.ReturnBaseItem ());
						this.transform.name = itemSwap.AddItemToSlot (this.gameObject, equipmentSlotType.ToString());
						this.transform.GetChild (0).gameObject.SetActive (true);
						//basePlayerScript.Inventory = playerInventory;
						//Debug.Log ("added item to " + equipmentSlotType.ToString() + " slot.");
						ShowSelectedItemText ();
					}

					if (equipmentSlotItem.ItemStackable) {
						this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
						this.transform.GetChild (0).GetComponentInChildren<Text> ().text = equipmentSlotItem.ItemCurrentAmount.ToString ();
					} else {
						this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
					}

					UpdateEquipAnimation ();
					equipWindow.UpdatePlayerEquipStats ();
					//call function to change the item id on the player animation options along with the save file.
				} else {
					//Debug.Log ("not the same equipmentType");
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
}

