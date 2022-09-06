using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSelectedItem : MonoBehaviour, IPointerDownHandler {


	private Text selectedItemText;

	private ShopInventory shopWindow;
	private ItemSwap iSwap;
	public BaseItem ThisItem{ get; set; }

	public bool removeItem = true;
	// Use this for initialization
	void Start () {
		selectedItemText = GameObject.Find ("SelectedItemText").transform.GetChild (0).GetComponent<Text> ();
		shopWindow = FindObjectOfType<ShopInventory> ();
		iSwap = FindObjectOfType<ItemSwap> ();
	}
	
	// Update is called once per frame
	void Update () {
		
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


	public void OnPointerDown(PointerEventData eventData)// showseletectitem text
	{
		ShowSelectedItemText ();
		if (this.name != "ShopInventory") {
			if (!iSwap.beingDragged && this.name != "Empty") {
				//Debug.Log ("buy");
				if (Input.GetKey (KeyCode.LeftControl)) {
					shopWindow.PlayerWantsToBuy (ThisItem);
					return;
				}

				//if player has enough for this item show a popup asking if they want to buy it.
				//if player doesn't have enough or room then say transaction failed in a pop up

				shopWindow.BuyItem (ThisItem);
			}
		}
	}

}
