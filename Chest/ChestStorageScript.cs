using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStorageScript : MonoBehaviour {

	//private DialogManager dManager;
	private ChestWindow cWindow;
	//private InventoryWindow invWindow;
	private GameController gController;
	[SerializeField]private int numberOfChestSlots;
	[SerializeField]private bool hasChest;
	[SerializeField]private bool randomInputForItems;
	[SerializeField]private List<int> itemsInThisIDs;
	[SerializeField]private List<int>itemsInThisAmounts;
	[SerializeField] private int chestID;
	[SerializeField] private List<int> itemsThatCanSpawnInChest;
	[SerializeField]private int numberOfItemsToSpawn;
	private List<int> itemsAtChestIDs;
	private List<int> itemsAtChestAmount;
	private int gameSlotPos;
	private bool newItem;

	private bool playerInZone = false;

	//public string[] dialogueLines;

	// Use this for initialization
	void Start () {
		//dManager = FindObjectOfType<DialogManager> ();
		//invWindow = FindObjectOfType<InventoryWindow> ();
		cWindow = FindObjectOfType<ChestWindow> ();
		gController = FindObjectOfType<GameController> ();
		itemsAtChestIDs = new List<int> ();
		itemsAtChestAmount = new List<int> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (playerInZone && !dManager.dBox.activeSelf) {
		//if (dManager.CanDialog ()) {
		if (playerInZone) {
			if (Input.GetButtonUp ("Interact")) {
				if (hasChest) {
					FindChestID ();
					if (newItem) {
						//Debug.Log ("new item");
						gController.ChestIDs.Add (chestID);
						if (randomInputForItems) {
							getRandomIDsWithinLimit ();
							cWindow.OpenChestWindow (itemsAtChestIDs, itemsAtChestAmount, numberOfChestSlots,chestID);
						} else {
							gController.ChestItemIds.Add( itemsInThisIDs);
							gController.ChestItemAmounts.Add (itemsInThisAmounts);
							cWindow.OpenChestWindow (itemsInThisIDs, itemsInThisAmounts, numberOfChestSlots,chestID);
						}
					} else {
						//Debug.Log ("not new item");
						itemsAtChestIDs = gController.ChestItemIds [gameSlotPos];
						itemsAtChestAmount = gController.ChestItemAmounts [gameSlotPos];
						cWindow.OpenChestWindow (itemsAtChestIDs, itemsAtChestAmount, numberOfChestSlots,chestID);
					}
					return;
				}
			}
		}
	}

	private void FindChestID()
	{
		if (gController.ChestIDs != null) {
			for (int i = 0; i < gController.ChestIDs.Count; i++) {
				//Debug.Log ("g id " + gController.ChestIDs [i].ToString ());
				//Debug.Log (" this id " + chestID.ToString ());
				if (gController.ChestIDs [i] == chestID) {
					//Debug.Log ("not a new item");
					gameSlotPos = i;
					newItem = false;
					return;
				}
			}
		}
		newItem = true;
	}

	private void getRandomIDsWithinLimit()
	{
		int temp;
		for (int i = 0; i < numberOfItemsToSpawn; i++) {

			temp = Random.Range (0, itemsThatCanSpawnInChest.Count);
			itemsAtChestIDs.Add (itemsThatCanSpawnInChest[temp]);
			itemsAtChestAmount.Add (1);
		}
		gController.ChestItemIds.Add (itemsAtChestIDs);
		gController.ChestItemAmounts.Add (itemsAtChestAmount);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			//Debug.Log ("entered");
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			//Debug.Log ("Exit");
			playerInZone = false;

			cWindow.CloseChestWindow ();
		}
	}
}
