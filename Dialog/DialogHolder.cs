using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour {

	//public string dialog;
	private DialogManager dManager;
	private QuestDialog qDialog;
	//private InventoryWindow pInv;
	private ShopInventory sInv;
	private GameController gController;

	private bool playerInZone = false;

	public string[] dialogueLines;

	public bool hasQuest;
	public bool hasShop;
	public bool hasItem;
	[SerializeField]private List<quickDragItems> itemsForSale;
	[SerializeField]private int goldAmount;


	// Use this for initialization
	void Start () {
		dManager = FindObjectOfType<DialogManager> ();
		//pInv = FindObjectOfType<InventoryWindow> ();
		sInv = FindObjectOfType<ShopInventory> ();
		gController = FindObjectOfType<GameController> ();
		qDialog = gameObject.GetComponent<QuestDialog>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInZone && !dManager.dBox.activeSelf) {
			if (dManager.CanDialog ()) {
					
				if (Input.GetButtonUp ("Interact")) {
					if (dManager.dialogActive) {
						return;
					}
					if (GameInformation.shopOpen) {
						
						return;
					}
					if (hasItem)
					{
						QuestItem temp = this.GetComponentInParent<QuestItem>();
						temp.interaction();
						if (!temp.isActiveAndEnabled)
						{// change this to quest completed or something
							hasItem = false;
							return;
						}
					}
					gController.CloseWindows ();
					if (GetComponentInParent<VillagerMovement> () != null) {
						GetComponentInParent<VillagerMovement> ().canMove = false;
					}
					if (hasShop) {

						dManager.dialogLines = dialogueLines;
						dManager.currentLine = 0;
						dManager.ShowShopDialog(this);
						return;
					}
					if (hasQuest) {
						//qDialog.Invoke ("CheckToQuest", .1f);
						qDialog.CheckToQuest ();

					} else if (!dManager.dialogActive) {
						dManager.dialogLines = dialogueLines;
						dManager.currentLine = 0;
						//dManager.Invoke ("ShowDialog", .1f);
						dManager.ShowDialog ();
					}	
				}
			}
		}
	}

	public void ActivateShop()
    {
		//sInv.OpenShopWindowByID(itemsInThisIDs, goldAmount);
		sInv.OpenShopWindowByItem(itemsForSale, goldAmount);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			playerInZone = false;
			if (hasShop && GameInformation.shopOpen) {
				sInv.CloseShopWindow ();
				dManager.CloseShopDialog();
			}
		}
	}
}
