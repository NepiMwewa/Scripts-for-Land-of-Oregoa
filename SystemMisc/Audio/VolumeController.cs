using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController: MonoBehaviour {

	private AudioSource theAudio;

	public bool isMusic;

	private float audioLevel = 0;
	[SerializeField] private float defaultAudio; 

	// Use this for initialization
	void Start () {
		theAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetAudioLevel(float volume)
	{
		if (theAudio == null) {
			theAudio = GetComponent<AudioSource> ();
		}
		////Debug.Log ("music + " + volume.ToString ());
		
		audioLevel = defaultAudio * volume;
		theAudio.volume = audioLevel;
	}
}
