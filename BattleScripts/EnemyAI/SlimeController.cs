using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {

	public float moveSpeed;

	private Animator anim;
	private Rigidbody2D myRigidbody;
	private EnemyHealthManager thisHealthM;

	private bool moving;

	public bool playerInRange;

	public bool playerInAgroRange;

	public GameObject target;

	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;

	private Vector3 moveDirection;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		thisHealthM = GetComponent<EnemyHealthManager> ();

		//timeBetweenMoveCounter = timeBetweenMove;
		//timeToMoveCounter = timeToMove;

		timeBetweenMoveCounter = Random.Range (timeBetweenMove	* 0.75f, timeBetweenMove * 1.25f);

		timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeToMove * 1.25f);

		playerInRange = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (playerInRange) {
			myRigidbody.velocity = Vector2.zero;
			return;
		}

		if (!thisHealthM.Alive ()) {// if this enemy is not alive dont move
			return;
		}
		if (playerInAgroRange) {
			myRigidbody.velocity = Vector2.zero;
			myRigidbody.position = Vector3.MoveTowards (this.transform.position, target.transform.position, moveSpeed * Time.deltaTime);
			return;
		}

		if (moving) {

			timeToMoveCounter -= Time.deltaTime;

			myRigidbody.velocity = moveDirection;

			if (timeToMoveCounter < 0f) {
				moving = false;
				SetMoving ();
				//timeBetweenMoveCounter = timeBetweenMove;

				timeBetweenMoveCounter = Random.Range (timeBetweenMove	* 0.75f, timeBetweenMove * 1.25f);
			}

		} else {
			timeBetweenMoveCounter -= Time.deltaTime;

			myRigidbody.velocity = Vector2.zero;

			if (timeBetweenMoveCounter < 0f) {
				moving = true;
				SetMoving ();
				//timeToMoveCounter = timeToMove;

				timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeToMove * 1.25f);

				moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, Random.Range (-1f, 1f) * moveSpeed, 0f);
			}
		}
	}

	public void setIdle()
	{
		anim.SetBool ("Moving", false);
	}

	public void SetMoving()
	{
		anim.SetBool ("Moving", true);
	}
}