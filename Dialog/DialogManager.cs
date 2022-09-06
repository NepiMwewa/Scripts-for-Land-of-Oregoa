using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public GameObject dBox, qPrompt;
	public Text dText;	

	public QuestManager qManager;
	private DialogHolder dHolder;

	public bool dialogActive;
	public bool isShop;

	public string[] dialogLines;
	public int currentLine;

	private int promptLine;
	private string declineString, acceptString;

	private bool promptDialog;

	public float waitTime;
	private float waitCounter;
	private bool canDialog;

	private PlayerController thePlayer;
	// Use this for initialization
	void Start () {
		qManager = FindObjectOfType<QuestManager> ();
		thePlayer = FindObjectOfType<PlayerController> ();
		dBox.SetActive (false);
		dialogActive = false;
		dialogLines = new string[1];
		dialogLines[0] = "uninitialized";
		isShop = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (waitCounter <= 0) {
			canDialog = true;
		} else {
			waitCounter -= Time.deltaTime;
			canDialog = false;
		}
			if (dialogActive && !qPrompt.activeSelf && promptDialog) {
				if (currentLine == promptLine) {
					promptDialog = false;
					qPrompt.SetActive (true);
				}
			}
			if (dialogActive && Input.GetButtonUp ("Interact") && !qPrompt.activeSelf) {
				/*if (currentLine == promptLine) {
				qPrompt.SetActive (true);
			}*/
				currentLine++;
				if (currentLine >= dialogLines.Length) {
				
					currentLine = 0;

					waitCounter = waitTime;
					canDialog = false;

					dBox.SetActive (false);
					dialogActive = false;
					qPrompt.SetActive (false);

					thePlayer.canMove = true;
                if (isShop)
                {
					isShop = false;
					dHolder.ActivateShop();
					dHolder = null;
                }
				} else {
					//if(dialogLines[currentLine] == "Prompt")
					dText.text = dialogLines [currentLine];
				}
			}
			

	}

	public bool CanDialog()
	{
		return canDialog;
	}

	public void ShowBox(string dialog)
	{
		dialogActive = true;
		dBox.SetActive (true);
		dText.text = dialog;
	}

	public void ShowDialog()
	{
		//promptLine = -1;
		dialogActive = true;
		dBox.SetActive (true);
		dText.text = dialogLines [0];	

		thePlayer.canMove = false;
	}
	public void ShowShopDialog(DialogHolder temp)
    {
		isShop = true;
		dHolder = temp;
		dialogActive = true;
		dBox.SetActive(true);
		Debug.Log(dialogLines + ": " + dialogLines.Length);
		dText.text = dialogLines[0];
	}
	public void CloseShopDialog()
    {
		dialogActive = false;
		dBox.SetActive(false);
	}

	public void ShowHelpDialog()
	{
		dialogActive = true;
		dBox.SetActive (true);
		dText.text = dialogLines [0];
	}

	public void ShowQuestDialog(int promptUserForOption, string declineStr, string acceptStr)
	{
		promptLine = promptUserForOption;
		acceptString = acceptStr;
		declineString = declineStr;

		dialogActive = true;
		dBox.SetActive (true);
		dText.text = dialogLines [0];
		promptDialog = true;

		thePlayer.canMove = false;
	}

	public void acceptQuest()
	{
		//Debug.Log ("accepted the quest");
		promptDialog = false;

		qPrompt.SetActive (false);

		currentLine = 0;
		dialogLines = new string[1];
		dialogLines [0] = acceptString;

		dText.text = acceptString;

		qManager.acceptedQuest ();
	}
	public void declineQuest()
	{
		//Debug.Log ("declined the quest");
		promptDialog = false;

		qPrompt.SetActive (false);

		currentLine = 0;
		dialogLines = new string[1];
		dialogLines [0] = declineString;

		dText.text = dialogLines [currentLine];

		qManager.declinedQuest ();
	}
}
