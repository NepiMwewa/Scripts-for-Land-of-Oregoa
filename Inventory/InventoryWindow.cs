using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour {

	public int slotCnt;

	public GameObject itemSlotPrefab;
	public ToggleGroup itemSlotToggleGroup;
	public GameObject guiPanel;
	[SerializeField] private BuySellButtonScript bSScript;
	[SerializeField] private ItemSwap iSwap;
	[SerializeField] private altMenuManager altMM;

	private string slotName;

	[SerializeField] private GameObject Content;
	[SerializeField] private Text goldDisplayText;
	//[SerializeField] private RPGItemDatabase data;

	private GameObject itemSlot;
	private List<GameObject> inventorySlots;

	private List<BaseItem> playerInventory;

	private List<SelectedItem> pInv;

	private List<BaseItem> itemDatabase;

	[SerializeField] private QuestManager qManager;

	//private BasePlayer basePlayerScript;

	void Awake(){
		playerInventory = new List<BaseItem> ();
		pInv = new List<SelectedItem> ();

		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;

		goldDisplayText.text = GameInformation.PlayerGoldAmount.ToString();

		CreateInventorySlotsInWindow ();
		if (!GameInformation.loadSave) {
			AddItemsFromInventory ();
		}
	}


	// Use this for initialization
	void Start () {
		goldDisplayText.text = GameInformation.PlayerGoldAmount.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void closeInvWindow()
	{
		if (iSwap.beingDragged) {
			iSwap.CloseWindows ();
		}
		CloseAltMenu ();
		transform.GetChild (0).gameObject.SetActive (false);
	}

	public List<BaseItem> ReturnInventory()
	{
		playerInventory = new List<BaseItem> ();
		for (int i = 0; i < pInv.Count; i++) {
			if (pInv [i].ThisItem 	!= null) {
				playerInventory.Add (pInv [i].ThisItem);
			}
		}

		return playerInventory;
	}

	public void OpenAltMenuByInv(SelectedItem temp)
	{
		altMM.openAltMenuByINV (temp);
	}
	public void OpenAltMenuByEq(EquipmentSelectedItem temp)
	{
		altMM.openAltMenuByEQ (temp);
	}
	public void CloseAltMenu()
	{
		altMM.closeAltMenu ();
	}

	public void RefreshPlayerInventory ()
	{
		playerInventory = new List<BaseItem> ();
		for (int i = 0; i < pInv.Count; i++) {
			if (pInv [i].ThisItem 	!= null) {
				playerInventory.Add (pInv [i].ThisItem);
			}
		}
	}

	public void RefreshItemDatabase()
	{
		//itemDScript.LoadItemDatabase ();
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;
	}

	public void RefreshPlayerGold (){
		goldDisplayText.text = GameInformation.PlayerGoldAmount.ToString();
	}

	public void SellItem(SelectedItem tempSelected)
	{
		bSScript.setupSItem (tempSelected);
	}

	public void PlayerWantsToSell(SelectedItem tempSelected)
	{
		if (tempSelected.ThisItem.ItemCurrentAmount > 1) {
			tempSelected.ThisItem.ItemCurrentAmount -= 1;
			GameInformation.PlayerGoldAmount += tempSelected.ThisItem.ItemValue;

			RefreshPlayerInventory ();
			RefreshPlayerGold ();
			if (tempSelected.ThisItem.ItemCurrentAmount > 1) {
				tempSelected.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
				tempSelected.transform.GetChild (0).GetComponentInChildren<Text> ().text = tempSelected.ThisItem.ItemCurrentAmount.ToString ();
			} else {
				tempSelected.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
			}
		} else {
			GameInformation.PlayerGoldAmount += tempSelected.ThisItem.ItemValue;
			tempSelected.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
			tempSelected.transform.GetChild (0).gameObject.SetActive (false);

			tempSelected.ThisItem = null;

			tempSelected.transform.name = "Empty";
			RefreshPlayerInventory ();
			RefreshPlayerGold ();

		}
		//Debug.Log ("item sold");

		return;
	}

	public void LoadPlayerInventory(){

		List<BaseItem> temp = GameInformation.GameStats.PlayerInventory;

		for (int i = 0; i < temp.Count; i++) {
			if (inventorySlots [i].name == "Empty") {
				//inventorySlots [i].name = i.ToString ();
				pInv[i].ThisItem = temp [i];
				inventorySlots[i].name = pInv[i].ThisItem.ItemName.ToString();

				GameObject icon = inventorySlots [i].transform.GetChild (0).gameObject;
				icon.SetActive (true);
				icon.GetComponent<Image> ().sprite = ReturnItemIcon (pInv[i].ThisItem);
				if (pInv[i].ThisItem.ItemStackable) {
					icon.transform.GetChild (0).gameObject.SetActive (true);
					icon.GetComponentInChildren<Text> ().text = pInv[i].ThisItem.ItemCurrentAmount.ToString ();
				}
			}
		}
	}
	/*
	public bool CheckItemIdAmount(int id, int amount)
	{
		RefreshPlayerInventory ();
		for (int i = 0; i < playerInventory.Count; i++) {
			if (playerInventory [i].ItemID == id) {
				if (playerInventory [i].ItemCurrentAmount >= amount) {
					return true;// enough items
				}
				return false;//not enough items
			}			
		}
		return false;//no items
	}
	*/
	public int CheckItemIdAmount(int id)
	{
		RefreshPlayerInventory();
		for (int i = 0; i < playerInventory.Count; i++)
		{
			if (playerInventory[i].ItemID == id)
			{
				return playerInventory[i].ItemCurrentAmount;
			}
		}
		return 0;//no items
	}

	public bool AddItemWithAmount(int id, int amount)
	{
		
		//itemDScript.LoadItemDatabase ();
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;
		for (int i = 0; i < itemDatabase.Count; i++) {
			if (itemDatabase[i].ItemID == id) {
				//Debug.Log ("item data: " + itemDatabase[i].ItemCurrentAmount);
				//Debug.Log ("item name" + itemDatabase[i].ItemName.ToString());
				//Debug.Log ("item type" + itemDatabase[i].ItemType.ToString());
				//Debug.Log ("item eq" + itemDatabase[i].EquipmentType.ToString());
				//Debug.Log ("item stack" + itemDatabase[i].ItemStackable.ToString());

				for (int x = 0; x < inventorySlots.Count; x++) {
					if (inventorySlots [x].name == "Empty") {
						//playerInventory.Add (data.InventoryItems()[i]);

						//itemDatabase[i].ItemCurrentAmount = 1;
						pInv[x].ThisItem = itemDatabase[i];
						pInv [x].ThisItem.ItemCurrentAmount = amount;
						inventorySlots [x].name = pInv[x].ThisItem.ItemName;

						GameObject icon = inventorySlots [x].transform.GetChild (0).gameObject;
						icon.SetActive (true);
						icon.GetComponent<Image> ().sprite = ReturnItemIcon (pInv[x].ThisItem);
						inventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);

						if (pInv [x].ThisItem.ItemCurrentAmount > 1) {
							inventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
							inventorySlots [x].transform.GetChild (0).GetComponentInChildren<Text> ().text = pInv [x].ThisItem.ItemCurrentAmount.ToString ();
						} else {
							inventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
						}

						qManager.CheckItemQuests();
						return true;
					}
				}
				//Debug.Log ("Not enough room in inventory to add the item");
				return false;
			}
		}
		//Debug.Log ("Could not find item in database");
		return false;
	}

	public void AddItemByItem(BaseItem newItem)
	{
		for (int x = 0; x < inventorySlots.Count; x++) {
			if (inventorySlots [x].name == "Empty") {
				//playerInventory.Add (data.InventoryItems()[i]);
				pInv[x].ThisItem = newItem;
				inventorySlots [x].name = pInv[x].ThisItem.ItemName;
				GameObject icon = inventorySlots [x].transform.GetChild (0).gameObject;
				icon.SetActive (true);
				icon.GetComponent<Image> ().sprite = ReturnItemIcon (pInv[x].ThisItem);

				if (pInv [x].ThisItem.ItemCurrentAmount > 1) {
					inventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
					inventorySlots [x].transform.GetChild (0).GetComponentInChildren<Text> ().text = pInv [x].ThisItem.ItemCurrentAmount.ToString ();
				} else {
					inventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
				}
				return;
			}
		}
	}

	public void AddItemByID(int id)
	{
		bool tempBool = false;

		//itemDScript.LoadItemDatabase ();
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;

		//Debug.Log ("start of loop");
		//Debug.Log (id.ToString ());
		//Debug.Log (itemDatabase[id].ItemName);
		if (itemDatabase[id].ItemStackable) {
			for (int x = 0; x < inventorySlots.Count; x++) {
				if (inventorySlots [x].name != "Empty") {
					if (pInv [x].ThisItem.ItemID == id && pInv [x].ThisItem.ItemStackable) {
						//Debug.Log ("found item");
						if (pInv [x].ThisItem.ItemCurrentAmount < pInv [x].ThisItem.ItemMaxAmount) {
							//Debug.Log ("added 1 to item amount");
							//++playerInventory [invName].ItemCurrentAmount;
							++pInv [x].ThisItem.ItemCurrentAmount;

							/*//Debug.Log ("item data: " + itemDatabase[id].ItemCurrentAmount);
							//Debug.Log ("item name" + itemDatabase[id].ItemName.ToString());
							//Debug.Log ("item type" + itemDatabase[id].ItemType.ToString());
							//Debug.Log ("item eq" + itemDatabase[id].EquipmentType.ToString());
							//Debug.Log ("item stack" + itemDatabase[id].ItemStackable.ToString());*/

							inventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
							inventorySlots [x].transform.GetChild (0).GetComponentInChildren<Text> ().text = pInv [x].ThisItem.ItemCurrentAmount.ToString ();

							//Debug.Log ("current amount: " + pInv [x].ThisItem.ItemCurrentAmount.ToString ());
							tempBool = true;
							qManager.CheckItemQuests();
							return;
						}
					}
				}
			}
		}
		if (!tempBool)
		{
			AddItemWithAmount(id, 1);
		}
	}

	public void RemoveItemByID(int id)
	{
		//playerInventory = GameInformation.PlayerInventory;
		RefreshPlayerInventory ();
		for (int i = 0; i < inventorySlots.Count; i++) {
			if (inventorySlots [i].name != "Empty") {
				if (pInv [i].ThisItem.ItemID == id) {

					if (pInv [i].ThisItem.ItemStackable && pInv [i].ThisItem.ItemCurrentAmount > 1) {
						--pInv [i].ThisItem.ItemCurrentAmount;
						inventorySlots[i].transform.GetChild (0).GetComponentInChildren<Text> ().text = pInv [i].ThisItem.ItemCurrentAmount.ToString();
						qManager.CheckItemQuests();
						return;
					} else 
					{
						pInv [i].ThisItem = null;

						inventorySlots [i].transform.GetChild (0).GetComponent<Image> ().sprite = null;
						inventorySlots [i].name = "Empty";
						inventorySlots [i].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
						inventorySlots [i].transform.GetChild (0).gameObject.SetActive (false);

						//UpdateInventory (i);
						return;
					}
				}
			}
		}
		//Debug.Log ("could not find item in inventory");
	}

	public void RemoveItemByIdPos(int id)
	{
		int invName;
		//playerInventory = GameInformation.PlayerInventory;
		for (int i = 0; i < inventorySlots.Count; i++) {
			if (inventorySlots [i].name != "Empty") {
				invName = int.Parse (inventorySlots [i].name);
				if (invName == id) {

					//GameInformation.PlayerInventory.RemoveAt (invName);
					pInv[i].ThisItem = null;

					inventorySlots [i].transform.GetChild (0).GetComponent<Image> ().sprite = null;
					inventorySlots [i].name = "Empty";
					inventorySlots [i].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
					inventorySlots [i].transform.GetChild (0).gameObject.SetActive (false);

					//UpdateInventory (invName);
					return;
				}
			}
		}
		//Debug.Log ("could not find item in inventory");
	}

	// get the number of item that was deleted then if the inventoryslot name is more then it minus its name by 1
	//so if you have 10 items and you take out item in slot name 4, 5678 and 9 will all be minused by one and become 4567 and 8

	public void UpdateInventory(int itemIndex)
	{
		for (int i = 0; i < inventorySlots.Count; i++) {
			if (inventorySlots [i].name != "Empty") {
				Debug.Log(inventorySlots[i].name);
				if (int.Parse(inventorySlots [i].name) > itemIndex) {
					inventorySlots [i].name = (int.Parse(inventorySlots[i].name) - 1).ToString ();
					GameObject icon = inventorySlots [i].transform.GetChild (0).gameObject;
					icon.GetComponent<Image> ().sprite = ReturnItemIcon (playerInventory[int.Parse(inventorySlots[i].name)]);
					qManager.CheckItemQuests();
					//Debug.Log (pInv[i].ThisItem.ItemName.ToString ());
					//Debug.Log ("data stack amount: " + itemDatabase[1].ItemCurrentAmount);
				}
			}
		}
	}
	public void RefreshInventory()
	{
		for (int i = 0; i < inventorySlots.Count; i++) {
			if (inventorySlots [i].name != "Empty") {
				inventorySlots[i].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive(true);
				inventorySlots[i].transform.GetChild (0).GetComponentInChildren<Text> ().text = playerInventory [int.Parse (inventorySlots[i].name)].ItemCurrentAmount.ToString();
			}
		}
	}

	private void CreateInventorySlotsInWindow()
	{
		inventorySlots = new List<GameObject> ();
		for (int i = 0; i < slotCnt; i++) {
			itemSlot = (GameObject)Instantiate (itemSlotPrefab);

			itemSlot.transform.SetParent (Content.transform);
			itemSlot.name = "Empty";
			itemSlot.GetComponent<Toggle> ().group = itemSlotToggleGroup;
			inventorySlots.Add (itemSlot);
			pInv.Add (itemSlot.GetComponent<SelectedItem> ());
		}
	}

	private void AddItemsFromInventory()
	{
		//playerInventory = GameInformation.PlayerInventory;
		//Debug.Log("lengh" + playerInventory.Count.ToString());
		for (int i = 0; i < playerInventory.Count; i++) {
			if (inventorySlots [i].name == "Empty") {
				inventorySlots [i].name = i.ToString ();

				GameObject icon = inventorySlots [i].transform.GetChild (0).gameObject;
				icon.SetActive (true);
				icon.GetComponent<Image> ().sprite = ReturnItemIcon (pInv[i].ThisItem);
				icon.transform.GetChild (0).gameObject.SetActive(true);
				icon.GetComponentInChildren<Text> ().text = pInv[i].ThisItem.ItemCurrentAmount.ToString();
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
