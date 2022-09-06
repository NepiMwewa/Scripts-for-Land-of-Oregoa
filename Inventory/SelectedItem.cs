using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedItem : MonoBehaviour, IDragHandler, IPointerDownHandler{

	private Text selectedItemText;

	private ChestWindow chestWindow;
	private EquipmentWindow eqWindow;
	private InventoryWindow invWindow;

	//private BasePlayer basePlayerScript;
//	private InventoryWindow inventoryWindow;
	private ItemSwap itemSwap;
	private BaseItem thisItem;

	public bool removeItem;

	// Use this for initialization

	void Awake(){
		/*selectedItemText = GameObject.Find ("SelectedItemText").transform.GetChild (0).GetComponent<Text> ();
		basePlayerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ();
		inventoryWindow = GameObject.Find ("InventoryWindow").GetComponent<InventoryWindow> ();
		itemSwap = GameObject.Find ("GUIPanel").GetComponent<ItemSwap> ();

		chestWindow = GameObject.Find ("ChestInventoryWindow").GetComponent<ChestWindow> ();*/

	}

	void Start () {

		selectedItemText = GameObject.Find ("SelectedItemText").transform.GetChild (0).GetComponent<Text> ();
		//basePlayerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ();
//		inventoryWindow = GameObject.Find ("InventoryWindow").GetComponent<InventoryWindow> ();
		itemSwap = FindObjectOfType<ItemSwap> ();
		eqWindow = FindObjectOfType<EquipmentWindow> ();
		invWindow = FindObjectOfType<InventoryWindow> ();

		chestWindow = FindObjectOfType<ChestWindow> ();
	}

	public BaseItem ThisItem{
		get{ return thisItem; }
		set{ thisItem = value; }
	}


	public void ShowSelectedItemText()
	{
		//if (this.gameObject.GetComponent<Toggle> ().isOn) {
			if (this.gameObject.name == "Empty") {
				selectedItemText.text = "This Slot is empty.";
			} else {

			selectedItemText.text = ThisItem.ItemName + " " + ThisItem.ItemDescription;
			}
		//}
	}

	public void OnDrag(PointerEventData eventData)
	{
		//if (!inventoryWindow.beingDragged && this.name != "Empty" && this.name != "InventoryWindow") {
		//ShowSelectedItemText();
		if(!itemSwap.beingDragged && Input.GetKey(KeyCode.Mouse0) && this.name != "Empty" && this.name != "InventoryWindow" && !GameInformation.shopOpen) {
			//inventoryWindow.ShowDraggedItem (this.gameObject.name);
			//Debug.Log ("this: " + thisItem.ItemName);

			itemSwap.ShowDraggedItem(this.gameObject.name, thisItem, ItemSwap.InventoryWindows.playerInventory);
			this.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
			this.transform.GetChild (0).gameObject.SetActive (false);

			thisItem = null;

			this.transform.name = "Empty";
			//GameInformation.PlayerInventory.RemoveAt (int.Parse (this.name));
			//playerInventory = GameInformation.PlayerInventory;
			//inventoryWindow.UpdateInventory (int.Parse (this.name));

//			this.transform.GetChild (0).GetComponent<Image> ().sprite = null;
//			this.gameObject.name = "Empty";
//			this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
//			this.transform.GetChild (0).gameObject.SetActive (false);

		}
	}

	public void OnPointerDown(PointerEventData eventData)// showseletectitem text
	{
		ShowSelectedItemText ();
		if (this.name != "InventoryWindow") {

			if (GameInformation.shopOpen && Input.GetKey(KeyCode.Mouse0) && this.name != "Empty" && !itemSwap.beingDragged) {
				invWindow.CloseAltMenu ();
				if (Input.GetKey (KeyCode.LeftControl)) {
					invWindow.PlayerWantsToSell (this);
					return;
				}
				invWindow.SellItem (this );
			}

			if(Input.GetKey(KeyCode.Mouse1) && this.name != "Empty" && !itemSwap.beingDragged /*&& ThisItem.EquipmentType != BaseItem.EquipmentSlotTypes.questitem*/)
			{
				if (GameInformation.shopOpen) {
					return;
				}
				//Debug.Log ("right click");
				invWindow.OpenAltMenuByInv (this);
			}else{
				invWindow.CloseAltMenu ();
			}

			if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey(KeyCode.Mouse0) && this.name != "Empty") {
				if (thisItem.ItemCurrentAmount > 1 && !itemSwap.beingDragged) {

					int tempRemainder = 0;
					int tempHalf = 0;
					bool enoughRoom = false;

					tempHalf = thisItem.ItemCurrentAmount / 2;
					tempRemainder = thisItem.ItemCurrentAmount % 2;

					enoughRoom = invWindow.AddItemWithAmount (ThisItem.ItemID, tempHalf);

					if (enoughRoom) {
						thisItem.ItemCurrentAmount = tempRemainder + tempHalf;
						if (ThisItem.ItemCurrentAmount > 1) {
							this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
							this.transform.GetChild (0).GetComponentInChildren<Text> ().text = ThisItem.ItemCurrentAmount.ToString ();
						} else {
							this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
						}
					}

					return;
				}
			}
			if (Input.GetKey (KeyCode.LeftControl) && Input.GetKey(KeyCode.Mouse0) && this.name != "Empty" && !itemSwap.beingDragged) {
				if (chestWindow.transform.GetChild(0).gameObject.activeSelf && chestWindow.slotCnt > chestWindow.ReturnChestInventory().Count) {
					chestWindow.AddItemWithAmount (thisItem.ItemID, thisItem.ItemCurrentAmount);
					this.transform.GetChild (0).gameObject.SetActive (false);

					thisItem = null;

					this.transform.name = "Empty";
				}

				if (eqWindow.transform.GetChild (0).gameObject.activeSelf) {
					bool temp;

					temp = eqWindow.EquipItem(thisItem, this);

					if (temp) {
						Debug.Log ("could not equip item");
						return;
					} 

					if (removeItem) {
						this.transform.GetChild (0).gameObject.SetActive (false);

						thisItem = null;

						this.transform.name = "Empty";
					} else {
						this.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (thisItem);
						this.transform.name = thisItem.ItemName.ToString();
						this.transform.GetChild (0).gameObject.SetActive (true);
						if (ThisItem.ItemCurrentAmount > 1) {
							this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
							this.transform.GetChild (0).GetComponentInChildren<Text> ().text = ThisItem.ItemCurrentAmount.ToString ();
						} else {
							this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
						}
					}
					removeItem = true;
				}

				return;
			}
			if (itemSwap.beingDragged && Input.GetKey(KeyCode.Mouse0)) {
				if (this.name != "Empty") {
					ThisItem = itemSwap.SwapItem (this.gameObject, ThisItem);
					this.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (thisItem);
					this.name = thisItem.ItemName;
					ShowSelectedItemText ();
				} else {
					ThisItem = itemSwap.ReturnBaseItem ();
					//playerInventory.Add (itemSwap.ReturnBaseItem ());
					this.transform.name = itemSwap.AddItemToSlot(this.gameObject, ThisItem.ItemName.ToString());
					this.transform.GetChild (0).gameObject.SetActive (true);
					//basePlayerScript.Inventory = playerInventory;
					//Debug.Log ("added item");
					ShowSelectedItemText ();
				}
				if (ThisItem.ItemCurrentAmount > 1) {
					this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
					this.transform.GetChild (0).GetComponentInChildren<Text> ().text = ThisItem.ItemCurrentAmount.ToString ();
				} else {
					this.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
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