using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestWindow : MonoBehaviour {

	public int slotCnt;
	[SerializeField] private int chestID;
	[SerializeField] private List<int> itemsThatCanSpawnInChest;
	[SerializeField]private int numberOfItemsToSpawn;
	private int gameSlotPos;
	//private bool newitem = true;

	public GameObject chestItemSlotPrefab;
	public GameObject guiPanel;
	public ToggleGroup chestSlotToggleGroup;

	private ItemSwap iSwap;
	private EquipmentWindow eqWindow;

	public GameObject draggedIcon;
	public BaseItem itemBeingDragged;
	public bool beingDragged = false;
	private const int mousePositionOffset = 47;
	private string slotName;

	private List<int> itemsAtChestIDs;
	private List<int> itemsAtChestAmount;
	private List<ChestSelectedItem> cInv;
	private InventoryWindow invWindow;
	private ItemDatabaseScript itemDScript;
	private GameController gController;
	private int chestIDPos;


	[SerializeField] private GameObject Content;

	private GameObject chestSlot;

	private List<GameObject> chestInventorySlots;

	private List<BaseItem> chestInventory;
	private List<BaseItem> itemDatabase;

	void Awake(){
		chestInventory = new List<BaseItem> ();
		itemsAtChestIDs = new List<int> ();
		cInv = new List<ChestSelectedItem> ();
		invWindow = FindObjectOfType<InventoryWindow> ();
		itemDScript = FindObjectOfType<ItemDatabaseScript> ();
		chestInventorySlots = new List<GameObject> ();
		gController = FindObjectOfType<GameController> ();
		iSwap = FindObjectOfType<ItemSwap> ();
		eqWindow = FindObjectOfType <EquipmentWindow> ();
	}
	// Use this for initialization
	void Start () {
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;
	}

	// Update is called once per frame
	void Update () {
	}

	public List<BaseItem> ReturnChestInventory()
	{
		chestInventory = new List<BaseItem> ();
		for (int i = 0; i < cInv.Count; i++) {
			if (cInv [i].ThisItem 	!= null) {
				chestInventory.Add (cInv [i].ThisItem);
			}
		}

		return chestInventory;
	}

	private void FindChestID()
	{
		for (int i = 0; i < gController.ChestIDs.Count; i++) {
			//Debug.Log ("g id " + gController.ChestIDs [i].ToString ());
			//Debug.Log (" this id " + chestID.ToString ());
			if (gController.ChestIDs [i] == chestID) {
				//Debug.Log ("not a new item");
				chestIDPos = i;
				return;
			}
		}
	}

	public void RefreshChestInventory ()
	{
		int temp = 0;

		for (int i = 0; i < gController.ChestItemIds[chestIDPos].Count; i++) {
			//Debug.Log("before: " + gController.ChestItemIds [chestIDPos] [i].ToString ());
		}
		//chestInventory = new List<BaseItem> ();
		gController.ChestItemIds [chestIDPos] = new List<int> ();
		gController.ChestItemAmounts [chestIDPos] = new List<int> ();
		for (int i = 0; i < cInv.Count; i++) {
			if (cInv [i].ThisItem != null) {
				//chestInventory.Add (cInv [i].ThisItem);
				//Debug.Log("thisItem id base: " + cInv[i].ThisItem.ItemID.ToString());
				gController.ChestItemIds[chestIDPos].Add(cInv[i].ThisItem.ItemID);
				gController.ChestItemAmounts[chestIDPos].Add(cInv[i].ThisItem.ItemCurrentAmount);
				//Debug.Log("thisItem id: " + gController.ChestItemIds[chestIDPos][temp].ToString());
				temp++;

			}
		}
	}


	public void OpenChestWindow(List<int> itemIDs, List<int> itemAmounts,int slotAmount,int idOfChest)
	{
		itemsAtChestIDs = itemIDs;
		itemsAtChestAmount = itemAmounts;

		slotCnt = slotAmount;
		chestID = idOfChest;

		FindChestID ();

		invWindow.CloseAltMenu ();
		eqWindow.CloseEquipmentWindow ();

		this.transform.GetChild (0).gameObject.SetActive (true);

		invWindow.transform.GetChild (0).gameObject.SetActive (true);

		RemoveSlots ();
		CreateChestSlotsInWindow ();
		LoadChestInv ();
	}

	public void CloseChestWindow()
	{
		if (iSwap.beingDragged) {
			iSwap.CloseWindows ();
		}
		if (this.transform.GetChild (0).gameObject.activeSelf) {
			RefreshChestInventory ();
			this.transform.GetChild (0).gameObject.SetActive (false);
		}
		invWindow.CloseAltMenu ();
	}

	private void RemoveSlots()
	{

		for (int i = 0; i < chestInventorySlots.Count; i++) {
			Destroy (chestInventorySlots [i]);
		}
		chestInventorySlots = new List<GameObject> ();
		cInv = new List<ChestSelectedItem> ();
	}

	public void LoadChestInv()
	{
		//Debug.Log ("length of int list " + itemsAtChestIDs.Count.ToString ());
		for (int i = 0; i < itemsAtChestIDs.Count; i++) {
			AddItemWithAmount (itemsAtChestIDs [i], itemsAtChestAmount[i]);
		}
	}

	public bool AddItemWithAmount(int id, int amount)
	{
		itemDScript.LoadItemDatabase ();
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;
		for (int i = 0; i < itemDatabase.Count; i++) {
			if (itemDatabase[i].ItemID == id) {

				for (int x = 0; x < chestInventorySlots.Count; x++) {
					if (chestInventorySlots [x].name == "Empty") {
						//playerInventory.Add (data.InventoryItems()[i]);

						//itemDatabase[i].ItemCurrentAmount = 1;
						cInv[x].ThisItem = itemDatabase[i];
						if(amount > 0 && cInv[x].ThisItem.ItemStackable)
						{
						cInv [x].ThisItem.ItemCurrentAmount = amount;
						}
						chestInventorySlots [x].name = cInv[x].ThisItem.ItemName;


						GameObject icon = chestInventorySlots [x].transform.GetChild (0).gameObject;
						icon.SetActive (true);
						icon.GetComponent<Image> ().sprite = ReturnItemIcon (cInv[x].ThisItem);
						chestInventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);

						if (cInv [x].ThisItem.ItemCurrentAmount > 1) {
							chestInventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
							chestInventorySlots [x].transform.GetChild (0).GetComponentInChildren<Text> ().text = cInv [x].ThisItem.ItemCurrentAmount.ToString ();
						} else {
							chestInventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
						}
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

	public void AddChestItemByID(int id)
	{
		itemDScript.LoadItemDatabase ();
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;
		//Debug.Log ("adding item");
		for (int i = 0; i < itemDatabase.Count; i++) {
			if (itemDatabase[i].ItemID == id) {
				for (int x = 0; x < chestInventorySlots.Count; x++) {
					if (chestInventorySlots [x].name == "Empty") {
						//playerInventory.Add (data.InventoryItems()[i]);
						//Debug.Log ("added item");
						//itemDatabase[i].ItemCurrentAmount = 1;
						cInv[x].ThisItem = itemDatabase[i];
						chestInventorySlots [x].name = cInv[x].ThisItem.ItemName;


						GameObject icon = chestInventorySlots [x].transform.GetChild (0).gameObject;
						icon.SetActive (true);
						icon.GetComponent<Image> ().sprite = ReturnItemIcon (cInv[x].ThisItem);

						if (cInv [x].ThisItem.ItemCurrentAmount > 1) {
							chestInventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
							chestInventorySlots [x].transform.GetChild (0).GetComponentInChildren<Text> ().text = cInv [x].ThisItem.ItemCurrentAmount.ToString ();
						} else {
							chestInventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
						}
						return;
					}
				}
				//Debug.Log ("Not enough room in inventory to add the item");
				return;
			}
		}
		//Debug.Log ("Could not find item in database");
		return;
	}

	// get the number of item that was deleted then if the inventoryslot name is more then it minus its name by 1
	//so if you have 10 items and you take out item in slot name 4, 5678 and 9 will all be minused by one and become 4567 and 8

	private void CreateChestSlotsInWindow()
	{
		chestInventorySlots = new List<GameObject> ();
		for (int i = 0; i < slotCnt; i++) {
			chestSlot = (GameObject)Instantiate (chestItemSlotPrefab);

			chestSlot.transform.SetParent (Content.transform);
			chestSlot.name = "Empty";
			chestSlot.GetComponent<Toggle> ().group = chestSlotToggleGroup;
			chestInventorySlots.Add (chestSlot);
			cInv.Add (chestSlot.GetComponent<ChestSelectedItem> ());
		}
	}

	private Sprite ReturnItemIcon(BaseItem item)
	{
		Sprite icon	;

		icon = Resources.Load<Sprite> ("ItemIcons/" + item.ItemSlug);

		return icon;
	}
}
