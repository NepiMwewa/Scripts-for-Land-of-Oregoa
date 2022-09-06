using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSwap : MonoBehaviour {


	public GameObject draggedIcon;
	private BaseItem itemBeingDragged;
	public bool beingDragged = false;
	private const int mousePositionOffset = 47;
	private InventoryWindows windowSlot;
	private ChestWindow chestWindow;
	private InventoryWindow invWindow;
	//private EquipmentWindow equipWindow;
//	private string slotName;

	private GameObject itemSlot;

	public enum InventoryWindows
	{
		playerInventory,
		chestInventory,
		equipmentInventory
	}

	void Awake(){
		chestWindow = FindObjectOfType<ChestWindow> ();
		invWindow = FindObjectOfType<InventoryWindow> ();
		//equipWindow = FindObjectOfType<EquipmentWindow> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		if (beingDragged) {
			Vector3 mousePosition = (Input.mousePosition - GameObject.FindGameObjectWithTag ("Canvas").GetComponent<RectTransform> ().localPosition);
			draggedIcon.GetComponent<RectTransform> ().localPosition = new 
				Vector3 (mousePosition.x + mousePositionOffset, mousePosition.y - mousePositionOffset, mousePosition.z);
		}
	}

	public void CloseWindows()
	{
		switch (windowSlot) {
		case InventoryWindows.chestInventory:
			{
				chestWindow.AddItemWithAmount (itemBeingDragged.ItemID, itemBeingDragged.ItemCurrentAmount);

				break;
			}
		case InventoryWindows.playerInventory:
			{
				invWindow.AddItemWithAmount (itemBeingDragged.ItemID, itemBeingDragged.ItemCurrentAmount);
				break;
			}
		case InventoryWindows.equipmentInventory:
			{
				invWindow.AddItemWithAmount (itemBeingDragged.ItemID, itemBeingDragged.ItemCurrentAmount);
				break;
			}
		}

		invWindow.CloseAltMenu ();

		draggedIcon.SetActive (false);
		itemBeingDragged = null;
		beingDragged = false;
	}

	public void ShowDraggedItem(string name, BaseItem itemBaseItem, InventoryWindows tempWindow)
	{
//		slotName = name;
		windowSlot = tempWindow;
		beingDragged = true;
		draggedIcon.SetActive (true);
		//Debug.Log ("dragged item amount " + itemBaseItem.ItemCurrentAmount.ToString ());
		itemBeingDragged = itemBaseItem;

		draggedIcon.GetComponent<Image> ().sprite = ReturnItemIcon (itemBeingDragged);

		if (itemBeingDragged.ItemCurrentAmount > 1) {
			draggedIcon.transform.GetChild (0).gameObject.SetActive (true);
			draggedIcon.GetComponentInChildren<Text> ().text = itemBeingDragged.ItemCurrentAmount.ToString ();
		} else {
			draggedIcon.transform.GetChild (0).gameObject.SetActive (false);
		}


	}

	public BaseItem ReturnBaseItem()
	{
		return itemBeingDragged;
	}

	public string AddItemToSlot(GameObject slot, string name)
	{
		slot.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (itemBeingDragged);

		draggedIcon.SetActive (false);
		itemBeingDragged = null;
		beingDragged = false;
		return name.ToString();
	}

	public BaseItem SwapItem(GameObject slot, BaseItem itemBaseItem)
	{
		BaseItem swapItem = itemBeingDragged;
		BaseItem tempItem = itemBaseItem;
//		string temp = name.ToString();

		if (tempItem.ItemID == swapItem.ItemID && tempItem.ItemStackable) {
			if (tempItem.ItemMaxAmount >= (tempItem.ItemCurrentAmount + swapItem.ItemCurrentAmount)) {
				//merge together and return null or something
				//Debug.Log ("item current amount 1: " + tempItem.ItemCurrentAmount.ToString());
				//Debug.Log ("item current amount 2: " + swapItem.ItemCurrentAmount.ToString());
				tempItem.ItemCurrentAmount = tempItem.ItemCurrentAmount + swapItem.ItemCurrentAmount;
				slot.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (tempItem);

				draggedIcon.SetActive (false);
				itemBeingDragged = null;
				beingDragged = false;
				return tempItem;
			} else {
				//item being returned will = max amount and left over will be in the item being dragged
				int tempTotal;

				//Debug.Log ("item current amount 1: " + tempItem.ItemCurrentAmount.ToString());
				//Debug.Log ("item current amount 2: " + swapItem.ItemCurrentAmount.ToString());
				tempTotal = tempItem.ItemCurrentAmount + swapItem.ItemCurrentAmount;
				//Debug.Log ("before " + tempTotal.ToString ());
				tempTotal -= tempItem.ItemMaxAmount;
				//Debug.Log ("after " + tempTotal.ToString ());

				tempItem.ItemCurrentAmount = tempItem.ItemMaxAmount;
				//Debug.Log(tempItem.ItemCurrentAmount.ToString());
				itemBeingDragged.ItemCurrentAmount = tempTotal;
				if (itemBeingDragged.ItemCurrentAmount > 1) {
					draggedIcon.transform.GetChild (0).gameObject.SetActive (true);
					draggedIcon.GetComponentInChildren<Text> ().text = itemBeingDragged.ItemCurrentAmount.ToString ();
				} else {
					draggedIcon.transform.GetChild (0).gameObject.SetActive (false);
				}
				return tempItem;
			}
		}

		itemBeingDragged = itemBaseItem;

		slot.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ReturnItemIcon (itemBeingDragged);
		//slot.name = slotName;
		draggedIcon.GetComponent<Image> ().sprite = ReturnItemIcon (itemBeingDragged);

		if (itemBeingDragged.ItemStackable) {
			draggedIcon.transform.GetChild (0).gameObject.SetActive (true);
			draggedIcon.GetComponentInChildren<Text> ().text = itemBeingDragged.ItemCurrentAmount.ToString ();
		} else {
			draggedIcon.transform.GetChild (0).gameObject.SetActive (false);
		}

		return swapItem;
	}

	private Sprite ReturnItemIcon(BaseItem item)
	{
		Sprite icon;


		icon = Resources.Load<Sprite> ("ItemIcons/" + item.ItemSlug);

		return icon;
	}

}
