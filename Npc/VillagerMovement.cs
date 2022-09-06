																																																																									using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour {

	public float moveSpeed;
	private Vector2 minWalkPoint;
	private Vector2 maxWalkPoint;

	private Animator anim;
	private Rigidbody2D myRigidbody;

	public bool isWalking;

	public float walkTime;
	private float walkCounter;
	public float waitTime;
	private float waitCounter;

	private int WalkDirection;

	public Collider2D walkZone;

	private bool hasWalkZone;

	public bool canMove;
	public bool playerInZone;
	private DialogManager dManager;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D> ();
		dManager = FindObjectOfType<DialogManager> ();

		waitCounter = waitTime;
		walkCounter = walkTime;

		ChooseDirection ();


		if (walkZone != null) {
			minWalkPoint = walkZone.bounds.min;
			maxWalkPoint = walkZone.bounds.max;
			hasWalkZone = true;
		}

		canMove = true;
		playerInZone = false;
	}

	// Update is called once per frame
	void Update()
	{

		if (!dManager.dialogActive)
		{
			canMove = true;

			if (!canMove || playerInZone)
			{

				myRigidbody.velocity = Vector2.zero;
				anim.SetBool("IsMoving", false);

				return;
			}

			if (isWalking)
			{
				walkCounter -= Time.deltaTime;
				

				switch (WalkDirection)
				{

					case 0:
						{
							myRigidbody.velocity = new Vector2(0, moveSpeed);
							anim.SetBool("IsMoving", true);
							anim.SetFloat("MoveX", 0);
							anim.SetFloat("MoveY", 1);
							if (hasWalkZone && maxWalkPoint.y < transform.position.y)
							{
								isWalking = false;
								waitCounter = waitTime;
							}
							break;
						}
					case 1:
						{
							myRigidbody.velocity = new Vector2(moveSpeed, 0);
							anim.SetBool("IsMoving", true);
							anim.SetFloat("MoveX", 1);
							anim.SetFloat("MoveY", 0);
							if (hasWalkZone && maxWalkPoint.x < transform.position.x)
							{
								isWalking = false;
								waitCounter = waitTime;
							}
							break;
						}
					case 2:
						{
							myRigidbody.velocity = new Vector2(0, -moveSpeed);
							anim.SetBool("IsMoving", true);
							anim.SetFloat("MoveX", 0);
							anim.SetFloat("MoveY", -1);
							if (hasWalkZone && minWalkPoint.y > transform.position.y)
							{
								isWalking = false;
								waitCounter = waitTime;
							}
							break;
						}
					case 3:
						{
							myRigidbody.velocity = new Vector2(-moveSpeed, 0);
							anim.SetBool("IsMoving", true);
							anim.SetFloat("MoveX", -1);
							anim.SetFloat("MoveY", 0);
							if (hasWalkZone && minWalkPoint.x > transform.position.x)
							{
								isWalking = false;
								waitCounter = waitTime;
							}
							break;
						}

				}

				if (walkCounter < 0)
				{
					isWalking = false;
					waitCounter = waitTime;
					anim.SetBool("IsMoving", false);
				}

			}
			else
			{
				waitCounter -= Time.deltaTime;

				myRigidbody.velocity = Vector2.zero;
				anim.SetBool("IsMoving", false);

				if (waitCounter < 0)
				{
					ChooseDirection();
				}
			}

		}
	}

	public void ChooseDirection(){
		WalkDirection = Random.Range (0, 4);
		isWalking = true;
		walkCounter = walkTime;
		anim.SetBool("IsMoving", true);
	}

	void OnColliderEnter2D(Collider2D other)
	{
		if (other.name == "Player") {
			myRigidbody.velocity = Vector2.zero;
			canMove = false;
			anim.SetBool("IsMoving", false);
		}
	}

	void OnColliderExit2D(Collider2D other)
	{
		if (other.name == "Player") {
			canMove = true;
		}
	}
}
