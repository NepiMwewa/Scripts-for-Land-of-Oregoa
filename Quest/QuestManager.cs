 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class QuestManager : MonoBehaviour {

	public QuestObject[] quests;
	private GameObject questGObject;
	GameObject tempQuestObject;
	//QuestObject tempQuestObject;
	private QuestDialog tempQDialog;
	[SerializeField] private bool[] questsCompleted;
	[SerializeField] private PlayerStats playerStats;
	[SerializeField] private QuestFader qFader;
	[SerializeField] private InventoryWindow inv;

	public DialogManager dManager;

	public string itemCollected;//change this to an int for the item id database
	public int itemCollectedID;
	public int cLine = 0;

	public int currentQuestID;

	// Use this for initialization
	void Start() {

		if (GameInformation.GameStats == null)
		{
			InitializeQuests();
			QuestsCompleted = new bool[quests.Length];
		}
	}

	// Update is called once per frame
	void Update() {

	}

	public bool[] QuestsCompleted {
		get { return questsCompleted; }
		set { questsCompleted = value; }
	}

	public void InitializeQuests()
    {
		quests = new QuestObject[gameObject.transform.childCount];


		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			quests[i] = gameObject.transform.GetChild(i).GetComponent<QuestObject>();
			//quests[i].questID = i;
			quests[i].qManager = this;
			if (GameInformation.GameStats == null)
			{
				quests[i].gameObject.SetActive(false);
			}

			/*
			 * 
			 * 
			 * 
			 * Run once and comment out before final release
			 * 
			 * 
			 * 
			 */
			/*
			tempQuestObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/questTemplate/quests/" + quests[i].gameObject.name.ToString() + ".prefab",
				 typeof(GameObject)) as GameObject;


            //tempQuestObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/questTemplate/quests/" + quests[i].gameObject.name.ToString() + ".prefab",
            //	 typeof(QuestObject)) as QuestObject;

            //Debug.Log("quest number before: " + tempQuestObject.questID);

            //tempQuestObject.questID = i;
            //Debug.Log("quest number after: " + tempQuestObject.questID);
            tempQuestObject.GetComponent<QuestObject>().questID = i;
            EditorUtility.SetDirty(tempQuestObject);
			AssetDatabase.SaveAssetIfDirty(tempQuestObject);
			*/
		}


	}

	public void ShowQuestText(string[] questText)
	{

		dManager.dialogLines = new string[1];
		dManager.dialogLines = questText;

		dManager.currentLine = cLine;
		dManager.ShowDialog();
	}

	public void CheckItemQuests()
	{
		for (int i = 0; i < quests.Length; i++) {
			if (!QuestsCompleted[i]) {
				if (quests[i].gameObject.activeSelf) {
					if (quests[i].isItemQuest) {
						quests[i].CheckItemQuest();

					}
				}
			}
		}
	}
	public void CheckEnemyQuests(string enemyKilled)
	{
		for (int i = 0; i < quests.Length; i++) {
			if (!QuestsCompleted[i]) {
				if (quests[i].gameObject.activeSelf) {
					if (quests[i].isEnemyQuest) {

						quests[i].CheckEnemyQuest(enemyKilled);
					}
				}
			}
		}
	}

	public void PromptQuestText(string[] questText, int promptLine, string declineStr, string acceptStr, GameObject questMark, QuestDialog qDialog)
	{
		tempQDialog = qDialog;
		questGObject = questMark;
		dManager.dialogLines = new string[1];
		dManager.dialogLines = questText;

		dManager.currentLine = cLine;
		dManager.ShowQuestDialog(promptLine, declineStr, acceptStr);
	}

	public void acceptedQuest()
	{
		quests[currentQuestID].gameObject.SetActive(true);

		questGObject.transform.GetChild(0).gameObject.SetActive(false);//deactivate the mark
		tempQDialog.RefreshQuestDialog();
		//enemyKilled = "";

		questGObject = null;
		tempQDialog = null;

		if (FindObjectOfType<QuestItem>() != null)
		{
			var tempQI = FindObjectsOfType<QuestItem>();
			foreach (var item in tempQI)
			{
				item.RefreshQuestItem();
			}
		}
		else
		{
			Debug.Log("Couldn't find questItem in scene");
		}
	}

	public void declinedQuest()
	{
		questGObject = null;
		tempQDialog = null;
	}

	//quest object stuff
	public void sendExperience(int expToGive)
	{
		playerStats.AddExperience(expToGive);
	}
	public void refreshGold()
	{
		inv.RefreshPlayerGold();
	}
	public void givePlayerItems(quickDragItems[] itemsToGiveMultiple)
    {
		for (int i = 0; i < itemsToGiveMultiple.Length; i++)
		{
			inv.AddItemByID(itemsToGiveMultiple[i].ItemID);
		}
	}
	public void takePlayerItems(quickDragItems targetItem, int numberOfItems)
	{
		for (int i = 0; i < numberOfItems; i++)
		{
			inv.RemoveItemByID(targetItem.ItemID);
		}
	}
	public int checkItemAmount(int itemID)
    {
		return inv.CheckItemIdAmount(itemID);
    }
	public void alertUser(float displayTime, string displayText)
    {
		qFader.FadeIn(displayTime, displayText);
	}
}
