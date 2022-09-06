using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateNewCharacter : MonoBehaviour {

	private BasePlayerStats newPlayer;

	[SerializeField] private GameObject firstUIObj,secondUIObj,thirdUIObj;
	[SerializeField] private InputField mothersInputField;
	[SerializeField] private InputField fathersInputField;
	[SerializeField] private InputField playerInputField;
	[SerializeField] private XMLManager xmlManager;
	[SerializeField] private Animator anim;

	private bool isDwarvenRace, isElvenRace, isGoblinRace,
		isHumanRace, isMermanRace, isOrcishRace, isDarkElvenRace;
	private int whichRaceInt = 0;//elven = 0, halfling = 1, human = 2,
	//darkElven = 3, darkHalfling = 4, darkHuman = 5
	private int whichEyeColorInt = 0; //brown = 0, blue = 1, green = 2
	private string whichEyeColorString;
	private int whichHairStyleInt = 0;//spikey = 0, longStraight = 1,
	//fohawk = 2, longBun = 3,messy = 4, plain = 5, shortBun = 6, slick = 7
	private string whichHairStyleString;
	private int whichHairColorInt = 0;//grey = 0, blonde = 1, dirtyBlonde = 2,
	// brown = 3, darkBrown = 4, black = 5, lightRed = 6, red = 7,
	//lightBlue = 8, blue = 9, lightGreen = 10, green = 11
	private string whichHairColorString;
	private int UIPosition = 0;

	private string mothersName = "";
	private string fathersName = "";
	private string playerName = "";

	public string levelToLoad;

	public string exitPoint;

	private PlayerController thePlayer;

	void Start(){
		newPlayer = new BasePlayerStats ();
		//xmlManager.LoadPlayer ();
		////Debug.Log (GameInformation.PlayerStats.PlayerName);
		////Debug.Log (GameInformation.PlayerStats.MaxHealth.ToString ());

	}
	// Update is called once per frame
	void Update () {
		
	}

	public void RaceDropdown(Dropdown objDropdown)
	{
		whichRaceInt = objDropdown.value;
		anim.SetInteger ("RaceId", whichRaceInt);
	}

	public void EyeColorDropdown(Dropdown objDropdown)
	{
		whichEyeColorInt = objDropdown.value;
		whichEyeColorString = objDropdown.options[whichEyeColorInt].text;
		anim.SetInteger ("EyeColorId", whichEyeColorInt);
	}

	public void HairColorDropdown(Dropdown objDropdown)
	{
		whichHairColorInt = objDropdown.value;
		whichHairColorString = objDropdown.options[whichHairColorInt].text;
		anim.SetInteger ("HairColorId", whichHairColorInt);
	}
	public void HairStyleDropdown(Dropdown objDropdown)
	{
		whichHairStyleInt = objDropdown.value;
		whichHairStyleString = objDropdown.options[whichHairStyleInt].text;
		anim.SetInteger ("HairId", whichHairStyleInt);
	}

	public void GenderOption(string gender)
	{
		newPlayer.PlayerGender = gender;
	}



	private void firstUI()
	{
		switch (whichRaceInt) {
		case 0:
			{
				SetNewPlayerInfo( new BaseDwarvenRace ());
				break;
			}
		case 1:
			{
				SetNewPlayerInfo(new BaseElvenRace ());
				break;
			}
		case 2:
			{
				SetNewPlayerInfo(new BaseGoblinRace ());
			break;
			}
		case 3:
			{
				SetNewPlayerInfo( new BaseHumanRace ());
				break;
			}
		case 4:
			{
				SetNewPlayerInfo( new BaseMermanRace ());
				break;
			}
		case 5:
			{
				SetNewPlayerInfo(new BaseOrcishRace ());
				break;
			}
		case 6:
			{
				SetNewPlayerInfo( new BaseDarkElvenRace ());
				break;
			}
		}

		mothersName = mothersInputField.text.ToString();
		fathersName = fathersInputField.text.ToString();

		if (mothersName.Length > 1 && fathersName.Length > 1) {
			
			//save information
			newPlayer.PlayerRaceId = whichRaceInt;
			newPlayer.MothersName = mothersName;
			newPlayer.FathersName = fathersName;
			StoreNewPlayerInfo ();
			//Debug.Log ("went Through");
			//Debug.Log (newPlayer.CharacterRaceName);
			//Debug.Log (newPlayer.CharacterRaceDescription);
			//Debug.Log (newPlayer.CharacterRaceSlug);
			//Debug.Log (newPlayer.MaxHealth.ToString ());
			//Debug.Log (newPlayer.MothersName.ToString() + " " + newPlayer.FathersName.ToString());
			firstUIObj.SetActive (false);
			UIPosition++;
			secondUIObj.SetActive (true);
		}

	}
	private void secondUI()
	{
		newPlayer.PlayerEyeId = whichEyeColorInt;
		newPlayer.PlayerEyeColor = whichEyeColorString;
			//save information
		//Debug.Log(newPlayer.PlayerEyeColor);
		//Debug.Log (newPlayer.PlayerGender);
			secondUIObj.SetActive (false);
			UIPosition++;
			thirdUIObj.SetActive (true);
	}
	private void thirdUI()
	{

		playerName = playerInputField.text;
		if (playerName.Length > 0) {
			//save infomation
			newPlayer.PlayerHairId = whichHairStyleInt;
			newPlayer.PlayerHairColorId = whichHairColorInt;
			newPlayer.PlayerHairColor = whichHairColorString;
			newPlayer.PlayerName = playerName;

			//Debug.Log(newPlayer.PlayerHairColor);
			//Debug.Log(newPlayer.PlayerName);
			thirdUIObj.SetActive (false);
			UIPosition++;
			NextScreen ();
		}

	}

	public void NextScreen()
	{
		switch(UIPosition)
		{
		case 0:
			{
				firstUI ();
				break;
			}
		case 1:
			{
				secondUI ();
				break;
			}
		case 2:
			{
				thirdUI ();
				break;
			}
		case 3:
			{
				StartGame (); 
				break;
			}
		}
	}

	private void StartGame()
	{
		//Debug.Log ("________");

		StoreNewPlayerInfo ();

		//Debug.Log (GameInformation.PlayerStats.PlayerName);
		//Debug.Log (GameInformation.PlayerStats.CharacterRaceName);
		//Debug.Log (GameInformation.PlayerStats.MaxHealth.ToString());
		//Debug.Log (GameInformation.PlayerStats.FathersName);
		//Debug.Log (GameInformation.PlayerStats.PlayerHairColor);
		//Debug.Log (GameInformation.PlayerStats.PlayerEyeColor);
		//SaveInformation.SaveAllInformation ();
		//xmlManager.SavePlayer();

		SceneManager.LoadScene (levelToLoad);
		//thePlayer.startPoint = exitPoint;
	}
		

	/*void OnGUI()
	{
		playerName = GUILayout.TextField (playerName,18);
		isDwarvenRace = GUILayout.Toggle(isDwarvenRace, "Dwarven Race");
		isElvenRace = GUILayout.Toggle(isElvenRace, "Elven Race");
		isGoblinRace = GUILayout.Toggle(isGoblinRace, "Goblin Race");
		isHumanRace = GUILayout.Toggle(isHumanRace, "Human Race");
		isMermanRace = GUILayout.Toggle(isMermanRace, "Merman Race");
		isOrcishRace = GUILayout.Toggle(isOrcishRace, "Orcish Race");
		isDarkElvenRace = GUILayout.Toggle(isDarkElvenRace, "DarkElven Race");

		if(GUILayout.Button("Create"))
		{
					
			if(isDwarvenRace){
			newPlayer.PlayerRace = new BaseDwarvenRace ();
			}
			if(isElvenRace){
				newPlayer.PlayerRace = new BaseElvenRace ();
			}
			if(isGoblinRace){
			newPlayer.PlayerRace = new BaseGoblinRace ();
			}
			if(isHumanRace){
				newPlayer.PlayerRace = new BaseHumanRace ();
			}
			if(isMermanRace){
				newPlayer.PlayerRace = new BaseMermanRace ();
			}
			if(isOrcishRace){
				newPlayer.PlayerRace = new BaseOrcishRace ();
			}
			if(isDarkElvenRace){
				newPlayer.PlayerRace = new BaseDarkElvenRace ();
			}

			newPlayer.PlayerLevel = 1;
			newPlayer.PlayerXP = 0;
			newPlayer.PlayerName = playerName;

			StoreNewPlayerInfo ();

			SaveInformation.SaveAllInformation ();

			//Debug.Log("Player Name: " + newPlayer.PlayerName);
			//Debug.Log("Player Race: " + newPlayer.PlayerRace.CharacterRaceName);
			//Debug.Log("Player Race Slug: " + newPlayer.PlayerRace.CharacterRaceSlug);
			//Debug.Log (newPlayer.PlayerRace.Stamina.ToString ());
		}
		if (GUILayout.Button ("Load")) {
			LoadInformation.LoadAllInformation ();
			//Debug.Log (GameInformation.PlayerName.ToString());
			//Debug.Log (GameInformation.CharacterRaceName.ToString());
			//Debug.Log (GameInformation.Health.ToString ());
			//Debug.Log(GameInformation.Health.ToString());
		}
	}*/

	private void SetNewPlayerInfo(BaseCharacterRace characterRace){

		newPlayer.PlayerLevel = 1;
		newPlayer.PlayerXP = 0;
		newPlayer.XPToLevelUp = 15;
		newPlayer.CharacterRaceName = characterRace.CharacterRaceName;
		newPlayer.CharacterRaceDescription = characterRace.CharacterRaceDescription;
		newPlayer.CharacterRaceSlug = characterRace.CharacterRaceSlug;
		//stats
		newPlayer.BaseStrength = characterRace.BaseStrength;
		newPlayer.TotalStrength = newPlayer.BaseStrength;
		newPlayer.BaseWisdom = characterRace.BaseWisdom;
		newPlayer.TotalWisdom = newPlayer.BaseWisdom;
		newPlayer.BaseDexterity = characterRace.BaseDexterity;
		newPlayer.TotalDexterity = newPlayer.BaseDexterity;
		newPlayer.MaxHealth = characterRace.MaxHealth;
		newPlayer.CurrentHealth = characterRace.MaxHealth;
		newPlayer.MaxWeight = characterRace.MaxWeight;
		newPlayer.CurrentWeight = characterRace.CurrentWeight;
		newPlayer.CombatSkillExp = characterRace.CombatSkillExp;
		newPlayer.CombatSkillLvl = characterRace.CombatSkillLvl;
		newPlayer.CombatSkillDmgValue = characterRace.CombatSkillDmgValue;
		newPlayer.CombatSkillDodgeValue = characterRace.CombatSkillDodgeValue;
		newPlayer.CombatSkillDiceValue = characterRace.CombatSkillDiceValue;
		//weapon skill
		newPlayer.WeaponSkillExp = characterRace.WeaponSkillExp;
		newPlayer.WeaponSkillLvl = characterRace.WeaponSkillLvl;
		newPlayer.WeaponSkillValue = characterRace.WeaponSkillValue;
		//combat stats
		newPlayer.MagicSkillsExp = characterRace.MagicSkillsExp;
		newPlayer.MagicSkillValue = characterRace.MagicSkillValue;
		newPlayer.MagicSkillsLvl = characterRace.MagicSkillsLvl;

		newPlayer.BaseDamage = characterRace.BaseDamage;
		newPlayer.TotalDamage = newPlayer.BaseDamage;
		newPlayer.BaseSpeed = characterRace.BaseSpeed;
		newPlayer.TotalSpeed = newPlayer.BaseSpeed;
		newPlayer.BaseDodge = characterRace.BaseDodge;
		newPlayer.TotalDodge = newPlayer.BaseDodge;
		newPlayer.CurrentFatigue = characterRace.MaxFatigue;
		newPlayer.MaxFatigue = characterRace.MaxFatigue;
		newPlayer.CurrentStamina = characterRace.MaxStamina;
		newPlayer.MaxStamina = characterRace.MaxStamina;
	}

	private void StoreNewPlayerInfo(){

		GameInformation.PlayerStats = newPlayer;

		/*
		//GameInformation.PlayerName = newPlayer.PlayerName;
		GameInformation.PlayerLevel = newPlayer.PlayerLevel;
		GameInformation.PlayerXp = newPlayer.PlayerXP;

		GameInformation.CharacterRaceName = newPlayer.CharacterRaceName;
		GameInformation.CharacterRaceDescription = newPlayer.CharacterRaceDescription;
		GameInformation.CharacterRaceSlug = newPlayer.CharacterRaceSlug;
		//stats
		GameInformation.Strength = newPlayer.Strength;
		GameInformation.Agility = newPlayer.Agility;
		GameInformation.Wisdom = newPlayer.Wisdom;
		GameInformation.Endurance = newPlayer.Endurance;
		GameInformation.Health = newPlayer.Health;
		GameInformation.Weight = newPlayer.Weight;
		//combat stats
		GameInformation.WeaponSkills = newPlayer.WeaponSkills;
		GameInformation.MagicSkills = newPlayer.MagicSkills;
		GameInformation.Speed = newPlayer.Speed;
		GameInformation.HitRate = newPlayer.HitRate;
		GameInformation.Dodge = newPlayer.Dodge;
		GameInformation.Parry = newPlayer.Parry;
		GameInformation.Fatigue = newPlayer.Fatigue;
		GameInformation.Stamina = newPlayer.Stamina;

		GameInformation.MothersName = mothersName;
		GameInformation.FathersName = fathersName;*/

	}
}
