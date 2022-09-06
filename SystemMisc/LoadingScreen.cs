using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	public bool fromSave, fromBattle, lostBattle;
	[SerializeField] private string levelToLoad;
	private Vector3 positionToSpawn;
	private PlayerController thePlayer;
	private Animator playersAnim;
	private XMLManager xmlManager;
	private GameController gController;

	// Use this for initialization
	void Start () {
		xmlManager = FindObjectOfType<XMLManager>();
		gController = FindObjectOfType<GameController> ();
		thePlayer = FindObjectOfType<PlayerController> ();
		playersAnim = thePlayer.gameObject.GetComponent<Animator> ();

		fromSave = GameInformation.loadSave;
		fromBattle = GameInformation.fromBattle;
		lostBattle = GameInformation.lostBattle;

		if (fromBattle) {
			//Debug.Log ("from battle");
			//Debug.Log (GameInformation.GameStats.PlayerPosition.x.ToString ());
			gController.UpdateAccessToAssets ();
			thePlayer.UpdatePlayerStats ();
			gController.UpdateCurrentGameStats ();

			GameInformation.startOfScene = true;

			//PositionToSpawn = GameInformation.GameStats.PlayerPosition;
			LevelToLoad = GameInformation.GameStats.MapSlug;

			GameInformation.fromBattle = false;
			GameInformation.lostBattle = false;
		}

		if (fromSave) {
			
			xmlManager.LoadPlayer ();
			xmlManager.LoadGame ();
			xmlManager.LoadNpc ();
			xmlManager.LoadMob ();
			thePlayer.UpdatePlayerStats ();

			gController.UpdateCurrentGameStats ();
			SetPlayerAnimations ();

			/*gController.updateCurrentNpcList ();
			gController.updateCurrentMobList ();*/
			GameInformation.startOfScene = true;

			PositionToSpawn = GameInformation.GameStats.PlayerPosition;
			LevelToLoad = GameInformation.GameStats.MapSlug;

			GameInformation.loadSave = false;
		
		} else {
			SetPlayerAnimations ();
		}

		//Invoke ("LoadLevel", 1);
		LoadLevel ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string LevelToLoad
	{
		get	{ return levelToLoad;}
		set{ levelToLoad = value; }
	}

	public Vector3 PositionToSpawn{
		get { return positionToSpawn; }
		set{ positionToSpawn = value; }
	}

	public void SetPlayerAnimations()
	{
		//load player apperence
		playersAnim.SetInteger("RaceId",GameInformation.PlayerStats.PlayerRaceId);
		playersAnim.SetInteger("HairId",GameInformation.PlayerStats.PlayerHairId);
		playersAnim.SetInteger("HairColorId",GameInformation.PlayerStats.PlayerHairColorId);
		playersAnim.SetInteger("EyeColorId",GameInformation.PlayerStats.PlayerEyeId);
		//end of player apperence
	}


	//basically call the xml file that the saves and such are loaded at. and if the player selects load then load them to that scene
	//and set all the npcs/enemies at the same spot that they left them when they saved it.

	public void LoadLevel()
	{
		if (fromSave || fromBattle && !lostBattle) {
			// make a method in player controller that takes a vector 2 or 3 that makes them spawn at a certion position on the map
			//then set that position here
			PositionToSpawn = GameInformation.GameStats.PlayerPosition;
			thePlayer.PlayerPos = PositionToSpawn;
			//Debug.Log ("battle won");

		} else if (lostBattle) {

			//Debug.Log ("battle lost");
			thePlayer.startPoint = GameInformation.GameStats.RespawnPoint;

		}else{
			thePlayer.startPoint = "Start_Point";
			gController.AddStartingItems ();
		}
		//SetPlayerAnimations ();

		SceneManager.LoadScene (levelToLoad);
	}
}
