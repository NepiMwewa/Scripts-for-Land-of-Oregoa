using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour {

	public int playerMaxHealth;
	public int playerCurrentHealth;

	private bool flashActive = false,isFlashOn;
	public float flashLength, flashLengthBetween;
	private float flashCounter, flashLengthCounter;
	private PlayerStats thePS;
	private MenuManager mManager;
	private SFXManager sManager;

	private SpriteRenderer playerSprite;


	// Use this for initialization
	void Start () {
		//Debug.Log("Player health before is: " + playerCurrentHealth.ToString());
		//Debug.Log("Player had health set");
		thePS = this.GetComponent<PlayerStats> ();
		playerMaxHealth = thePS.currentPlayerStats.MaxHealth;
		playerCurrentHealth = thePS.currentPlayerStats.CurrentHealth;
		//Debug.Log("Player health is now: " + playerCurrentHealth.ToString());
		//Debug.Log("Player max health is now: " + playerMaxHealth.ToString());
		playerSprite = GetComponent<SpriteRenderer> ();

		mManager = FindObjectOfType<MenuManager> ();
		sManager = FindObjectOfType<SFXManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (playerCurrentHealth <= 0) {
			//Debug.Log("Player Died, max health was at: " + playerMaxHealth.ToString());
			sManager.PlayerDead ();

			mManager.CloseGame ();
		}

		if (flashActive) 
		{

			if (flashCounter <= 0) {
				flashCounter = flashLengthBetween;
				if (isFlashOn) {
					isFlashOn = false;
					playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
				} else {
					isFlashOn = true;
					playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
				}
			}

			if (flashLengthCounter <= 0) {
				playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
				flashActive = false;
				return;
			}

			flashLengthCounter -= Time.deltaTime;
			flashCounter -= Time.deltaTime;
		}
	}

	public bool FlashActive()
	{
		return flashActive;
	}

	public void HurtPlayer(int damageToGive)
	{
		if (flashActive) {
			//Debug.Log ("blocked");
			return;
		} else {
			flashActive = true;
			isFlashOn = false;
			flashLengthCounter = flashLength;
			flashCounter = flashLengthBetween;
			playerCurrentHealth -= damageToGive;
			thePS.currentPlayerStats.CurrentHealth = playerCurrentHealth;

			//Debug.Log ("Player Health: " + playerCurrentHealth);
			sManager.PlayerHurt ();
		}
	}

	public void SetMaxHealth()
	{
		playerCurrentHealth = playerMaxHealth;
	}

}
