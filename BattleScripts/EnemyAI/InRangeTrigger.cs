using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeTrigger : MonoBehaviour {


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
				sController.playerInRange = true;
				sController.setIdle ();
			} else {
				eController.playerInRange = true;
				eController.setIdle ();
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			if (isSlime) {
				sController.playerInRange = true;
			} else {
				eController.playerInRange = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			if (isSlime) {
				sController.playerInRange = false;
				sController.SetMoving ();
			} else {
				eController.playerInRange = false;
				eController.SetMoving ();
			}
		}
	}
}
