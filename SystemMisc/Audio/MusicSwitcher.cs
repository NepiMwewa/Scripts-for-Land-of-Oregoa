using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour {

	private MusicManager msManager;

	[SerializeField] private BaseMusicTrack.MusicRegion newTrack;
	[SerializeField] private bool switchOnScene;

	// Use this for initialization
	void Start () {
		msManager = FindObjectOfType<MusicManager> ();
		if (switchOnScene) {
			msManager.SwitchTrackRegion (newTrack);
			gameObject.SetActive (false);
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BackOutOfAudio()
	{
		//Debug.Log("back out of audio called");
		msManager.NextTrack ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			msManager.SwitchTrackRegion (newTrack);
			gameObject.SetActive (false);
		}
	}
}
