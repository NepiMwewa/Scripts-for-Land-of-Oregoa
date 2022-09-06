using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject followTarget;
	private Vector3 targetPosition;
	private float distanceBetween;
	public float moveSpeed;
	public float distToMove;

	private static bool cameraExists;
	private Camera myCam;

	public bool IsCreationScene;//turn on if its creation scene if not leave off

	public BoxCollider2D boundBox;
	private Vector3 minBounds;
	private Vector3 maxBounds;

	private float halfHeight;
	private float halfWidth;

	// Use this for initialization
	void Start () {
		if (!IsCreationScene) {
			DontDestroyOnLoad (transform.gameObject);
		}

		myCam = GetComponent<Camera> ();

		if (boundBox != null) {
			minBounds = boundBox.bounds.min;
			maxBounds = boundBox.bounds.max;
		}
		myCam.orthographicSize = (Screen.height / 128f);
		halfHeight = Screen.height / 64f / 2f;
		halfWidth = halfHeight * Screen.width / Screen.height;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!IsCreationScene) {
			MoveCamera ();
		}
			
	}

	private void MoveCamera()
	{
		targetPosition = new Vector3 (followTarget.transform.position.x,
			followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, moveSpeed * Time.deltaTime);
		
		distanceBetween = Vector3.Distance (transform.position, targetPosition);
		if (distanceBetween > distToMove) {
			transform.position = Vector3.Lerp (transform.position, targetPosition, moveSpeed * Time.deltaTime);
		}

		 if (boundBox != null) {
			float clampedX = Mathf.Clamp (transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
			float clampedY = Mathf.Clamp (transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
			transform.position = new Vector3 (clampedX, clampedY, transform.position.z);
		} 
	
	}

	public void SetBounds(BoxCollider2D newBounds)
	{
		boundBox = newBounds;

		minBounds = boundBox.bounds.min;
		maxBounds = boundBox.bounds.max;

		halfHeight = Screen.height / 64f / 2f;
		halfWidth = halfHeight * Screen.width / Screen.height;
	}
}
