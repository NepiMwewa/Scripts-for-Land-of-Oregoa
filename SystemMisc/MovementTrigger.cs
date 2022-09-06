using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrigger : MonoBehaviour {

	private VillagerMovement vMovement;

	// Use this for initialization
	void Start () {
		vMovement = GetComponentInParent<VillagerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player"){
			vMovement.playerInZone = true;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			vMovement.playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			vMovement.playerInZone = false;
		}
	}
}
