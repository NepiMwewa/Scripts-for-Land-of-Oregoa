using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroRange : MonoBehaviour {

	[SerializeField] private EnemyController eController;
	[SerializeField] private SlimeController sController;
	[SerializeField] private bool isSlime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player"){
			if (isSlime) {
				sController.playerInAgroRange = true;
				sController.target = other.gameObject;
				sController.SetMoving ();
			} else {
				eController.playerInAgroRange = true;
				eController.target = other.gameObject;
				eController.SetMoving ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			if (isSlime) {
				sController.playerInAgroRange = false;
			} else {
				eController.playerInAgroRange = false;
			}
		}
	}
}
