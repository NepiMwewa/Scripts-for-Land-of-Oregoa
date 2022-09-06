using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float currentMoveSpeed;
	//public float diagonalMoveModifer;

	private Animator anim;
	private Rigidbody2D myRigidbody;
	private PlayerStats thePS;

	private bool playerMoving;
	public Vector2 lastMove;
	private Vector2 moveInput;

	public int partySize;

	private static bool playerExists;

	private bool attacking;
	public float attacTime;
	private float attackTimeCounter;

	public string startPoint;
	private Vector3 playerPos;

	public bool canMove = true;

	private InventoryWindow inventoryWindow;
	private ShopInventory sInv;
	//private ItemSwap iSwap;
	private GameController gController;
	private SFXManager sManager;

	[SerializeField] private GameObject inventoryWindowChild;
	[SerializeField] private GameObject chestWindowChild;
	[SerializeField] private GameObject equipmentWindowChild;
	[SerializeField] private EquipmentWindow eWindow;
	private GameObject sInvChild;
	//[SerializeField] private GameObject inventory;

	[SerializeField] private GameObject swing_Big;

	//private InventoryWindow inv;
	//private ChestWindow chestInv;

	public bool hadHelp;

	public string respawnMapSlug, respawnPoint;

	// Use this for initialization
	void Start () {


		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		thePS = this.gameObject.GetComponent<PlayerStats> ();
		//iSwap = FindObjectOfType<ItemSwap> ();
		gController = FindObjectOfType<GameController> ();
		sManager = FindObjectOfType<SFXManager> ();

		//inv = inventoryWindow.GetComponent<InventoryWindow> ();
		//chestInv = chestWindow.GetComponent<ChestWindow> ();
		sInv = FindObjectOfType<ShopInventory> ();
		sInvChild = sInv.transform.GetChild (0).gameObject;
		inventoryWindow = FindObjectOfType<InventoryWindow> ();

		CloseInvetory ();
		CloseChest ();
		CloseEquipment ();
		CloseShopInventory ();

		/*inv.AddItemByID(1);
		inv.AddItemByID (3);
		inv.AddItemByID (4);*/

		partySize = 1;

		lastMove = new Vector2 (0, -1f);
		
	}
	
	// Update is called once per frame
	void Update () {
		playerMoving = false;

		if (!canMove) {
			myRigidbody.velocity = Vector2.zero;
			return;
		}

		if (!attacking) {

			if (Input.GetButtonDown ("Cancel")) {
				gController.CloseWindows ();
			}
			if (!GameInformation.shopOpen) {

				if (Input.GetButtonDown ("Inventory")) {
					if (inventoryWindowChild.activeSelf) {
						inventoryWindow.closeInvWindow ();
					} else if (!inventoryWindowChild.activeSelf) {
						OpenInvetory ();
					}
				}

				if (Input.GetButtonDown ("Equipment")) {
					if (equipmentWindowChild.activeSelf) {
						CloseEquipment ();
					} else if (!equipmentWindowChild.activeSelf) {
						OpenEquipment ();
					}
				}
			}

			//consume consumable slot
			if (Input.GetButtonDown ("ConsumableOne")) {
				eWindow.UseConsumableOne ();
			}
			if (Input.GetButtonDown ("ConsumableTwo")) {
				eWindow.UseConsumableTwo ();
			}

			/*if (Input.GetAxisRaw ("Horizontal") != 0f) {
				//transform.Translate (new Vector3 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
				myRigidbody.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal") * currentMoveSpeed, myRigidbody.velocity.y);
				playerMoving = true;
				lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
			}
			if (Input.GetAxisRaw ("Vertical") != 0f) {
				//transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f));
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, Input.GetAxisRaw ("Vertical") * currentMoveSpeed);
				lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
				playerMoving = true;
			}

			if (Input.GetAxisRaw ("Horizontal") == 0) {
				myRigidbody.velocity = new Vector2 (0f, myRigidbody.velocity.y);
			}

			if (Input.GetAxisRaw ("Vertical") == 0) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0f);
			}*/

			moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

			if (moveInput != Vector2.zero) {
				myRigidbody.velocity = new Vector2 (moveInput.x * moveSpeed, moveInput.y * moveSpeed);

				playerMoving = true;
				lastMove = moveInput;
			} else {
				myRigidbody.velocity = Vector2.zero;
				playerMoving = false;
			}

			if (Input.GetButtonDown("MainAttack")) {

				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);

				attackTimeCounter = attacTime;
				attacking = true;
				myRigidbody.velocity = Vector2.zero;
				anim.SetBool ("Attack", true);
				swing_Big.GetComponent<HurtEnemy> ().attacking = true;
				sManager.PlayerAttack ();
			}

			/*if (Mathf.Abs (Input.GetAxisRaw ("Horizontal")) > 0.5f &&
			   Mathf.Abs (Input.GetAxisRaw ("Vertical")) > 0.5f) {
				currentMoveSpeed = moveSpeed * diagonalMoveModifer;
			} else {
				currentMoveSpeed = moveSpeed;
			}*/
		} else if (attackTimeCounter >= 0) {
			attackTimeCounter -= Time.deltaTime;
		} else if (attackTimeCounter <= 1) {
			attacking = false;
			anim.SetBool ("Attack", false);
		}

		anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetBool ("PlayerMoving", playerMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
	}
		
	void OpenInvetory ()
	{
		inventoryWindow.CloseAltMenu ();
		inventoryWindowChild.SetActive (true);
	}

	void CloseInvetory ()
	{
		inventoryWindowChild.SetActive (false);
	}

	public void CloseShopInventory()
	{
		sInvChild.SetActive (false);
		sInv.CloseShopWindow ();
	}

	public void OpenEquipment()
	{
		inventoryWindow.CloseAltMenu ();
		equipmentWindowChild.SetActive (true);
	}
	public void CloseEquipment()
	{
		inventoryWindow.CloseAltMenu ();
		equipmentWindowChild.SetActive (false);
	}

	void CloseChest ()
	{
		
		chestWindowChild.SetActive (false);
	}

	void OpenNpcWindow()
	{
		//NpcWindow.SetActive (true);
	}

	void CloseNpcWindow()
	{
		//NpcWindow.SetActive (false);
	}

	public Vector3 PlayerPos
	{
		get{
			playerPos = this.transform.position; 
			return playerPos; 
		}
		set{
			playerPos = value;
			this.transform.position = playerPos;
		}
	}

	public void UpdateGameInformation(){

		GameInformation.PlayerStats = thePS.currentPlayerStats;

	}

	public void UpdatePlayerStats(){
		thePS.currentPlayerStats = GameInformation.PlayerStats;
		thePS.UpdateCurrentPS ();
	}

	/*public Vector3 GetPlayerPos()
	{
		playerPos = this.transform.position;
		return playerPos;
	}*/

}
