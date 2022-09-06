using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuScene : MonoBehaviour {


	// Use this for initialization
	void Awake () {
		
		if (!GameObject.FindGameObjectWithTag ("MainCamera")) {
			//Debug.Log("could not find camera");
			SceneManager.LoadScene ("MainMenu");
		} else {
			//Debug.Log("found camera");
		}
	}

}
