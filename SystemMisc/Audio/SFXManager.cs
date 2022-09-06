using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	[SerializeField] private AudioSource[] playerHurt, playerHit, playerAttack, playerDead;

	public AudioClip clip;

	//add sound effect clips here

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;


	// Use this for initialization
	void Start () {
		
	}

	private void PlayClip (AudioSource[] tempSource)
	{
		int randId = 0;
		if (tempSource.Length > 1) {
			randId = Random.Range (0, tempSource.Length);
		}
		float randPitch = Random.Range (lowPitchRange, highPitchRange);
		tempSource[randId].pitch = randPitch;
		tempSource[randId].Play ();
		
	}

	public void PlayerHurt()
	{
		PlayClip (playerHurt);
	}
	public void PlayerHit()
	{
		PlayClip (playerHit);
	}
	public void PlayerAttack()
	{
		PlayClip (playerAttack);
	}
	public void PlayerDead()
	{
		PlayClip (playerDead);
	}
}
