using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour {
	private bool playerInZone;
	// Use this for initialization
	void Start () {
		playerInZone = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool isPlayerInZone()
	{
		return playerInZone;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player"){
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			playerInZone = false;
		}
	}
}
