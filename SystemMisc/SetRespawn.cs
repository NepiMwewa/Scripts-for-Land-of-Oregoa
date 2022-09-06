using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawn : MonoBehaviour {

	[SerializeField]private string mapSlug;
	[SerializeField]private string respawnPoint;
	private PlayerController pControl;

	// Use this for initialization
	void Start () {

		pControl = FindObjectOfType<PlayerController> ();

		pControl.respawnMapSlug = mapSlug;
		pControl.respawnPoint = respawnPoint;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
