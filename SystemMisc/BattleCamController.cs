using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamController : MonoBehaviour {

	//public GameObject followTarget;
	//private Vector3 targetPosition;
	//public float moveSpeed;

	private Camera myCam;

	//private float halfHeight;
	//private float halfWidth;

	// Use this for initialization
	void Start () {

		myCam = GetComponent<Camera> ();

		//halfHeight = Screen.height / 64f / 2f;
		//halfWidth = halfHeight * Screen.width / Screen.height;

	}

	// Update is called once per frame
	void Update () {
		myCam.orthographicSize = (Screen.height / 64f / 2f);

		/*targetPosition = new Vector3 (followTarget.transform.position.x,
			followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, targetPosition, moveSpeed * Time.deltaTime);*/
	}
		
}
