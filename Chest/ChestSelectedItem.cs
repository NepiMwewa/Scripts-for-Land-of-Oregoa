using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestSelectedItem : MonoBehaviour, IDragHandler, IPointerDownHandler{

	private Text selectedItemText;

	private ChestWindow chestWindow;
	private InventoryWindow inventoryWindow;

	private ItemSwap itemSwap;
	private int slotName;
	public BaseItem ThisItem{ get; set; }

	// Use this for initialization

	void Awake(){ 
		selectedItemText = GameObject.Find ("SelectedItemText").transform.GetChild (0).GetComponent<Text> ();
		chestWindow = FindObjectOfType<ChestWindow> ();
		itemSwap = FindObjectOfType<ItemSwap> ();

		inventoryWindow = FindObjectOfType<InventoryWindow> ();


	}

	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowSelectedItemText()
	{
		if (this.gameObject.name == "Empty") {
			selectedItemText.text = "This Slot is empty.";
		} else {

			selectedItemText.text = ThisItem.ItemName + " " + ThisItem.ItemDescription;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if(!itemSwap.beingDragged && this.name != "Empty" && this.name != "ChestWindow") {
			//inventoryWindow.ShowDraggedItem (this.gameObject.name);
			////Debug.Log ("this: " + ThisItem.ItemName);

			itemSwap.ShowDraggedItem(this.gameObject.name, ThisItem, ItemSwap.InventoryWindows.chestInventory);
			this.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
			this.transform.GetChild (0).gameObject.SetActive (false);

			ThisItem = null;

			this.transform.name = "Empty";

		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		ShowSelectedItemText ();
		if (this.name != "InventoryWindow") {


			if (Input.GetKey (KeyCode.LeftShift) && this.name != "Empty") {
				if (ThisItem.ItemCurrentAmount > 1 && itemSwap.ReturnBaseItem() == null) {

					int tempRemainder = 0;
					int tempHalf = 0;
					bool enoughRoom = false;

					tempHalf = ThisItem.ItemCurrentAmount / 2;
					tempRemainder = ThisItem.ItemCurrentAmount % 2;

					enoughRoom = chestWindow.AddItemWithAmount (ThisItem.ItemID, tempHalf);

					if (enoughRoom) {
						ThisItem.ItemCurrentAmount = tempRemainder + tempHalf;
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
			if (Input.GetKey (KeyCode.LeftControl) && this.name != "Empty") {
				if (inventoryWindow.transform.GetChild(0).gameObject.activeSelf && inventoryWindow.slotCnt > inventoryWindow.ReturnInventory ().Count) {
					inventoryWindow.AddItemWithAmount (ThisItem.ItemID, ThisItem.ItemCurrentAmount);
					this.transform.GetChild (0).gameObject.SetActive (false);

					ThisItem = null;

					this.transform.name = "Empty";
				}
				return;
			}
			if (itemSwap.beingDragged) {
				if (this.name != "Empty") {
					ThisItem = itemSwap.SwapItem (this.gameObject, ThisItem);
					this.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (ThisItem);
					this.name = ThisItem.ItemName;
					ShowSelectedItemText ();
				} else {
					ThisItem = itemSwap.ReturnBaseItem ();
					//playerInventory.Add (itemSwap.ReturnBaseItem ());
					this.transform.name = itemSwap.AddItemToSlot (this.gameObject, ThisItem.ItemName.ToString());
					this.transform.GetChild (0).gameObject.SetActive (true);
					//basePlayerScript.Inventory = playerInventory;
					////Debug.Log ("added item");
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
