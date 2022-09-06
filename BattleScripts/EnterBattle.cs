using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour {

	private string sceneToLoad = "Battle_Scene";
	private GameController gController;
	private bool playerInZone, resetDistance;
	private Vector3 startPos, currentPos;
	public float distanceToEngage;
	private float distance;
	private GameObject playerGameObject;
	private int randomNumber;
	public int ChanceToBeAttacked, maxNumberOfMobs, minNumberOfMobs;
	public string mobSlug;

	// Use this for initialization
	void Awake () {
		gController = FindObjectOfType<GameController> ();
		playerGameObject = FindObjectOfType<PlayerController> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (playerInZone) {

			if (resetDistance) {
				startPos = playerGameObject.transform.position;
				resetDistance = false;
			}

			currentPos = playerGameObject.transform.position;

			distance = Vector3.Distance (startPos, currentPos);
			if (distance >= distanceToEngage) {
				//check if you got engaged
				//Debug.Log("walked the distance");
				resetDistance = true;
				CheckIfAttacked ();
			}


		}

	}

	private void CheckIfAttacked()
	{

		randomNumber = Random.Range (0, 100);
		//Debug.Log (randomNumber.ToString ());

		if (randomNumber <= ChanceToBeAttacked) {

			randomNumber = Random.Range (minNumberOfMobs, maxNumberOfMobs);

			GameInformation.numberOfMobs = randomNumber;
			GameInformation.mobSlugInBattle = mobSlug;

			EnterBattleScene ();


		}
	}

	private void EnterBattleScene()
	{
		//Debug.Log("entering battle scene");
		gController.UpdateGameStats (SceneManager.GetActiveScene().name.ToString());
		gController.UpdateMobStats ();
		gController.UpdateNpcStats ();
		GameInformation.fromBattle = true;
		//Debug.Log (GameInformation.GameStats.PlayerPosition.x.ToString ());


		gController.DestroyGObjectsForBattle ();

		SceneManager.LoadScene (sceneToLoad);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {

			playerInZone = true;
			resetDistance = true;
			//Debug.Log ("player entered mob zone");

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {

			playerInZone = false;
			//Debug.Log ("player Entered mob zone");


		}
	}

}
