using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

	//private XMLManager xmlManager;
	//private EnemyStats eStats;

	private enum BattleStates
	{
		PlayerTurn,
		EnemyTurn,
		Calculate,
		DisplayResult
	}


	//battle state 0=players turn, 1=enemies turn, 2=calcState, 3=display result
	private BattleStates battleState;
	private bool turnState = true;

	public GameObject statsPanel, topOptionsPanel, attackOptionsPanel, attackOptionsSlots, interactPanel, abilityOptionsPanel, abilityOptionsSlots, inventoryOptionsPanel, inventoryOptionsSlots, enemyPanel;
	[SerializeField]private GameObject battleStatSlotPrefab;
	[SerializeField]private GameObject attackOptionsSlotPrefab;
	[SerializeField]private GameObject abilityOptionsSlotPrefab;
	[SerializeField]private GameObject inventoryOptionsSlotPrefab;
	[SerializeField]private GameObject enemySlotPrefab;
	[SerializeField]private GameObject enemyPrefab;
	[SerializeField]private GameObject[] enemySlots;
	private Text slotText;

	[SerializeField] private int[] attackIntID;
	private List<GameObject> AttackSlotsList;

	//equipment slots
	private List<BaseItem> consumablesList;
	private List<GameObject> consumablesGameObjectList;

	public int numberOfMobs;
	private int numberOfPartyMembers = 1;
	private int currentPartyTurn = 0;
	//private int currentMobTurn = 0;
	private TurnMoves[] turnMoves;
	private TurnMoves tempMove;
	private EnemyStats[] EnemyToAttack;
	private BaseItem[] turnConsumable;
	private GameObject tempSlot;
    private List<GameObject> partySlotsList;

	public string[] turnOrderSlotsString;
	public int[] turnOrderSlotsInt;

	private string pTurn = "p";
	private string mTurn = "m";
	public string DeadTurn = "d";

	private EnemyStats[] mobStats;
	private List<GameObject> mobSlotsList;
	//private bool attackOptions = false;

	public enum TurnMoves
	{
		Melee,
		Ranged,
		Parry,
		Block,
		Consumable
	}

	void Awake(){
		partySlotsList = new List<GameObject> ();
		AttackSlotsList = new List<GameObject> ();
		mobSlotsList = new List<GameObject> ();
		consumablesList = new List<BaseItem> ();
		consumablesGameObjectList = new List<GameObject> ();
		//xmlManager = FindObjectOfType<XMLManager> ();
		///eStats = FindObjectOfType<EnemyStats> ();
	
		numberOfPartyMembers = GameInformation.GameStats.PartyNumber;
		numberOfMobs = GameInformation.numberOfMobs;

		//Delete this once done with testing battle scene
//		xmlManager.LoadBattleData ();
//		xmlManager.LoadPlayer();//instead of loading it in, when you first enter the battle the gameinformation does not get destoyed
//		xmlManager.LoadGame();
		//end
		mobStats = new EnemyStats[numberOfMobs];
		AddBattleStatsSlots ();
		AddEnemySlots ();
		AddItemSlots ();
		//AddBattleAttackSlots ();
		attackOptionsPanel.SetActive (false);
		abilityOptionsPanel.SetActive (false);
		inventoryOptionsPanel.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		////Debug.Log ("start of scene");
		////Debug.Log (GameInformation.mobSlugInBattle.ToString ());
		////Debug.Log (GameInformation.numberOfMobs.ToString ());


		////Debug.Log ("Exiting Battle Scene");

		battleState = BattleStates.PlayerTurn;

		turnMoves = new TurnMoves[numberOfPartyMembers];
		EnemyToAttack = new EnemyStats[numberOfPartyMembers];
		turnConsumable = new BaseItem[numberOfPartyMembers];

		/*
		List<BattleData> bDataList = new List<BattleData> ();

		for (int i = 0; i < 2; i++) {
			BattleData bData = new BattleData ();

			bData.AttackDescription = "descript";
			bData.AttackDmg = 1;
			bData.AttackID = i;
			bData.AttackName = "name";
			bData.AttackSlug = "slug";
			bDataList.Add (bData);

		}
		GameInformation.BattleDataList = bDataList;*/


	//	xmlManager.LoadBattleData ();

		//SceneManager.LoadScene ("Loading_Screen");
	}
	
	// Update is called once per frame
	void Update () {

		switch (battleState) {
		case BattleStates.PlayerTurn://player turn
			{
				if (turnState) {
					turnState = false;
					currentPartyTurn = 0;
					if (!interactPanel.activeSelf) {
						interactPanel.SetActive (true);
					}
				}
				break;
			}
		case BattleStates.EnemyTurn://enemy turn get what move the enemy will use
			{
				if (turnState) {
					turnState = false;
					EnemiesTurn ();
				}
				break;
			}
		case BattleStates.Calculate:// calculate see who attacks who first the dmg
			{
				if (turnState) {
					turnState = false;
					CalculateTurn ();
				}
				break;
			}
		case BattleStates.DisplayResult:// display display the result of calculate
			{
				if (turnState) {
					turnState = false;
					DisplayTurn ();
				}
				break;
			}
		}
	}

	private void PlayersTurn()
	{
		if (currentPartyTurn >= turnMoves.Length) {

			battleState = BattleStates.EnemyTurn;
			interactPanel.SetActive (false);
			turnState = true;
		}
	}
	private void EnemiesTurn()
	{
		battleState = BattleStates.Calculate;
		turnState = true;
	}
	private void CalculateTurn()
	{
		UpdateTurnOrderSlots ();
		for (int i = 0; i < turnOrderSlotsString.Length; i++) {
			
			if (turnOrderSlotsString [i] == pTurn) {

				switch (turnMoves [turnOrderSlotsInt [i]]) {
				case TurnMoves.Melee:
					{
						Melee (i, false);
						break;
					}
				case TurnMoves.Ranged:
					{
						

						int attackRoll = 0;
						int diceRoll = 0;

						diceRoll = GetDiceRoll (GameInformation.PlayerStats.CombatSkillDiceValue);

						////Debug.Log (diceRoll.ToString ());

						attackRoll = Mathf.RoundToInt (diceRoll +
						(diceRoll * ((GameInformation.PlayerStats.BaseDexterity / 100) + GetCurrentSkills ())));
						//GameInformation.PlayerStats.Strength

						////Debug.Log (attackRoll.ToString ());

						////Debug.Log (((GameInformation.PlayerStats.BaseStrength / 100) + GetCurrentSkills ()).ToString ());
						////Debug.Log ((diceRoll * ((GameInformation.PlayerStats.BaseStrength / 100) + GetCurrentSkills ())).ToString ());

						//if enemy did a parry or block do the method for that.

						if (attackRoll > EnemyToAttack [turnOrderSlotsInt [i]].dodge) {//instead of player dodge it will be the enemies
							int dmgToGive;

							GetTotalDmg ();

							dmgToGive = GameInformation.PlayerStats.TotalDamage;// dmgtotal - (Armornegation - armorpen)

							dmgToGive -= GetPenDmgReduction (EnemyToAttack [turnOrderSlotsInt [i]].armor);//enemies armor here

							////Debug.Log ("players armor " + GetArmorNegation ().ToString ());

							////Debug.Log ("move hit-----------player");
							////Debug.Log (dmgToGive.ToString ());
							EnemyToAttack [turnOrderSlotsInt [i]].HurtTheEnemy (dmgToGive);

						} else {
							////Debug.Log ("move missed---------------player");
						}
						break;
					}
				case TurnMoves.Block:
					{
						break;
					}
				case TurnMoves.Parry:
					{
						break;
					}
				case TurnMoves.Consumable:
					{
						int currentHtemp, maxHTemp;
						BaseItem tempItem;
						tempItem = turnConsumable [turnOrderSlotsInt [i]];
						currentHtemp = GameInformation.PlayerStats.CurrentHealth;
						maxHTemp = GameInformation.PlayerStats.MaxHealth;

						currentHtemp += tempItem.ItemPotionHpAmount; /*change 5 to the effect of the potion*/

						if (currentHtemp > maxHTemp) {
							GameInformation.PlayerStats.CurrentHealth = maxHTemp;
						} else {
							GameInformation.PlayerStats.CurrentHealth = currentHtemp;
						}

						--tempItem.ItemCurrentAmount;

						for (int x = 0; x < GameInformation.GameStats.EquipmentItems.Length; x++) {
							if (GameInformation.GameStats.EquipmentItems [x] != null) {
								if (GameInformation.GameStats.EquipmentItems [x].ItemID == tempItem.ItemID) {
									////Debug.Log ("loop same item--------------------------------------");
									if (tempItem.ItemCurrentAmount <= 0) {
										for (int y = 0; y < consumablesList.Count; y++) {
											if (consumablesList [y].ItemID == GameInformation.GameStats.EquipmentItems [x].ItemID) {
												consumablesGameObjectList [y].SetActive (false);
											}
										}
										GameInformation.GameStats.EquipmentItems [x] = null;
										turnConsumable [turnOrderSlotsInt [i]] = GameInformation.GameStats.EquipmentItems[x];

									} else {
										for (int y = 0; y < consumablesList.Count; y++) {
											if (consumablesList[y].ItemID == GameInformation.GameStats.EquipmentItems [x].ItemID) {
												////Debug.Log ("loop same item--------------------------------------");
												consumablesGameObjectList [y].GetComponentInChildren<Text> ().text = tempItem.ItemName + " " + tempItem.ItemCurrentAmount.ToString();
											}
										}
										GameInformation.GameStats.EquipmentItems [x].ItemCurrentAmount = tempItem.ItemCurrentAmount;
										turnConsumable [turnOrderSlotsInt [i]] = GameInformation.GameStats.EquipmentItems[x];
									}
								}
							}
						}
						/*
						 * 
						 * consumes the consumable.
						 * add a statement that removes 1 from the potion amount and checks if the amount is now 0 or not
						 * if it is zero remove the slot and item from inv
						 * 
						 * also add a check to see if there is enough potions before the player is
						 * able to select the potions in the menu
						 * 
						 */

						break;
					}
				}
			}

			////Debug.Log (i.ToString ());

			if (turnOrderSlotsString [i] == mTurn) {
				switch (turnMoves[0]) {//switch the 0 out to the player that the mob is attacking
				/*case TurnMoves.Block:
					{
						//Debug.Log ("blocked");

						bool blocked = false;

						int blockRoll = 0;
						int totalBlock = 0;

						blockRoll = GetDiceRoll (GameInformation.PlayerStats.CombatSkillDiceValue);

						totalBlock =  Mathf.RoundToInt (blockRoll + (blockRoll * ((GameInformation.PlayerStats.TotalStrength / 100) +
							(GameInformation.PlayerStats.WeaponSkillValue / 100))));

						blocked = mobStats [turnOrderSlotsInt [i]].Blocking (totalBlock);

						if (!blocked) {//instead of player dodge it will be the enemies
							////Debug.Log ("did not block");
							GameInformation.PlayerStats.CurrentHealth -= mobStats [turnOrderSlotsInt [i]].DealPlayerDmg ();
							if (GameInformation.PlayerStats.CurrentHealth <= 0) {
								PlayerDied ();
							}

						} else
							////Debug.Log ("blocked");

						break;
					}*/

				case TurnMoves.Parry:
					{

						bool parried = false;

						int parriedRoll = 0;
						int totalParry = 0;

						parriedRoll = GetDiceRoll (GameInformation.PlayerStats.CombatSkillDiceValue);

						totalParry =  Mathf.RoundToInt (parriedRoll + (parriedRoll * ((GameInformation.PlayerStats.TotalDexterity / 100) +
							(GameInformation.PlayerStats.WeaponSkillValue / 100))));

						parried = mobStats [turnOrderSlotsInt [i]].Blocking (totalParry);

						if (!parried) {//instead of player dodge it will be the enemies
							////Debug.Log ("did not parry");
							GameInformation.PlayerStats.CurrentHealth -= mobStats [turnOrderSlotsInt [i]].DealPlayerDmg ();
							if (GameInformation.PlayerStats.CurrentHealth <= 0) {
								PlayerDied ();
							}

						} else{
							
							int counterChance = 5;
							int result;
							result = Random.Range (1, 101);

							if (counterChance > result) {
								Melee (i, true);
								////Debug.Log ("countered");

							} else {
								////Debug.Log ("not countered");
							}

							////Debug.Log ("Parried");
						}

						break;
					}

				default:
					{
						GameInformation.PlayerStats.CurrentHealth -= mobStats [turnOrderSlotsInt [i]].CalculatePlayerDmg ();
						if (GameInformation.PlayerStats.CurrentHealth <= 0) {
							PlayerDied ();
						}

						break;
					}
				}
			}
		}
		CheckMobs ();

		////Debug.Log ("calculate");
		battleState = BattleStates.DisplayResult;
		turnState = true;
	}

	private void DisplayTurn()
	{
		////Debug.Log ("display");

		setSlotText ();
		UpdateEnemySlotText ();
		//UpdateEnemySlotText ();

		if (numberOfMobs <= 0) {
			WithdrawFromBattle ();
		}

		battleState = BattleStates.PlayerTurn;
		turnState = true;
	}

	public void UseItem(BaseItem item)
	{
		////Debug.Log ("used item " + item.ItemName.ToString());

		Consumable (item);
	}

	private void AddItemSlots()
	{
		//int temp = 0;
		BaseItem tempItem;
		for (int i = 0; i < GameInformation.GameStats.EquipmentItems.Length; i++) {
			if (GameInformation.GameStats.EquipmentItems [i] != null) {
				if (GameInformation.GameStats.EquipmentItems [i].EquipmentType == BaseItem.EquipmentSlotTypes.Consumable) {
					//instiant this item and add it to the inv slots
					tempItem = GameInformation.GameStats.EquipmentItems [i];

					consumablesList.Add (tempItem);

					tempSlot = (GameObject)Instantiate (inventoryOptionsSlotPrefab);

					tempSlot.transform.SetParent (inventoryOptionsSlots.transform);
					tempSlot.name = tempItem.ItemName + " " + tempItem.ItemCurrentAmount.ToString();//make this equal to party members name
					tempSlot.GetComponentInChildren<Text> ().text = tempSlot.name;
					tempSlot.GetComponent<ListSelectedItem> ().SlotItem = tempItem;
					consumablesGameObjectList.Add (tempSlot);

				}
			}
		}
	}

	private void PlayerDied()//have the party member info or script beimported into this method
	{
		////Debug.Log ("player died");
		LostBattle ();
	}

	public void UpdateTurnOrderSlots()
	{
		int lengthTemp;
		lengthTemp = mobStats.Length + turnMoves.Length;
		int tempTopSpeed;

		bool[] boolMobTemp = new bool[mobStats.Length];
		bool[] boolPartyTemp = new bool[numberOfPartyMembers];

		for (int i = 0; i < boolMobTemp.Length; i++) {
			boolMobTemp [i] = false;
		}

		for (int i = 0; i < boolPartyTemp.Length; i++) {
			boolPartyTemp [i] = false;
		}

		////Debug.Log (lengthTemp.ToString());

		turnOrderSlotsString = new string[lengthTemp];
		turnOrderSlotsInt = new int[lengthTemp];

		for (int i = 0; i < turnOrderSlotsString.Length; i++) {
			tempTopSpeed = -1;

			turnOrderSlotsString [i] = "not set";

			////Debug.Log ("for loop current trn: " + i.ToString () + "\n length " + turnOrderSlotsString.Length.ToString());


			for (int x = 0; x < mobStats.Length; x++) {
				////Debug.Log("mob: " + x.ToString() + "\n " + mobStats[x].speed.ToString());
				if (mobStats [x].speed > tempTopSpeed && boolMobTemp[x] == false) {
					////Debug.Log ("mob stat set");
					tempTopSpeed = mobStats [x].speed;
					turnOrderSlotsString [i] = mTurn;
					turnOrderSlotsInt [i] = x;
					//boolMobTemp [x] = true;
				}
			}
			for (int y = 0; y < numberOfPartyMembers; y++) {//change this to the party array or something
				if ( GameInformation.PlayerStats.TotalSpeed > tempTopSpeed && boolPartyTemp[y] == false) {
					////Debug.Log ("player stat set");
					tempTopSpeed = GameInformation.PlayerStats.TotalSpeed;
					turnOrderSlotsString [i] = pTurn;
					turnOrderSlotsInt [i] = y;
					//boolPartyTemp [y] = true;
				}
			}

			if (turnOrderSlotsString [i] == "m") {
				boolMobTemp [turnOrderSlotsInt[i]] = true;
				mobStats [turnOrderSlotsInt [i]].turnPosID = i;
			} else {
				boolPartyTemp [turnOrderSlotsInt[i]] = true;
			}

		}
		for (int i = 0; i < turnOrderSlotsString.Length; i++) {
			
			////Debug.Log (turnOrderSlotsString [i].ToString ());
		}
	}

	private int GetPenDmgReduction(int armorNegation)
	{
		int temp = 0;

		temp = armorNegation - GetWeaponPen ();
		////Debug.Log ("armor nagation: " + temp.ToString ());
		if (temp < 0) {
			return 0;
		}
		return temp;
	}

	private void GetTotalDmg()
	{
		bool temp = false;

		for (int i = 0; i < GameInformation.GameStats.EquipmentItems.Length; i++) {
			if (GameInformation.GameStats.EquipmentItems [i] != null) {
				if (GameInformation.GameStats.EquipmentItems [i].EquipmentType == BaseItem.EquipmentSlotTypes.Weapon) {
					GameInformation.PlayerStats.BaseDamage = GameInformation.GameStats.EquipmentItems [i].ItemWeaponDamage;
					//GameInformation.GameStats.EquipmentItems [i].ItemWeaponPen;
					temp = true;
					break;
				}
			}
	
		}

		if (!temp) {
			GameInformation.PlayerStats.BaseDamage = 2;
			temp = false;
		}

		GameInformation.PlayerStats.TotalDamage = Mathf.RoundToInt( GameInformation.PlayerStats.BaseDamage
		+ (GameInformation.PlayerStats.BaseDamage * (GameInformation.PlayerStats.BaseStrength / 100)
				+ GetCurrentSkills()));
	}
	private int GetWeaponPen()
	{
		for (int i = 0; i < GameInformation.GameStats.EquipmentItems.Length; i++) {
			if (GameInformation.GameStats.EquipmentItems [i] != null) {
				if (GameInformation.GameStats.EquipmentItems [i].EquipmentType == BaseItem.EquipmentSlotTypes.Weapon) {
					return GameInformation.GameStats.EquipmentItems [i].ItemWeaponPen;
				}
			}

		}
		////Debug.Log ("could not find weapon");
		return 0;
	}

	public int GetArmorNegation()
	{
		int temp = 0;
		for (int i = 0; i < GameInformation.GameStats.EquipmentItems.Length; i++) {
			if (GameInformation.GameStats.EquipmentItems [i] != null) {
				if (GameInformation.GameStats.EquipmentItems [i].ItemArmor > 0) {
					temp += GameInformation.GameStats.EquipmentItems [i].ItemArmor;
				}
			}
		}
		return temp;
	}
		
	public void GetTotalDodge()
	{
		GameInformation.PlayerStats.TotalDodge = Mathf.RoundToInt(GameInformation.PlayerStats.BaseDodge +
			(GameInformation.PlayerStats.BaseDodge * GameInformation.PlayerStats.CombatSkillDodgeValue));
	}

	private float GetCurrentSkills()//will have an input of the current player and weapon
	{
		return GameInformation.PlayerStats.WeaponSkillValue + GameInformation.PlayerStats.CombatSkillDmgValue;
	}

	public void MoveType(string temp)
	{
		tempMove = (TurnMoves)System.Enum.Parse (typeof(BattleManager.TurnMoves), temp.ToString());

		for (int i = 0; i < mobStats.Length; i++) {

			mobStats [i].gameObject.transform.GetChild (0).gameObject.SetActive (true);

		}

	}

	public void EnemySelected(EnemyStats temp)// have an stats id or something that the calculations knows which enemy you are attcking
	{
		EnemyToAttack [currentPartyTurn] = temp;
		switch (tempMove) {
		case TurnMoves.Melee:
			{
				turnMoves [currentPartyTurn] = TurnMoves.Melee;

				currentPartyTurn++;

				PlayersTurn ();
				break;
			}
		case TurnMoves.Ranged:
			{
				Ranged ();
				break;
			}
		}
		for (int i = 0; i < mobStats.Length; i++) {

			mobStats [i].gameObject.transform.GetChild (0).gameObject.SetActive (false);

		}
	}

	public void Melee(int orderCount, bool counterAttack)
	{
		int attackRoll = 0;
		int diceRoll = 0;

		diceRoll = GetDiceRoll (GameInformation.PlayerStats.CombatSkillDiceValue);

		////Debug.Log (diceRoll.ToString ());

		attackRoll = Mathf.RoundToInt (diceRoll +
			(diceRoll * ((GameInformation.PlayerStats.BaseStrength / 100) + GetCurrentSkills ())));
		//GameInformation.PlayerStats.Strength

		////Debug.Log (attackRoll.ToString ());

		////Debug.Log (((GameInformation.PlayerStats.BaseStrength / 100) + GetCurrentSkills ()).ToString ());
		////Debug.Log ((diceRoll * ((GameInformation.PlayerStats.BaseStrength / 100) + GetCurrentSkills ())).ToString ());
			
		//if enemy did a parry or block do the method for that.

		if (counterAttack) {
			if (attackRoll > mobStats [turnOrderSlotsInt [orderCount]].dodge) {
				int dmgToGive;

				GetTotalDmg ();

				dmgToGive = GameInformation.PlayerStats.TotalDamage;// dmgtotal - (Armornegation - armorpen)

				dmgToGive -= GetPenDmgReduction (mobStats [turnOrderSlotsInt [orderCount]].armor);//enemies armor here
				////Debug.Log ("players armor " + GetArmorNegation ().ToString ());

				////Debug.Log ("move hit-----------player");
				////Debug.Log (dmgToGive.ToString ());
				mobStats [turnOrderSlotsInt [orderCount]].HurtTheEnemy (dmgToGive);//
			} else {
				////Debug.Log ("move missed---------------player");
			}
		} else {
			if (attackRoll > EnemyToAttack [turnOrderSlotsInt [orderCount]].dodge) {//instead of player dodge it will be the enemies
				int dmgToGive;

				GetTotalDmg ();

				dmgToGive = GameInformation.PlayerStats.TotalDamage;// dmgtotal - (Armornegation - armorpen)

				dmgToGive -= GetPenDmgReduction (EnemyToAttack [turnOrderSlotsInt [orderCount]].armor);//enemies armor here
				////Debug.Log ("players armor " + GetArmorNegation ().ToString ());

				////Debug.Log ("move hit-----------player");
				////Debug.Log (dmgToGive.ToString ());
				EnemyToAttack [turnOrderSlotsInt [orderCount]].HurtTheEnemy (dmgToGive);//
			} else {
				////Debug.Log ("move missed---------------player");
			}
		}
	}

	public void Ranged()
	{
		turnMoves [currentPartyTurn] = TurnMoves.Ranged;

		currentPartyTurn++;

		////Debug.Log ("Ranged: turn " + currentPartyTurn.ToString());
		PlayersTurn();
	}

	public void Block()
	{
		turnMoves [currentPartyTurn] = TurnMoves.Block;

		currentPartyTurn++;

		////Debug.Log ("Block: turn " + currentPartyTurn.ToString());
		PlayersTurn();
	}

	public void Parry()
	{
		turnMoves [currentPartyTurn] = TurnMoves.Parry;

		currentPartyTurn++;

		////Debug.Log ("Parry: turn " + currentPartyTurn.ToString());
		PlayersTurn();
	}

	public void Consumable(BaseItem baseItem)
	{
		turnMoves [currentPartyTurn] = TurnMoves.Consumable;
		turnConsumable [currentPartyTurn] = baseItem;

		currentPartyTurn++;

		////Debug.Log ("Consumable: turn " + currentPartyTurn.ToString());
		PlayersTurn();
	}

	public void WithdrawFromBattle ()
	{
		GameInformation.fromBattle = true;
		SceneManager.LoadScene ("Loading_Screen");
	}
	public void LostBattle ()
	{
		GameInformation.fromBattle = true;
		GameInformation.lostBattle = true;
		GameInformation.GameStats.MapSlug = GameInformation.GameStats.RespawnMapSlug;

		for (int i = 0; i < numberOfPartyMembers; i++) {//gameinfo.gamestats.partynumber
			GameInformation.PlayerStats.CurrentHealth = GameInformation.PlayerStats.MaxHealth;
			GameInformation.PlayerStats.CurrentStamina = GameInformation.PlayerStats.MaxStamina;
			GameInformation.PlayerStats.CurrentFatigue = GameInformation.PlayerStats.MaxFatigue;
		}
		SceneManager.LoadScene ("Loading_Screen");
	}
	public void AttackOptionsPanel()
	{
		topOptionsPanel.SetActive (false);
		attackOptionsPanel.SetActive (true);
	}
	public void AbilityOptionsPanel()
	{
		topOptionsPanel.SetActive (false);
		abilityOptionsPanel.SetActive (true);
	}
	public void InventoryOptionsPanel()
	{
		topOptionsPanel.SetActive (false);
		inventoryOptionsPanel.SetActive (true);
	}
	public void backToTopOptionsPanel()
	{

		for (int i = 0; i < mobStats.Length; i++) {
			mobStats [i].gameObject.transform.GetChild (0).gameObject.SetActive (false);
		}

		topOptionsPanel.SetActive (true);
		attackOptionsPanel.SetActive (false);
		abilityOptionsPanel.SetActive (false);
		inventoryOptionsPanel.SetActive (false);
		//add the rest of the panels
	}

	private void AddEnemySlots()
	{

		for (int i = 0; i < numberOfMobs; i++) {

			////Debug.Log ("added enemy");
			tempSlot = (GameObject)Instantiate (enemyPrefab);

			mobStats [i] = tempSlot.GetComponent<EnemyStats> ();

			tempSlot.transform.SetParent (enemySlots[i].transform);
			tempSlot.transform.position = tempSlot.transform.parent.transform.position;
			tempSlot.name = mobStats [i].enemyName;
			mobSlotsList.Add (tempSlot);

			slotText = tempSlot.GetComponentInChildren<Text> ();

			//setEnemySlotText (mobStats [i]);
		}

		/*for (int i = 0; i < numberOfMobs; i++) {
			tempSlot = (GameObject)Instantiate (enemySlotPrefab);

			//set the id/name/stats etc here
			mobStats[i] = tempSlot.GetComponent<EnemyStats>();

			tempSlot.transform.SetParent (enemyPanel.transform);
			tempSlot.name = mobStats [i].enemyName;
			mobSlotsList.Add (tempSlot);

			slotText = tempSlot.GetComponentInChildren<Text> ();

			setEnemySlotText (mobStats [i]);
		}*/
	}

	public void CheckMobs()//
	{
		List<EnemyStats> temp = new List<EnemyStats>();
		bool tempBool = false;

		for (int i = 0; i < mobStats.Length; i++) {
			if (mobStats [i].health <= 0) {
				tempBool = true;
			} else {
				temp.Add (mobStats [i]);
			}
		}

		if (tempBool) {

			mobStats = temp.ToArray ();

			tempBool = false;
		}
	}

	public void GiveXpToParty (int temp){
		int xpToGive;

		xpToGive = temp / turnMoves.Length;

		for (int i = 0; i < turnMoves.Length; i++) {
			GameInformation.PlayerStats.PlayerXP += xpToGive;
			GameInformation.PlayerStats.CombatSkillExp += xpToGive;
			GameInformation.PlayerStats.WeaponSkillExp += xpToGive;
		}
	}

	private void AddBattleStatsSlots()
	{
		for (int i = 0; i < numberOfPartyMembers; i++) {
			tempSlot = (GameObject)Instantiate (battleStatSlotPrefab);

			tempSlot.transform.SetParent (statsPanel.transform);
			tempSlot.name = "Empty";//make this equal to party members name
			partySlotsList.Add (tempSlot);
			slotText = tempSlot.GetComponentInChildren<Text> ();

			setSlotText ();
		}
	}

	private void AddBattleAttackSlots()
	{
		for (int i = 0; i < attackIntID.Length; i++) {
			tempSlot = (GameObject)Instantiate (attackOptionsSlotPrefab);

			BattleData bData = GameInformation.BattleDataList.ToArray()[i];

			tempSlot.transform.SetParent (attackOptionsSlots.transform);
			tempSlot.name = bData.AttackName;//make this equal to party members name
			tempSlot.GetComponentInChildren<Text>().text = tempSlot.name;
			tempSlot.GetComponent<AttackMove> ().attackMoveID = bData.AttackID;
			AttackSlotsList.Add (tempSlot);
			//attackOptionsSlots = tempSlot.GetComponentInChildren<Text> ();
		}
	}

	private void setSlotText()
	{

		foreach (var item in partySlotsList) {
				slotText = item.GetComponentInChildren<Text> ();

			slotText.text = "Player Lvl: " + GameInformation.PlayerStats.PlayerLevel +
				"\nHP: " + GameInformation.PlayerStats.CurrentHealth + "/" + GameInformation.PlayerStats.MaxHealth +
				"\nSP: " + GameInformation.PlayerStats.CurrentStamina + "/" + GameInformation.PlayerStats.MaxStamina +
				"\nFP: " + GameInformation.PlayerStats.CurrentFatigue + "/" + GameInformation.PlayerStats.MaxFatigue;
		}
	}
	private void setEnemySlotText(EnemyStats temp)
	{
		slotText.text = temp.enemyName.ToString() + " Lvl: " + temp.lvl.ToString() + "\nHP: " + temp.health.ToString();
	}

	private void UpdateEnemySlotText()
	{
		for (int i = 0; i < mobStats.Length; i++) {

			/*mobStats[i].gameObject.GetComponentInChildren<Text> ().text = mobStats[i].enemyName.ToString () +
				" Lvl: " + mobStats[i].lvl.ToString () + "\nHP: " + mobStats[i].health.ToString ();*/

			mobStats [i].GetComponentInChildren<HealthBar> ().UpdateHealthBar (mobStats [i].health, mobStats [i].maxHealth);
		}
	}

	public int GetDiceRoll(int numberOfDice)
	{
		int diceRoll = 0;
		for (int i = 0; i < numberOfDice; i++) {
			diceRoll += randomNumber ();
		}
		return diceRoll;

	}

	private int randomNumber()
	{
		return Random.Range (1, 21);
	}
}
