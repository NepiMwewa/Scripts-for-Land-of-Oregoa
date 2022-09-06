using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

	private VolumeManager vManager;
	private Slider thisSLiderObj;
	[SerializeField]private WhatSlider thisSliderPercent;

	private enum WhatSlider{
		master,
		sfx,
		music,
		vocal
	}

	// Use this for initialization
	void Awake () {
		vManager = FindObjectOfType<VolumeManager> ();
		thisSLiderObj = gameObject.GetComponent<Slider> ();
		switch (thisSliderPercent) {
		case WhatSlider.master:
			{
				thisSLiderObj.normalizedValue = GameInformation.AudioMaster;
				UpdateMasterVolume ();
				break;
			}
		case WhatSlider.sfx:
			{
				thisSLiderObj.normalizedValue = GameInformation.AudioSfx;
				UpdateSfxVolume ();
				break;
			}
		case WhatSlider.music:
			{
				thisSLiderObj.normalizedValue = GameInformation.AudioMusic;
				UpdateMusicVolume();
				break;
			}
		case WhatSlider.vocal:
			{
				thisSLiderObj.normalizedValue = GameInformation.AudioVocal;
				UpdateVocalVolume ();
				break;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetWhatToUpdate()
	{
		switch (thisSliderPercent) {
		case WhatSlider.master:
			{
				UpdateMasterVolume ();
				break;
			}
		case WhatSlider.sfx:
			{
				UpdateSfxVolume ();
				break;
			}
		case WhatSlider.music:
			{
				UpdateMusicVolume();
				break;
			}
		case WhatSlider.vocal:
			{
				UpdateVocalVolume ();
				break;
			}
		}
	}

	private void UpdateMasterVolume()
	{
		GameInformation.AudioMaster = thisSLiderObj.normalizedValue;
		//print (GameInformation.AudioMaster.ToString ());
		vManager.ApplyVolumeChanges ();
	}
	private void UpdateSfxVolume()
	{
		GameInformation.AudioSfx = thisSLiderObj.normalizedValue;
		vManager.ApplyVolumeChanges ();
	}
	private void UpdateMusicVolume()
	{
		GameInformation.AudioMusic = thisSLiderObj.normalizedValue;
		vManager.ApplyVolumeChanges ();
	}
	private void UpdateVocalVolume()
	{
		GameInformation.AudioVocal = thisSLiderObj.normalizedValue;
		vManager.ApplyVolumeChanges ();
	}
}
