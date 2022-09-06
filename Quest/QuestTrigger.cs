using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {

	private QuestManager qManager;

	private bool playerInZone = false;

	public int questID;

	public bool startQuest;
	public bool endQuest;

	// Use this for initialization
	void Start () {
		qManager = FindObjectOfType<QuestManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInZone) {

			if (Input.GetButtonUp ("Interact")) {

				if (!qManager.QuestsCompleted [questID]) {

					if (startQuest && !qManager.quests [questID].gameObject.activeSelf) {

						gameObject.transform.GetChild (0).gameObject.SetActive (false);//deactivate the mark

						qManager.cLine = 0;
						qManager.quests [questID].gameObject.SetActive (true);
						qManager.quests [questID].StartQuest ();
					}

					if (endQuest && qManager.quests [questID].gameObject.activeSelf) {

						gameObject.transform.GetChild (0).gameObject.SetActive (false);//deactivate the questionmark

						qManager.quests [questID].EndQuest ();
					}

				} else {
					//tell the user the quest is already completed or something
				}
			}
		}
		
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

	/*void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			if (Input.GetKeyUp ("Interact")) {

				if (!qManager.questsCompleted [questID]) {

					if (startQuest && !qManager.quests [questID].gameObject.activeSelf) {
					
						gameObject.transform.GetChild (0).gameObject.SetActive (false);//deactivate the mark

						qManager.cLine = 0;
						qManager.quests [questID].gameObject.SetActive (true);
						qManager.quests [questID].StartQuest ();
					}

					if (endQuest && qManager.quests [questID].gameObject.activeSelf) {
					
						gameObject.transform.GetChild (0).gameObject.SetActive (false);//deactivate the questionmark

						qManager.quests [questID].EndQuest ();
					}

				} else {
					//tell the user the quest is already completed or something
				}
			}
		}
	}*/
}
