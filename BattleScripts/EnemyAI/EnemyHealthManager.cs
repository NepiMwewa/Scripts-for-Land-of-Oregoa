using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

	public int modTypeId;
	public string mobSlug;
	public int maxHealth;
	public int currentHealth;

	private InventoryWindow inv;
	private PlayerStats thePlayerStats;
	private Animator anim;

	private bool thisAlive;

	public int expToGive;
	[SerializeField] int[] itemToGive;
	[SerializeField] int[] chanceToGive;
	[SerializeField] int chanceToGiveGold,minGold,maxGold;
	
	[SerializeField] GameObject healthbar;
	[SerializeField] bool isSlime;

	public string enemyQuestName;
	private QuestManager qManager;
	private MobManager mobManager;

	// Use this for initialization
	void Start () {
		inv = FindObjectOfType<InventoryWindow> ();
		if (currentHealth == 0 || currentHealth == null) {
			currentHealth = maxHealth;
		} else {
			UpdateHealthBar ();
		}
		thisAlive = true;
		anim = GetComponent<Animator> ();

		thePlayerStats = FindObjectOfType<PlayerStats> ();
		qManager = FindObjectOfType<QuestManager> ();
		mobManager = FindObjectOfType<MobManager> ();
	}

	// Update is called once per frame
	void Update () {

		if (currentHealth <= 0 && thisAlive) {
			currentHealth = 0;
			UpdateHealthBar ();
			qManager.CheckEnemyQuests(enemyQuestName);

			thisAlive = false;
			anim.SetTrigger ("Dead");

			thePlayerStats.AddExperience (expToGive);
			GetDroppedItem ();
			mobManager.RemoveMobOffList ();

			GetComponentInChildren<HurtPlayer> ().gameObject.SetActive (false);
			if (isSlime) {
				GetComponent<CircleCollider2D> ().isTrigger = true;
			} else {
				GetComponent<CapsuleCollider2D> ().isTrigger = true;
			}

			Destroy(gameObject, 2);
		}
	}
	public bool Alive()
	{
		return thisAlive;
	}
	 
	public void HurtEnemy(int damageToGive)
	{
		if (currentHealth > 0) {
			//anim.SetTrigger ("Hurt");
			currentHealth -= damageToGive;
			UpdateHealthBar ();
			//Debug.Log (this.gameObject.name.ToString () + " Health: " + currentHealth);
		}
	}

	public void SetMaxHealth()
	{
		currentHealth = maxHealth;
	}

	private void UpdateHealthBar()
	{
		float tempTotal, tempA, tempB;
		Vector3 tempVector;
		tempA = currentHealth;
		tempB = maxHealth;

		tempTotal = tempA / tempB;

		tempVector = new Vector3 (tempTotal, healthbar.transform.localScale.y, healthbar.transform.localScale.z);

		healthbar.transform.localScale = tempVector;

	}

	private void GetDroppedItem()
	{
		int tempRandom;

		//setting up item drop
		for (int i = 0; i < chanceToGive.Length; i++) {
			//Debug.Log (i.ToString () + " run through");
			tempRandom = Random.Range (0, 100);
			if (tempRandom <= chanceToGive[i]) {
				GameObject temp;

				temp = (GameObject)Instantiate (mobManager.droppedItemPrefab);
				temp.transform.SetParent (mobManager.droppedItemsObject.transform);
				temp.GetComponent<droppedItemScript> ().setupThisItem (true, itemToGive [i], 0);
				temp.transform.position = this.transform.position;


				//Debug.Log ("got drop: " + itemToGive[i]);
			}
		}

		//setting up gold drop
		tempRandom = Random.Range (0, 100);
		if (tempRandom <= chanceToGiveGold) {
			GameObject temp;

			temp = (GameObject)Instantiate (mobManager.droppedItemPrefab);
			temp.transform.SetParent (mobManager.droppedItemsObject.transform);

			tempRandom = Random.Range (minGold, maxGold);

			temp.GetComponent<droppedItemScript> ().setupThisItem (false, 0, tempRandom);
			temp.transform.position = this.transform.position;
		}

	}
}
