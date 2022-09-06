using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	private BattleManager bManager;

	public int enemyID;
	public int turnPosID;

	public int maxHealth{ get; set; }
	public int health{ get; set; }
	public int lvl;
	public string enemyName;
	public int numberOfDice;
	public int speed{ get; set; }
	public int attackDmg;
	public int weaponPen;
	public int armor{ get; set; }
	public int dodge;
	public int xp;

	void Awake(){
		bManager = FindObjectOfType<BattleManager> ();
		maxHealth = Random.Range (15, 30);
		health = maxHealth;
		armor = Random.Range (2, 6);
		speed = Random.Range (1, 3);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool Parrying(int parryRoll)
	{
		int attackRoll = 0;
		int diceRoll = 0;

		diceRoll = bManager.GetDiceRoll(numberOfDice);

		//Debug.Log (diceRoll.ToString ());

		attackRoll = Mathf.RoundToInt (diceRoll +
			(diceRoll * (lvl / 100)));

		//Debug.Log ("attack roll: " + attackRoll.ToString() + "\nparry roll: " + parryRoll.ToString());

		if (attackRoll > parryRoll) {
			return false;
		} else {
			return true;
		}
	}

	public bool Blocking(int blockRoll)
	{
		int attackRoll = 0;
		int diceRoll = 0;

		diceRoll = bManager.GetDiceRoll(numberOfDice);

		//Debug.Log (diceRoll.ToString ());

		attackRoll = Mathf.RoundToInt (diceRoll +
			(diceRoll * (lvl / 100)));

		//Debug.Log ("attack roll: " + attackRoll.ToString() + "\nblock roll: " + blockRoll.ToString());

		if (attackRoll > blockRoll) {
			return false;
		} else {
			return true;
		}
	}

	public int DealPlayerDmg()
	{
		int dmgToGive;
		//Debug.Log ("move hit!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

		dmgToGive = attackDmg;

		dmgToGive -= GetPenDmgReduction (bManager.GetArmorNegation());//enemies armor here

		//Debug.Log ("dmg to player " + dmgToGive.ToString());
		if (dmgToGive > 0) {
			return dmgToGive;
		}else{
			return 0;
		}
	}

	public int CalculatePlayerDmg()
	{
		int attackRoll = 0;
		int diceRoll = 0;

		diceRoll = bManager.GetDiceRoll(numberOfDice);

		//Debug.Log (diceRoll.ToString ());

		attackRoll = Mathf.RoundToInt (diceRoll +
			(diceRoll * (lvl / 100)));
		//GameInformation.PlayerStats.Strength

		//if enemy did a parry or block do the method for that.
		bManager.GetTotalDodge();
		////Debug.Log ("dodge: " + GameInformation.PlayerStats.TotalDodge.ToString () + "\nattack roll: " + attackRoll.ToString ());

		if (attackRoll > GameInformation.PlayerStats.TotalDodge) {//instead of player dodge it will be the enemies
			int dmgToGive;
			//Debug.Log ("move hit!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

			dmgToGive = attackDmg;

			dmgToGive -= GetPenDmgReduction (bManager.GetArmorNegation());//enemies armor here

			//Debug.Log ("dmg to player " + dmgToGive.ToString());
			if (dmgToGive > 0) {
				return dmgToGive;
			}else{
				return 0;
			}

		} else {
			//Debug.Log ("move missed !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			return 0;
		}
	}

	private int GetPenDmgReduction(int armorValue)
	{
		int temp = 0;

		temp = armorValue - weaponPen;

		if (temp < 0) {
			return 0;
		}
		return temp;
	}

	public void HurtTheEnemy(int dmg)
	{
		health -= dmg;
		//Debug.Log ("enemy took: " + dmg.ToString ());
		//Debug.Log ("enemy's health: " + health.ToString ());
		if (health <= 0) {
			//Debug.Log ("enemy died");
			bManager.GiveXpToParty (xp);
			bManager.numberOfMobs -= 1;
			//bManager.CheckMobs ();
			//bManager.UpdateTurnOrderSlots ();
			bManager.turnOrderSlotsString[turnPosID] = bManager.DeadTurn;

			RemoveEnemyFromBattle ();
		}
	}

	public void RemoveEnemyFromBattle()
	{
		//tell the battle manager the enemy died and check if there are any more enemies in the battle
		//if no more enemies. give the player the xp gold items etc, and then send him back to the main world
		Destroy(this.gameObject);
	}
}
