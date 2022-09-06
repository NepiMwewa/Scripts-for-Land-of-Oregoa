using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfSave : MonoBehaviour {

	private XMLManager xmlManager;

	// Use this for initialization
	void Start () {
		xmlManager = FindObjectOfType<XMLManager> ();
	}


	public void CheckIfSaveFound()
	{
		xmlManager.LoadGame ();
		if (GameInformation.GameStats.MapSlug == null || GameInformation.GameStats.MapSlug.Length > 0) {
			GameInformation.saveFound = false;
		} else {
			GameInformation.saveFound = true;
		}
	}
}
