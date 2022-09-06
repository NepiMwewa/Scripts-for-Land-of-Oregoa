using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontBreak : MonoBehaviour {

	private GameObject[] gArray;
	private string tempTag;

	// Use this for initialization
	void Start () {
		tempTag = this.gameObject.tag.ToString ();

		gArray = GameObject.FindGameObjectsWithTag (tempTag);

		for (int i = 0; i < gArray.Length; i++) {
			
			if (i > 0) {
				Destroy (gArray [i].gameObject);
			} else {
				DontDestroyOnLoad (transform.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
