using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private PlayerController thePlayer;
//	private XMLManager xmlManager;
//	private MenuManager mManager;
	private InventoryWindow pInv;
	private EquipmentWindow eInv;
	private ShopInventory sInv;
	private ChestWindow cInv;
	private ItemSwap iSwap;
	private QuestManager qManager;
	private GameObject cam;
	//private BasePlayer basePlayerScript;
	private GameObject gui;

	//public int PlayerGoldAmount{get; set;}



	private GameInformationStats tempGameStats = new GameInformationStats();
	private QuestObject[] tempQuestObject;


	private GameObject[] npcArray;
	private List<NpcStats> currentNpcStats = new List<NpcStats> ();
	private NpcStats tempNpcStats;

	private GameObject[] mobArray;
	private List<MobStats> currentMobStats = new List<MobStats> ();
	private MobStats tempMobStats;
	public GameObject[] mobPrefabArray;

	//chest items
	public List<List<int>> ChestItemIds{get; set;}
	public List<List<int>> ChestItemAmounts{ get; set; }
	public List<int> ChestIDs{get; set;}


	void Awake(){
		UpdateAccessToAssets ();

	}


	
	// Update is called once per frame
	void Update () {

		if (GameInformation.startOfScene) {
			if (GameInformation.GameStats.MapSlug == SceneManager.GetActiveScene ().name.ToString ()) {
				//Debug.Log ("start of scene number two");
				UpdateCurrentMobList ();
				UpdateCurrentNpcList ();
				GameInformation.startOfScene = false;
			}
		}

	}

	public void UpdateAccessToAssets()
	{
		thePlayer = FindObjectOfType<PlayerController> ();
//		xmlManager = FindObjectOfType<XMLManager> ();
		//mManager = FindObjectOfType<MenuManager> ();
		pInv = FindObjectOfType<InventoryWindow> ();
		eInv = FindObjectOfType<EquipmentWindow> ();
		sInv = FindObjectOfType<ShopInventory> ();
		cInv = FindObjectOfType<ChestWindow> ();
		iSwap = FindObjectOfType<ItemSwap> ();
		qManager = FindObjectOfType<QuestManager> ();
		cam = FindObjectOfType<Camera> ().gameObject;
		//basePlayerScript = FindObjectOfType<BasePlayer> ();
		gui = GameObject.FindGameObjectWithTag ("Gui");
	}

	public void DestroyGObjectsForBattle()
	{
		Destroy (thePlayer.gameObject);
		Destroy (gui);
		Destroy (cam);
	}

	public void AddStartingItems(){
	}

	public void CloseWindows()
	{
		if (iSwap.beingDragged) {
			iSwap.CloseWindows ();
		}
		cInv.CloseChestWindow ();
		pInv.closeInvWindow ();
		eInv.transform.GetChild (0).gameObject.SetActive (false);
		sInv.CloseShopWindow ();
		pInv.CloseAltMenu ();
	}

	public void UpdateGameStats(string levelToLoad){

		tempGameStats.MapSlug = levelToLoad;
		tempGameStats.PlayerPosition = thePlayer.transform.position;
		tempGameStats.PlayerLastMove = thePlayer.lastMove;
		tempGameStats.HadHelp = thePlayer.hadHelp;
		tempGameStats.CameraPosition = cam.transform.position;

		//party
		tempGameStats.PartyNumber = thePlayer.partySize;

		//battle
		tempGameStats.RespawnMapSlug = thePlayer.respawnMapSlug;
		tempGameStats.RespawnPoint = thePlayer.respawnPoint;

		//inventory/gold
		tempGameStats.PlayerInventory = pInv.ReturnInventory();
		tempGameStats.PlayerGoldAmount = GameInformation.PlayerGoldAmount;

		//equipment
		tempGameStats.EquipmentItems = eInv.GetEquipmentItems();

		//Chests
		tempGameStats.ChestItemIDs = ChestItemIds;
		tempGameStats.ChestItemAmounts = ChestItemAmounts;
		tempGameStats.ChestsID = ChestIDs;

		//Quests
		tempGameStats.QuestCompleted = new bool[qManager.QuestsCompleted.Length];
		tempGameStats.QuestCompleted = qManager.QuestsCompleted;

		int tempInt = qManager.quests.Length;

		tempGameStats.QuestActive = new bool[tempInt];
		tempGameStats.QuestPlayerHasItem = new bool[tempInt];
		tempGameStats.QuestEnemyKillCount = new int[tempInt];
		
		for (int i = 0; i < qManager.quests.Length; i++) {

			if (qManager.quests [i].gameObject.activeSelf) {
				tempGameStats.QuestActive [i] = true;

				if (qManager.quests [i].playerHasItem) {
					tempGameStats.QuestPlayerHasItem[i] = true;
				} else {
					tempGameStats.QuestPlayerHasItem[i] = false;
				}

				tempGameStats.QuestEnemyKillCount[i] = qManager.quests [i].EnemyKillCount;
			}else{
				tempGameStats.QuestActive[i] = false;
			}
		
		}

		GameInformation.GameStats = tempGameStats;

	}

	public void UpdateCurrentGameStats()
	{
		thePlayer.transform.position = GameInformation.GameStats.PlayerPosition;
		thePlayer.lastMove = GameInformation.GameStats.PlayerLastMove;
		thePlayer.hadHelp = GameInformation.GameStats.HadHelp;
		cam.transform.position = GameInformation.GameStats.CameraPosition;

		//player inv and gold
		GameInformation.PlayerGoldAmount = GameInformation.GameStats.PlayerGoldAmount;
		pInv.LoadPlayerInventory ();

		//battle
		thePlayer.respawnPoint = GameInformation.GameStats.RespawnPoint;
		thePlayer.respawnMapSlug = GameInformation.GameStats.RespawnMapSlug;

		//Equipment
		eInv.SetEquipmentItems(GameInformation.GameStats.EquipmentItems);

		//Chest
		ChestItemIds = GameInformation.GameStats.ChestItemIDs;
		ChestItemAmounts = GameInformation.GameStats.ChestItemAmounts;
		ChestIDs = GameInformation.GameStats.ChestsID;

		//quests
		qManager.InitializeQuests();
		qManager.QuestsCompleted = GameInformation.GameStats.QuestCompleted;
		
		for (int i = 0; i < qManager.quests.Length; i++) {
			
			if (GameInformation.GameStats.QuestActive [i]) {
				//qManager.quests [i].gameObject.SetActive (true);	
				

				qManager.quests [i].playerHasItem = GameInformation.GameStats.QuestPlayerHasItem [i];

				qManager.quests [i].EnemyKillCount = GameInformation.GameStats.QuestEnemyKillCount [i];

			} else {
				qManager.quests [i].gameObject.SetActive (false);
			}
		}


	}


	/*
	 *
	 *NPC
	 * 
	 */


	public void FindAllNpcsInLevel()
	{
		npcArray = GameObject.FindGameObjectsWithTag("NPC");
	}
	public void UpdateNpcStats(){
		FindAllNpcsInLevel ();

		GameObject tempObject;
		if (npcArray.Length > 0) {
			currentNpcStats = new List<NpcStats> ();
			for (int i = 0; i < npcArray.Length; i++) {
				tempObject = npcArray [i];

				//Debug.Log(npcArray[i].name.ToString());
				tempNpcStats = new NpcStats ();
				tempNpcStats.NpcName = tempObject.name.ToString ();
				tempNpcStats.Position = tempObject.transform.position;
				currentNpcStats.Add(tempNpcStats);
			}

			GameInformation.NpcStatsList = currentNpcStats;
			//GameInformation.LvlStats.mobList = new List<MobStats> ();
		} else {
			//Debug.Log ("no npcs found in current level");
		}
	}

	public void UpdateCurrentNpcList(){
		FindAllNpcsInLevel ();

		currentNpcStats = GameInformation.NpcStatsList;
		////Debug.Log (npcArray.Length.ToString());

		if (npcArray.Length > 0) {
			for (int i = 0; i < npcArray.Length; i++) {

				npcArray [i].name = currentNpcStats.ToArray () [i].NpcName.ToString ();
				npcArray [i].transform.position = currentNpcStats.ToArray()[i].Position;
			}
		} else {
			//Debug.Log ("no npcs found in current level");
		}
	}

	/*mob saves
	 *  
	 * get the prefab for the mobs, when you load a save, destory all mobs currently ingame,
	 * then repopulate it with the mob prefab and set there health.
	 * 
	 */

	public void FindAllMobsInLevel()
	{
		mobArray = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	public void UpdateMobStats(){
		FindAllMobsInLevel ();

		GameObject tempObject;
		if (mobArray.Length > 0) {
			currentMobStats = new List<MobStats> ();
			for (int i = 0; i < mobArray.Length; i++) {
				tempObject = mobArray [i];

				//Debug.Log(mobArray[i].name.ToString());
				tempMobStats = new MobStats ();
				tempMobStats.Name = tempObject.name.ToString ();
				tempMobStats.Position = tempObject.transform.position;
				tempMobStats.Health = tempObject.GetComponent<EnemyHealthManager> ().currentHealth;
				tempMobStats.MobTypeID = tempObject.GetComponent<EnemyHealthManager> ().modTypeId;
				currentMobStats.Add(tempMobStats);
			}

			GameInformation.MobStatsList = currentMobStats;
			//GameInformation.LvlStats.mobList = new List<MobStats> ();
		} else {
			//Debug.Log ("no Mobs found in current level");
		}
	}

	public void UpdateCurrentMobList(){
		FindAllMobsInLevel ();

		currentMobStats = GameInformation.MobStatsList;
		//Debug.Log ("number of mobs " + mobArray.Length.ToString());

		if (mobArray.Length > 0) {
			for (int i = 0; i < mobArray.Length; i++) {
				//Debug.Log(i.ToString());

				mobArray [i].name = currentMobStats.ToArray () [i].Name.ToString ();
				mobArray [i].transform.position = currentMobStats.ToArray()[i].Position;
			}
		} else {
			//Debug.Log ("no Mobs found in current level");
		}
	}

}
