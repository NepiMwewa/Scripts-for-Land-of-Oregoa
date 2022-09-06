using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

	private int damageToGive;
	[SerializeField] private int damageBoost, penBoost;
	public float damageVariance;
	private int currentDamage;
	public GameObject damageBurst;
	public GameObject attackMove;
	public Transform hitPoint;
	public GameObject damageNumber;
	public bool attacking;

	private PlayerStats thePS;
	private SFXManager sManager;

	// Use this for initialization
	void Start () {
		thePS = FindObjectOfType<PlayerStats> ();
		damageToGive = thePS.currentPlayerStats.BaseStrength;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//set to ontrigger2d and in the if statement add if attack is true run the code then set attack to false. 
	// if you do not hit the enemy set it attack to false aswell in the player controll script.
	// make a link between playercontroller and this script
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") {
			//Debug.Log ("slime entered");
			//if (attacking && other.gameObject.GetComponent<EnemyHealthManager>().Alive()) {
			if (other.gameObject.GetComponent<EnemyHealthManager>().Alive()) {
				//Destroy (other.gameObject);
				damageToGive = thePS.currentPlayerStats.TotalDamage + damageBoost;

				currentDamage = RandomDamage ();

				if (currentDamage < 1) {
					currentDamage = 1;
				}
				//import the min and max damage from the player and set it to min and max damage
				other.gameObject.GetComponent<EnemyHealthManager> ().HurtEnemy (currentDamage);
				//import the min and max damage from the player and set it to min and max damage
				Instantiate (damageBurst, other.transform.position, hitPoint.rotation);
				Instantiate (attackMove, other.transform.position, hitPoint.rotation);
				var clone = (GameObject)Instantiate (damageNumber, other.transform.position, Quaternion.Euler (Vector3.zero));
				clone.GetComponent<FloatingNumbers> ().damageNumber = currentDamage;
				attacking = false;
			}

		}
	}

	private int RandomDamage()
	{
		return (int)Random.Range (damageToGive - ( damageToGive * damageVariance), damageToGive + (damageToGive * damageVariance));
	}
}