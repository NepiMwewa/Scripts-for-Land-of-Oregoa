using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDialogHolder : MonoBehaviour {
	//public string dialog;
	private DialogManager dManager;
	private QuestDialog qDialog;
	private PlayerController pController;

	public string[] dialogueLines;

	public bool hasQuest;

	public bool hasHelp;

	// Use this for initialization
	void Start () {
		dManager = FindObjectOfType<DialogManager> ();

		pController = FindObjectOfType<PlayerController> ();
		/*if (hasHelp) {
			dManager.dialogLines = dialogueLines;
			dManager.currentLine = 0;
			dManager.ShowHelpDialog ();
		}*/
		hasHelp = pController.hadHelp;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			if (!hasHelp) {
				if (!dManager.dialogActive) {
					dManager.dialogLines = dialogueLines;
					dManager.currentLine = 0;
					dManager.ShowDialog ();
					hasHelp = true;
					this.gameObject.SetActive (false);
					pController.hadHelp = true;
				}
			}
				
		}
	}
}
