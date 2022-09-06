using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	private PlayerController thePlayer;
	private XMLManager xmlManager;
	private GameController gController;
	private GameObject cam;
	private GameObject GuiPanel;
	public GameObject tempC1,tempC2;

	void Awake(){
	}
	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
		xmlManager = FindObjectOfType<XMLManager> ();
		gController = FindObjectOfType<GameController> ();
		cam = GameObject.FindGameObjectWithTag ("MainCamera").gameObject;
		GuiPanel = FindObjectOfType<ItemDatabaseScript> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			if (this.transform.GetChild (0).gameObject.activeSelf) {
				ResumeGame ();
			} else {
				OpenMenu ();
			}
		}
	}

	public void OpenMenu()
	{
		Time.timeScale = 0f;
		this.transform.GetChild (0).gameObject.SetActive (true);
		thePlayer.canMove = false;
	}
	public void CloseMenu ()
	{
		this.transform.GetChild (0).gameObject.SetActive (false);
		thePlayer.canMove = true;
	}

	public void CloseGame()
	{
		Destroy (thePlayer.gameObject);
		Destroy (gController.gameObject);
		Destroy (xmlManager.gameObject);
		Destroy (cam);
		ResumeGame();
		SceneManager.LoadScene("MainMenu");
		GameInformation.GameStats = null;
		Destroy (GuiPanel);//should be the last method call in this method
	}

	public void ResumeGame()
	{
		Time.timeScale = 1f;
		CloseMenu ();
	}

	public void SaveGame()
	{
		thePlayer.UpdateGameInformation ();
		gController.UpdateGameStats (SceneManager.GetActiveScene ().name.ToString ());
		gController.UpdateNpcStats ();
		gController.UpdateMobStats ();
		thePlayer.transform.position = GameInformation.GameStats.PlayerPosition;
		xmlManager.SavePlayer ();
		xmlManager.SaveGame ();
		xmlManager.SaveNpc ();
		xmlManager.SaveMob ();
	}

	public void LoadGame()
	{
		GameInformation.loadSave = true;


		/*xmlManager.LoadPlayer ();
		xmlManager.LoadGame ();
		xmlManager.LoadNpc ();
		xmlManager.LoadMob ();
		thePlayer.UpdatePlayerStats ();
		gController.updateCurrentNpcList ();
		gController.updateCurrentMobList ();*/


		Destroy (thePlayer.gameObject);
		Destroy (gController.gameObject);
		//Destroy (xmlManager.gameObject);
		Destroy (cam);
		//SceneManager.LoadScene ("Loading_Screen");
		Destroy (GuiPanel);//should be the last method call in this method
		ResumeGame();
		loadScreen();
	}
	private void loadScreen(){
		SceneManager.LoadScene ("Loading_Screen");
	}

	public void GameOptions()
	{
		
	}
}
