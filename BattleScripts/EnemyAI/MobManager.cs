using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour {

	public GameObject[] mobPrefabsToSpawn;
	[SerializeField]private spawnPoint[] spawnPoints;
	[SerializeField]private Transform mobsTransform;
	public GameObject droppedItemsObject;
	public GameObject droppedItemPrefab;
	private bool[] ableToSpawn;
	private List<GameObject> mobList = new List<GameObject>();
	public int maxNumberOfMobs, maxTimeToSpawn, idealNumberOfMobs, idealTimeToSpawn;
	private int numberOfMobs;
	private float currentSpawnTime;
	private GameController gControl;


	//boss
	[SerializeField] private Transform bossSpawnPoint;
	[SerializeField] private GameObject bossPrefab;
	//boss prerequisites isQuestActive for the boss to spawn


	// Use this for initialization
	void Start () {
		gControl = FindObjectOfType<GameController> ();
		if (GameInformation.startOfScene) {
			//Debug.Log ("start of scene");
			LoadInMobs ();
		}else if (idealNumberOfMobs > numberOfMobs) {
			SpawnInMobs ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (idealNumberOfMobs > numberOfMobs) {			
			if (currentSpawnTime < idealTimeToSpawn) {
				currentSpawnTime += Time.deltaTime;
				return;
			} else {
				currentSpawnTime = 0.0f;
				SpawnInAMob ();
			}
		} else if(maxNumberOfMobs > numberOfMobs && numberOfMobs >= idealNumberOfMobs) {
			if (currentSpawnTime < maxTimeToSpawn) {
				currentSpawnTime += Time.deltaTime;
				return;
			} else {
				currentSpawnTime = 0.0f;
				SpawnInAMob ();
			}
		}
	}

	private void LoadInMobs()
	{
		GameObject temp;
		for (int i = 0; i < GameInformation.MobStatsList.Count; i++) {
			//change this so that the prefab will be in streaming assets so that you can search for the name in streaming assets
			// and not have to worry about having them all in the same array
			temp = (GameObject) Instantiate (gControl.mobPrefabArray[GameInformation.MobStatsList[i].MobTypeID] , mobsTransform);
			temp.transform.position = GameInformation.MobStatsList[i].Position;
			temp.GetComponent<EnemyHealthManager> ().currentHealth = GameInformation.MobStatsList [i].Health;
			temp.name = temp.GetComponent<EnemyHealthManager> ().mobSlug;

			mobList.Add (temp);
		}
		numberOfMobs = mobList.Count;
	}

	private void SpawnInMobs()
	{
		
		int temp;

		temp = idealNumberOfMobs - numberOfMobs;

		for (int i = 0; i < temp; i++) {
			SpawnInAMob ();
		}
		numberOfMobs = mobList.Count;
	}
	private void SpawnInAMob()
	{
		GameObject temp;
		int tempRandom = 0;
		bool keepLooping = true;
		while (keepLooping) {
			tempRandom = Random.Range (0, spawnPoints.Length);
			if (!spawnPoints [tempRandom].isPlayerInZone ()) {
				keepLooping = false;
			}
		}

		temp = (GameObject) Instantiate (mobPrefabsToSpawn [Random.Range (0, mobPrefabsToSpawn.Length)], mobsTransform);
		temp.transform.position = spawnPoints [tempRandom].transform.position;
		temp.transform.name = temp.GetComponent<EnemyHealthManager> ().mobSlug;

		mobList.Add (temp);
		numberOfMobs = mobList.Count;
	}
	public void RemoveMobOffList()
	{
		for (int i = 0; i < mobList.Count; i++) {
			if (!mobList [i].GetComponent<EnemyHealthManager> ().Alive ()) {
				mobList.RemoveAt (i);
			}
		}
		numberOfMobs = mobList.Count;
	}

	public void SpawnMinions(Vector3 bossPosition, GameObject[] minionPrefabs, int spawnAmount)
	{
		GameObject temp;
		for (int i = 0; i < spawnAmount; i++) {
			temp = (GameObject) Instantiate (minionPrefabs [Random.Range (0, minionPrefabs.Length)], mobsTransform);
			temp.transform.position = bossPosition;
			temp.transform.name = temp.GetComponent<EnemyHealthManager> ().mobSlug;

			mobList.Add (temp);
			numberOfMobs = mobList.Count;
		}
	}
}
