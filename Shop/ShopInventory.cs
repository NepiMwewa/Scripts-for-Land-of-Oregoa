using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour {

	public int shopGoldAmount;

	private List<int> itemsAtShopIDs;
	private List<quickDragItems> itemsAtShopObjects;

	public int slotCnt;

	public GameObject ShopSlotPrefab;
	public ToggleGroup ShopSlotToggleGroup;
	public GameObject guiPanel;
	private BuySellButtonScript bSScript;

	private string slotName;

	private InventoryWindow pInv;

	[SerializeField] private GameObject Content;
	[SerializeField] private Text goldDisplayText;

	private GameObject itemSlot;
	private List<GameObject> shopInventorySlots;

	private List<ShopSelectedItem> shopInv;

	private List<BaseItem> itemDatabase;

	void Awake(){
		shopInventorySlots = new List<GameObject> ();
		shopInv = new List<ShopSelectedItem> ();
		pInv = FindObjectOfType<InventoryWindow> ();
		bSScript = FindObjectOfType<BuySellButtonScript> ();
	}


	// Use this for initialization
	void Start () {
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BuyItem(BaseItem item)
	{
		bSScript.setupBItem (item);
	}

	public void PlayerWantsToBuy(BaseItem item)
	{
		if (pInv.slotCnt > pInv.ReturnInventory ().Count) {
			if (GameInformation.PlayerGoldAmount >= item.ItemValue) {
				pInv.AddItemByID (item.ItemID);
				GameInformation.PlayerGoldAmount -= item.ItemValue;
				//shopGoldAmount += item.ItemValue;
				pInv.RefreshPlayerGold ();
				return;
			}
			//not enough gold
			bSScript.NotEnoughGold();
			//Debug.Log("not enough gold");
			return;
		}
		//not enough room
		bSScript.NotEnoughSpace();
		//Debug.Log("not enough room");
		return;
	}

	public void CloseShopWindow ()
	{
		bSScript.CloseThis ();
		GameInformation.shopOpen = false;
		this.transform.GetChild (0).gameObject.SetActive (false);
	}

	public void OpenShopWindowByID(List<int> itemIDs, int shopGAmount)
	{
		itemsAtShopIDs = itemIDs;
		shopGoldAmount = shopGAmount;

		slotCnt = itemsAtShopIDs.Count;

		pInv.CloseAltMenu ();

		this.transform.GetChild (0).gameObject.SetActive (true);
		GameInformation.shopOpen = true;
		pInv.transform.GetChild (0).gameObject.SetActive (true);

		goldDisplayText.text = shopGoldAmount.ToString();

		RemoveSlots ();
		CreateShopInventorySlotsInWindow ();
		LoadShopinventoryByID ();

	}

	public void OpenShopWindowByItem(List<quickDragItems> itemObjects, int shopGAmount)
	{
		itemsAtShopObjects = itemObjects;
		shopGoldAmount = shopGAmount;

		slotCnt = itemsAtShopObjects.Count;

		pInv.CloseAltMenu();

		this.transform.GetChild(0).gameObject.SetActive(true);
		GameInformation.shopOpen = true;
		pInv.transform.GetChild(0).gameObject.SetActive(true);

		goldDisplayText.text = shopGoldAmount.ToString();

		RemoveSlots();
		CreateShopInventorySlotsInWindow();
		LoadShopinventoryByObject();

	}

	public void AddItemToShopByID(int id)
	{
		//itemDScript.LoadItemDatabase ();
		itemDatabase = new List<BaseItem> ();
		itemDatabase = GameInformation.ItemDatabase;
		for (int i = 0; i < itemDatabase.Count; i++) {
			if (itemDatabase[i].ItemID == id) {
				for (int x = 0; x < shopInventorySlots.Count; x++) {
					if (shopInventorySlots [x].name == "Empty") {
						//playerInventory.Add (data.InventoryItems()[i]);

						//itemDatabase[i].ItemCurrentAmount = 1;
						shopInv[x].ThisItem = itemDatabase[i];
						shopInventorySlots [x].name = shopInv[x].ThisItem.ItemName;


						GameObject icon = shopInventorySlots [x].transform.GetChild (0).gameObject;
						icon.SetActive (true);
						icon.GetComponent<Image> ().sprite = ReturnItemIcon (shopInv[x].ThisItem);
	
						shopInventorySlots [x].transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (true);
						shopInventorySlots [x].transform.GetChild (0).GetComponentInChildren<Text> ().text = shopInv [x].ThisItem.ItemValue.ToString ();

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

	private void RemoveSlots()
	{

		for (int i = 0; i < shopInventorySlots.Count; i++) {
			Destroy (shopInventorySlots [i]);
		}
		shopInventorySlots = new List<GameObject> ();
		shopInv = new List<ShopSelectedItem> ();
	}

	private void LoadShopinventoryByID(){
		for (int i = 0; i < itemsAtShopIDs.Count; i++) {
			AddItemToShopByID (itemsAtShopIDs [i]);
		}
	}
	private void LoadShopinventoryByObject()
	{
		for (int i = 0; i < itemsAtShopObjects.Count; i++)
		{
			AddItemToShopByID(itemsAtShopObjects[i].ItemID);
		}
	}
	private void CreateShopInventorySlotsInWindow()
	{
		shopInventorySlots = new List<GameObject> ();
		for (int i = 0; i < slotCnt; i++) {
			itemSlot = (GameObject)Instantiate (ShopSlotPrefab);

			itemSlot.transform.SetParent (Content.transform);
			itemSlot.name = "Empty";
			itemSlot.GetComponent<Toggle> ().group = ShopSlotToggleGroup;
			shopInventorySlots.Add (itemSlot);
			shopInv.Add (itemSlot.GetComponent<ShopSelectedItem> ());
		}
	}

	private Sprite ReturnItemIcon(BaseItem item)
	{
		Sprite icon;

		icon = Resources.Load<Sprite> ("ItemIcons/" + item.ItemSlug);

		return icon;
	}
}
