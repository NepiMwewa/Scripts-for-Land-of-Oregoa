using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

	
	private SpriteRenderer spriteRenderer;
	[SerializeField] private int droppedID;
	[SerializeField] private quickDragItems droppedItem;
	private GameObject itemChild;
	public QuestObject questObject;

	private QuestManager qManager;

	public string itemName;//change to int

	private bool playerInZone = false;
	private InventoryWindow inv;

	// Use this for initialization
	void Start () {
		qManager = FindObjectOfType<QuestManager> ();
		inv = FindObjectOfType<InventoryWindow>();
		itemChild = this.gameObject.transform.GetChild(0).gameObject;
		RefreshQuestItem();
	}
	
	public void RefreshQuestItem()
    {
		if(!qManager.quests[questObject.questID].gameObject.activeSelf)
		{
			itemChild.SetActive(false);
		}
		else
		{
			qManager.quests[questObject.questID].CheckItemQuest();

			if (qManager.quests[questObject.questID].isIQFinished)
			{
				itemChild.SetActive(false);
			}
			else
			{
				loadSprite();
				itemChild.SetActive(true);
			}
		}
     }

	private void loadSprite()
    {

		Sprite icon;

		icon = Resources.Load<Sprite>("ItemIcons/" + droppedItem.ItemSlug);

		itemChild.GetComponent<SpriteRenderer>().sprite = icon;

	}

	public void interaction()
	{
		qManager.cLine = 0;
		qManager.itemCollected = itemName;

		if (qManager.quests[questObject.questID].endOnComplete) {
			qManager.quests[questObject.questID].EndQuest();
			itemChild.SetActive(false);
			return;
		}
		else
		{
			inv.AddItemByID(droppedItem.ItemID);
			qManager.CheckItemQuests();
		}
		//qManager.cLine = 0;
		//qManager.itemCollected = itemName;

		if (FindObjectOfType<QuestDialog>() != null) 
		{
			var tempQD = FindObjectsOfType<QuestDialog>();
            foreach (var item in tempQD)
            {
				item.RefreshQuestDialog();
            }
        }
        else
        {
			Debug.Log("Couldn't find questDialog in scene");
        }
		itemChild.SetActive(false);
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
		}
	}
}
