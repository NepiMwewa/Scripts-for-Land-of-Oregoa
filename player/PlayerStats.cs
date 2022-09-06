using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

//	[SerializeField] private XMLManager xmlManager;

	public BasePlayerStats currentPlayerStats;

	/*public int currentLevel;

	public int currentExp;

	public int[] toLevelUp;

	public int[] HPLevels;
	public int[] attackLevels;
	public int[] defenceLevels;

	public int currentHP;
	public int currentAttack;
	public int currentDefence;*/

	private PlayerHealthManager thePlayerHealth;
	private double healthModifier, xpModifier, strengthModifier;

	void Awake(){
		//xmlManager = FindObjectOfType<XMLManager> ();
		thePlayerHealth = FindObjectOfType<PlayerHealthManager>();
	}


	// Use this for initialization
	void Start () {
		
		LoadPlayerStats ();
		/*currentHP = HPLevels [1];
		currentAttack = attackLevels [1];
		currentDefence = defenceLevels [1];*/

		
	}
	
	// Update is called once per frame
	void Update () {
		//change to a method that when you get xp you run the method instead of this.
		if (currentPlayerStats.PlayerXP >= currentPlayerStats.XPToLevelUp)
		{
			//currentLevel++;

			//currentPlayerStats.XPToLevelUp = 50 * currentPlayerStats.PlayerLevel;

			LevelUp();
		}
	}

	public void UpdateGameInformation(){
		GameInformation.PlayerStats = currentPlayerStats;
	}

	private void LoadPlayerStats()
	{
		//xmlManager.LoadPlayer ();
		currentPlayerStats = GameInformation.PlayerStats;
		//Debug.Log("health of player: " + currentPlayerStats.CurrentHealth.ToString());
	}

	public void UpdateCurrentPS()
	{

		thePlayerHealth.playerMaxHealth = currentPlayerStats.MaxHealth;
		thePlayerHealth.playerCurrentHealth = currentPlayerStats.CurrentHealth;

	}

	public void AddExperience(int experienceToAdd)
	{
		currentPlayerStats.PlayerXP += experienceToAdd;
		if (currentPlayerStats.PlayerXP >= currentPlayerStats.XPToLevelUp) {
			LevelUp ();
		}
	}

	public void LevelUp()
	{// going to have to update the currentplayerstats.maxhealth and such after you set theplayerhealth.playermaxhealth.

		
		currentPlayerStats.PlayerLevel++;

		if (currentPlayerStats.PlayerLevel < 10) {
			healthModifier = 4;
			strengthModifier = 1.2;
			xpModifier = 2.4;

		} else if (currentPlayerStats.PlayerLevel >= 10 && currentPlayerStats.PlayerLevel < 25) {
			healthModifier = 5;
			strengthModifier = 1.4;
			xpModifier = 2.8;
		} else if(currentPlayerStats.PlayerLevel >= 25)
		{
			healthModifier = 3;
			strengthModifier = 1.6;
			xpModifier = 3.4;
		}

		currentPlayerStats.MaxHealth += (int)(currentPlayerStats.PlayerLevel * healthModifier);
		currentPlayerStats.CurrentHealth = currentPlayerStats.MaxHealth;

		thePlayerHealth.playerMaxHealth = currentPlayerStats.MaxHealth;
		thePlayerHealth.playerCurrentHealth = currentPlayerStats.CurrentHealth;

		currentPlayerStats.BaseStrength += (int)(currentPlayerStats.PlayerLevel * strengthModifier);

		//Debug.Log ("before leveling " + currentPlayerStats.PlayerXP.ToString ());
		//Debug.Log ("xp to level before leveling " + currentPlayerStats.XPToLevelUp.ToString ());

		currentPlayerStats.PlayerXP = currentPlayerStats.PlayerXP - currentPlayerStats.XPToLevelUp;

		//Debug.Log ("after leveling " + currentPlayerStats.PlayerXP.ToString ());

		currentPlayerStats.XPToLevelUp = (int)((currentPlayerStats.XPToLevelUp + currentPlayerStats.PlayerLevel) * xpModifier);

		if (FindObjectOfType<QuestDialog>() != null)
		{
			var tempQD = FindObjectsOfType<QuestDialog>();
			foreach (var item in tempQD)
			{
				item.RefreshQuestDialog();
			}
		}
		else
		{
			Debug.Log("Couldn't find questDialog in scene");
		}

		if (currentPlayerStats.PlayerXP >= currentPlayerStats.XPToLevelUp) {
			LevelUp ();
		}
	}
}
