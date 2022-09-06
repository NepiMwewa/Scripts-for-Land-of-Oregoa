using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {

	public int questID;
	[SerializeField] private string questName;

	public bool readyToEnd = false;//if the quest is completed, you are able to talk and finish it at the npc.

	public QuestManager qManager;

	public bool isItemQuest;
	public quickDragItems targetItem;
	public int numberOfItems;

	public bool isEnemyQuest;
	public EnemyHealthManager targetEnemy;
	public int enemiesToKill;

	public bool endOnComplete;

	public int lvlToStartQuest;
	public List<int> prereqQuestIDs;
	public QuestObject[] prereqQuests;

	public int expToGive;
	public quickDragItems[] itemsToGiveMultiple;
	public int goldToGive;

	public string declineStr, acceptStr;
	private int promptUserLine;

	public string[] prereqText;
	public string[] startText;
	public string[] activeText;
	public string[] endText;
	public string[] completedText;

	public bool isIQFinished = false;
	private int currentNumberOfItems;

	public bool isEQFinished = false;
	private int enemyKillCount;

	public bool playerHasItem;
	public bool playerHasKills;

	public int EnemyKillCount{
		get{ return enemyKillCount; }
		set{ enemyKillCount = value; }
	}

	public void PromptQuest(QuestDialog qDialog)
	{
		Debug.Log("prompt quest");
		Debug.Log(questID);
		qManager.currentQuestID = questID;
		promptUserLine = startText.Length - 1;
		qManager.PromptQuestText (startText, promptUserLine, declineStr, acceptStr, qDialog.gameObject, qDialog);
	}

	public void QuestCompletedDialog()
	{
		qManager.currentQuestID = questID;
		qManager.ShowQuestText(completedText);
	}

	public void QuestActiveDialog()
	{
		qManager.currentQuestID = questID;
		qManager.ShowQuestText(activeText);
	}

	public void StartQuest()
	{
		qManager.ShowQuestText (startText);
	}

	public bool CheckItemQuest()
	{
		if (isItemQuest)
		{
			currentNumberOfItems = qManager.checkItemAmount(targetItem.ItemID);
			if (currentNumberOfItems < numberOfItems)
			{
				qManager.alertUser(.75f, (currentNumberOfItems + "/" + numberOfItems + " " + targetItem.ItemName).ToString());
			}
			else if (currentNumberOfItems >= numberOfItems && !isIQFinished)
			{
				qManager.alertUser(1.75f, (numberOfItems + "/" + numberOfItems + " " + targetItem.ItemName + ".\nQuest Completed.").ToString());
			}

			if (currentNumberOfItems >= numberOfItems)
			{
				if (!isEQFinished || !isEnemyQuest)
				{
					readyToEnd = true;
				}
				isIQFinished = true;
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
				return true;
			}
			else
			{
				readyToEnd = false;
				isIQFinished = false;
			}
		}
			return false;
	}
	public void CheckEnemyQuest(string enemyKilled)
	{
		if (!isEQFinished) {//if quest is not able able to be completed
			if (targetEnemy.mobSlug == enemyKilled) {
				if (enemyKillCount < enemiesToKill) {
					++enemyKillCount;
				}
				if (enemyKillCount >= enemiesToKill) {
					qManager.alertUser(1.75f, ("You killed " + enemiesToKill + " " + targetEnemy.mobSlug + ".\nQuest Completed.").ToString ());
					if (!isIQFinished || isItemQuest) {
						readyToEnd = true;
						
					}
					isEQFinished = true;
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
				} else {
					qManager.alertUser(0.75f, (enemyKillCount + "/" + enemiesToKill + " " + targetEnemy.mobSlug).ToString ());
				}
			}
		}
	}

	public void EndQuest()
	{
		if (isItemQuest) {
			qManager.takePlayerItems(targetItem, numberOfItems);
		}

		if (itemsToGiveMultiple.Length > 0) {
			qManager.givePlayerItems(itemsToGiveMultiple);
		}
		
		if (expToGive > 0) {
			qManager.sendExperience (expToGive);
		}
		if (goldToGive > 0) {
			GameInformation.PlayerGoldAmount += goldToGive;
			qManager.refreshGold();
		}

		qManager.ShowQuestText (endText);
		qManager.QuestsCompleted [questID] = true;
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
		gameObject.SetActive (false);
		Debug.Log("End quest completed");
	}
}
