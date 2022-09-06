using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialog : MonoBehaviour {

	private QuestManager qManager;
	private DialogManager dManager;
	private DialogHolder dHolder;

	[SerializeField]private Animator anim;

	//public List<int> quests;
	public QuestObject[] questsInDialog;
	private int questCounter = 0;
	private bool hasStarted = false;

	public bool startQuest;
	public bool endQuest;

    //if quest completed move onto the next quest

    // Use this for initialization
    void Start () {
		qManager = FindObjectOfType<QuestManager> ();
		dManager = FindObjectOfType<DialogManager> ();
		dHolder = GetComponent<DialogHolder>();
		hasStarted = true;
        RefreshQuestDialog();
	}

	private bool PlayerDidPrereqQuests(QuestObject tempQObject)
	{
		bool tempBool = false;
		if (tempQObject.prereqQuests.Length == 0) {
			return true;
		}
		for (int i = 0; i < tempQObject.prereqQuests.Length; i++) {
			if (qManager.QuestsCompleted [tempQObject.prereqQuests[i].questID]) {
				tempBool = true;
			} else {
				return false;
			}
		}

		return tempBool;
	}
	
	public void RefreshQuestDialog(){
		Debug.Log("villager name: " + gameObject.transform.parent.name.ToString());
		if (hasStarted)
		{
			for (int i = 0; i < questsInDialog.Length; i++)
			{
				questCounter = i;
				if (qManager.QuestsCompleted[questsInDialog[questCounter].questID])
				{
					startQuest = false;
					endQuest = true;
					gameObject.transform.GetChild(0).gameObject.SetActive(false);
				}
				else
				{
					if (PlayerDidPrereqQuests(qManager.quests[questsInDialog[questCounter].questID]) &&
							qManager.quests[questsInDialog[questCounter].questID].lvlToStartQuest <= GameInformation.PlayerStats.PlayerLevel)
					{

						if (qManager.quests[questsInDialog[questCounter].questID].readyToEnd)
						{
							gameObject.transform.GetChild(0).gameObject.SetActive(true);
							anim.SetTrigger("End");
							return;
						}
						else if (qManager.quests[questsInDialog[questCounter].questID].gameObject.activeSelf)
						{
							gameObject.transform.GetChild(0).gameObject.SetActive(false);
							startQuest = false;
							endQuest = false;
						}
						else
						{
							startQuest = true;
							endQuest = false;
							gameObject.transform.GetChild(0).gameObject.SetActive(true);
						}
					}
				}
			}
        }
        else
        {
			Debug.Log("sorry, questDialog has not started yet");
        }
	}
		

	public void CheckToQuest()
	{
		int lastSavedCounter = 0;
		bool isActive = false;
		bool isPrereq = false;
		bool isCompleted = false;

		for (int i = 0; i < questsInDialog.Length; i++)
		{//questID
			questCounter = i;
			Debug.Log("quest number: " + questsInDialog[questCounter].questID);
			if (qManager.QuestsCompleted[questsInDialog[questCounter].questID])
			{
				if (!(isActive || isPrereq))
				{
					isCompleted = true;
					lastSavedCounter = questCounter;
				}
			}

			if (PlayerDidPrereqQuests(qManager.quests[questsInDialog[questCounter].questID]))
			{
				if (qManager.quests[questsInDialog[questCounter].questID].gameObject.activeSelf && qManager.quests[questsInDialog[questCounter].questID].CheckItemQuest()
					|| qManager.quests[questsInDialog[questCounter].questID].isEQFinished) //ending quest
				{
					gameObject.transform.GetChild(0).gameObject.SetActive(false);//deactivate the questionmark
					qManager.quests[questsInDialog[questCounter].questID].EndQuest();
					return;
				}//starting quest dialog
				else if (startQuest && !qManager.quests[questsInDialog[questCounter].questID].gameObject.activeSelf)
				{

					if (!qManager.QuestsCompleted[questsInDialog[questCounter].questID])
					{
						if (qManager.quests[questsInDialog[questCounter].questID].lvlToStartQuest <= GameInformation.PlayerStats.PlayerLevel)
						{
							qManager.quests[questsInDialog[questCounter].questID].PromptQuest(this);
							// send this gameobject to promptquest so qmanager knows what to deactivate
							return;
						}
					}
				}
				else if(qManager.quests[questsInDialog[questCounter].questID].gameObject.activeSelf)
				{
					//if is active
					isActive = true;
					lastSavedCounter = questCounter;
				}
			}
			else
			{
				if (qManager.quests[questsInDialog[questCounter].questID].lvlToStartQuest <= GameInformation.PlayerStats.PlayerLevel && !isActive)
				{
					isPrereq = true;
					lastSavedCounter = questCounter;
				}
			}
		}

        if (isActive)
        {
			Debug.Log("is active");
			qManager.quests[questsInDialog[lastSavedCounter].questID].QuestActiveDialog();
		}
        else if (isPrereq)
        {
			Debug.Log("is prereq");
			dManager.dialogLines = qManager.quests[questsInDialog[lastSavedCounter].questID].prereqText;
			dManager.currentLine = 0;
			dManager.ShowDialog();
		}
		else if (isCompleted)
		{// quest already completed dialog
			Debug.Log("is completed");
			qManager.quests[questsInDialog[lastSavedCounter].questID].QuestCompletedDialog();
		}
        else
        {
			Debug.Log("other");
			dManager.dialogLines = dHolder.dialogueLines;
			dManager.currentLine = 0;
			dManager.ShowDialog();
		}


	}
}
