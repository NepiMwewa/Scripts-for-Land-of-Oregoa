using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour {
	[SerializeField] private VolumeController[] vcObjects;

	private float maxVolumeLevel = 1.0f; 
	private float currentSfxVolumeLevel, currentMusicVolumeLevel, currentVocalVolumeLevel;

	// Use this for initialization
	void Start () {
		vcObjects = FindObjectsOfType<VolumeController> ();
		LoadPP.LoadAllInformation ();
		currentSfxVolumeLevel = GameInformation.AudioSfx * GameInformation.AudioMaster;
		currentMusicVolumeLevel = GameInformation.AudioMusic * GameInformation.AudioMaster;
		currentVocalVolumeLevel = GameInformation.AudioVocal * GameInformation.AudioMaster;

		if (currentSfxVolumeLevel > maxVolumeLevel) {
			currentSfxVolumeLevel = maxVolumeLevel;
		}
		if (currentMusicVolumeLevel > maxVolumeLevel) {
			currentMusicVolumeLevel = maxVolumeLevel;
		}
		if (currentVocalVolumeLevel > maxVolumeLevel) {
			currentVocalVolumeLevel = maxVolumeLevel;
		}

		for (int i = 0; i < vcObjects.Length; i++) {
			if (vcObjects [i].isMusic) {
				vcObjects [i].SetAudioLevel (currentMusicVolumeLevel);
			} else {
				vcObjects [i].SetAudioLevel (currentSfxVolumeLevel);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ApplyVolumeChanges()
	{
		SavePP.SaveAllInformation ();
		currentSfxVolumeLevel = GameInformation.AudioSfx * GameInformation.AudioMaster;
		currentMusicVolumeLevel = GameInformation.AudioMusic * GameInformation.AudioMaster;
		currentVocalVolumeLevel = GameInformation.AudioVocal * GameInformation.AudioMaster;

		if (currentSfxVolumeLevel > maxVolumeLevel) {
			currentSfxVolumeLevel = maxVolumeLevel;
		}
		if (currentMusicVolumeLevel > maxVolumeLevel) {
			currentMusicVolumeLevel = maxVolumeLevel;
		}
		if (currentVocalVolumeLevel > maxVolumeLevel) {
			currentVocalVolumeLevel = maxVolumeLevel;
		}

		for (int i = 0; i < vcObjects.Length; i++) {
			if (vcObjects [i].isMusic) {
				vcObjects [i].SetAudioLevel (currentMusicVolumeLevel);
			} else {
				vcObjects [i].SetAudioLevel (currentSfxVolumeLevel);
			}
		}
	}
}
