using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	public int  damageToGive;
	public float damageVariance;
	private int currentDamage;
	public GameObject damageNumber;

	private Animator anim;

	private bool canAttack;
	private bool inRange;

	private GameObject player;

	public float attackWaitTime;
	private float attackWaitCounter;


	// Use this for initialization
	void Start () {
		canAttack = false;
		inRange = false;
		attackWaitCounter = 0;
		anim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (inRange) {
			if (canAttack) {
				if (!player.gameObject.GetComponent<PlayerHealthManager> ().FlashActive ()) {
					anim.SetTrigger ("Attack");
					currentDamage = damageToGive;

					currentDamage = RandomDamage ();

					if (currentDamage < 1) {
						currentDamage = 1;
					}
					player.gameObject.GetComponent<PlayerHealthManager> ().HurtPlayer (currentDamage);
					var clone = (GameObject)Instantiate (damageNumber, player.transform.position, Quaternion.Euler (Vector3.zero));
					clone.GetComponent<FloatingNumbers> ().damageNumber = currentDamage;
			

					canAttack = false;
				}
			}
		}

		if (canAttack) {
			return;// this is so the next fewlines of if statements do not run
		}

		if (attackWaitCounter >= attackWaitTime) {
			canAttack = true;
			attackWaitCounter = 0;
		} else {
			attackWaitCounter += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			inRange = true;

				player = other.gameObject;
		}
	}

	/*void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			if (canAttack) {

				player = other.gameObject;

				currentDamage = damageToGive - thePS.currentDefence;
				if (currentDamage < 1) {
					currentDamage = 1;
				}

				damageToGive = RandomDamage ();
				player.gameObject.GetComponent<PlayerHealthManager> ().HurtPlayer (currentDamage);
				var clone = (GameObject)Instantiate (damageNumber, player.transform.position, Quaternion.Euler (Vector3.zero));
				clone.GetComponent<FloatingNumbers> ().damageNumber = currentDamage;

				canAttack = false;
			}
		}
	}*/

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.name == "Player")
		{	
			inRange = false;
		}
	}


	/*void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Player") {

			currentDamage = damageToGive - thePS.currentDefence;
			if (currentDamage < 1) {
				currentDamage = 1;
			}

			damageToGive = RandomDamage ();
			other.gameObject.GetComponent<PlayerHealthManager> ().HurtPlayer (currentDamage);
			var clone = (GameObject) Instantiate (damageNumber, other.transform.position, Quaternion.Euler (Vector3.zero));
			clone.GetComponent<FloatingNumbers> ().damageNumber = currentDamage;
		}
	}*/

	private int RandomDamage()
	{
		return (int)Random.Range ((damageToGive - ( damageToGive * damageVariance)),
			( damageToGive + (damageToGive * damageVariance)));
	}
}