using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinionSpawn : MonoBehaviour {

	public int spawnTimeDelay, spawnAmount;
	private float spawnTimer;
	[SerializeField] GameObject[] minionPrefabs;
	private MobManager mManager;

	// Use this for initialization
	void Start () {
		mManager = FindObjectOfType<MobManager> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer >= spawnTimeDelay) {
			// spawn minions
			mManager.SpawnMinions (this.transform.position, minionPrefabs, spawnAmount);

			//reset timer
			spawnTimer = 0.0f;
		} else {
			spawnTimer += Time.deltaTime;
		}
	}

}
