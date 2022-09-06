using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {

	public float moveSpeed;
	public int damageNumber;
	public Text displayNumber;

	// Use this for initialization
	void Start () {
		FindObjectOfType<SFXManager>().PlayerHit ();
	}
	
	// Update is called once per frame
	void Update () {
		displayNumber.text = damageNumber.ToString ();
		transform.position = new Vector3 (transform.position.x,
			transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
		
	}
}
