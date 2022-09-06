using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float moveSpeed;

	private Animator anim;
	private Rigidbody2D myRigidbody;
	private EnemyHealthManager thisHealthM;

	private bool moving;
	public Vector2 lastMove;

	public bool playerInRange;

	public bool playerInAgroRange;

	public GameObject target;

	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;

	private Vector3 moveDirection;
	private Vector3 pos;

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
			moveDirection  = Vector3.MoveTowards (this.transform.position, target.transform.position, moveSpeed * Time.deltaTime);
			 
			pos.x = target.transform.position.x - transform.position.x;
			pos.y = target.transform.position.y - transform.position.y;

			pos.Normalize ();

			myRigidbody.position = moveDirection;
			if (pos.x >= 0.51) {
				anim.SetFloat ("MoveX", 1);
				lastMove.x = 1;
			} else if (pos.x <= -0.51) {
				anim.SetFloat ("MoveX", -1);
				lastMove.x = -1;
			} else {
				anim.SetFloat ("MoveX", 0);
				lastMove.x = 0;
			}

			if (pos.y >= 0.51) {
				anim.SetFloat ("MoveY", 1);
				lastMove.y = 1;
			} else if (pos.y <= -0.51) {
				anim.SetFloat ("MoveY", -1);
				lastMove.y = -1;
			} else {
				anim.SetFloat ("MoveY", 0);
				lastMove.y = 0;
			}
			anim.SetFloat ("LastMoveX", lastMove.x);
			anim.SetFloat ("LastMoveY", lastMove.y);
			SetMoving ();
			moveDirection = Vector3.zero;
			return;
		}

		if (moving) {

			timeToMoveCounter -= Time.deltaTime;

			myRigidbody.velocity = moveDirection;

			if (timeToMoveCounter < 0f) {
				moving = false;
				setIdle ();
				//timeBetweenMoveCounter = timeBetweenMove;

				timeBetweenMoveCounter = Random.Range (timeBetweenMove	* 0.75f, timeBetweenMove * 1.25f);
				lastMove = moveDirection;
			}
		} else {
			timeBetweenMoveCounter -= Time.deltaTime;

			myRigidbody.velocity = Vector2.zero;

			if (timeBetweenMoveCounter < 0f) {
				moving = true;
				SetMoving ();
				//timeToMoveCounter = timeToMove;

				timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeToMove * 1.25f);

				moveDirection = new Vector3 (Random.Range (-1, 2) * moveSpeed, Random.Range (-1, 2) * moveSpeed, 0f);

				if (moveDirection.x >= 0.51) {
					anim.SetFloat ("MoveX", 1);
					lastMove.x = 1;
				} else if (moveDirection.x <= -0.51) {
					anim.SetFloat ("MoveX", -1);
					lastMove.x = -1;
				} else {
					anim.SetFloat ("MoveX", 0);
					lastMove.x = 0;
				}

				if (moveDirection.y >= 0.51) {
					anim.SetFloat ("MoveY", 1);
					lastMove.y = 1;
				} else if (moveDirection.y <= -0.51) {
					anim.SetFloat ("MoveY", -1);
					lastMove.y = -1;
				} else {
					anim.SetFloat ("MoveY", 0);
					lastMove.y = 0;
				}
				anim.SetFloat ("LastMoveX", lastMove.x);
				anim.SetFloat ("LastMoveY", lastMove.y);
			}
		}
	}

	public void setIdle()
	{
		anim.SetBool ("IsMoving", false);
	}

	public void SetMoving()
	{
		anim.SetBool ("IsMoving", true);
	}
}
