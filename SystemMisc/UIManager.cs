using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Slider healthBar;
	public Text HPText;
	public PlayerHealthManager playerHealth;

	[SerializeField] private PlayerStats thePlayerStats;
	public Text levelText;

	// Use this for initialization
	void Start () {			
	}

	//change to a method that when asked to update the health of the player. then set the health max value and or value

	// Update is called once per frame
	void Update () {
		healthBar.maxValue = playerHealth.playerMaxHealth;
		healthBar.value = playerHealth.playerCurrentHealth;
		HPText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
		levelText.text = "Lvl: " + thePlayerStats.currentPlayerStats.PlayerLevel + "\nExp: " + thePlayerStats.currentPlayerStats.PlayerXP 
			+ "/" + thePlayerStats.currentPlayerStats.XPToLevelUp;
	}
}
