using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour {

	private static bool thisExists = false;

	void Awake(){
		if (!thisExists) {
			thisExists = true;
			DontDestroyOnLoad (this.transform.gameObject);
		} else {
			Destroy (this.transform.gameObject);
		}

	}

	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (this.gameObject);


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
