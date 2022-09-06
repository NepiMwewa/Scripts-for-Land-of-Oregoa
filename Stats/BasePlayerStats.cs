using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePlayerStats {

	//apperence
	private int playerRaceId;
	private int playerEyeId;
	private string playerEyeColor;
	private int playerHairId;
	private int playerHairColorId;
	private string playerHairColor;
	private string playerGender;
	//
	private string playerName;
	private string mothersName;
	private string fathersName;
	private int playerLevel;
	private int playerXP;
	private int xpToLevelUp;

	private string characterRaceName;
	private string characterRaceDescription;
	private string characterRaceSlug;
	//stats editable by the player
	public int BaseStrength{get; set;}
	public int TotalStrength{get; set;}
	public int BaseWisdom{get; set;}
	public int TotalWisdom{get; set;}
	public int BaseDexterity{get; set;}
	public int TotalDexterity{get; set;}

	//combat skills
	//combat experience
	public int CombatSkillExp{get; set;}
	public float CombatSkillDmgValue{get; set;}// stores a dmg percent that goes ontop of the base dmg
	public float CombatSkillDodgeValue{get; set;}//stores dodge percent that goes ontop of base dodge value
	public int CombatSkillDiceValue{get; set;}//how many dice are thrown
	public int CombatSkillLvl{get; set;}// what lvl it currently is at
	//WeaponSkill
	public int WeaponSkillExp{get; set;}
	public float WeaponSkillValue{get; set;}// stores a dmg percent that goes ontop of the base dmg 
	public int WeaponSkillLvl{get; set;}// what lvl it currently is at

	//magic skill
	public int MagicSkillsExp{get; set;}
	public float MagicSkillValue{get; set;}
	public int MagicSkillsLvl{get; set;}


	//stats stats
	public int MaxHealth{get; set;}
	public int CurrentHealth{get; set;}
	public int BaseDamage{get; set;}
	public int TotalDamage{get; set;}
	public int BasePen{get; set;}
	public int TotalPen{get; set;}
	public int BaseArmor{ get; set; }
	public int TotalArmor{ get; set; }
	public float CurrentWeight{get; set;}
	public float MaxWeight{get; set;}
	public int BaseSpeed{get; set;}
	public int TotalSpeed{ get; set; }
	public int BaseDodge{get; set;}
	public int TotalDodge{get; set;}
	public int CurrentFatigue{get; set;}
	public int MaxFatigue{get; set;}
	public int CurrentStamina{get; set;}
	public int MaxStamina{get; set;}

	//add all the stats from base character race
	public int PlayerRaceId
	{
		get{ return playerRaceId; }
		set{ playerRaceId = value; }
	}

	public int PlayerEyeId
	{
		get{ return playerEyeId; }
		set{ playerEyeId = value; }
	}
	public string PlayerEyeColor
	{
		get{ return playerEyeColor; }
		set{ playerEyeColor = value; }
	}

	public int PlayerHairId
	{
		get{ return playerHairId; }
		set{ playerHairId = value; }
	}
	public int PlayerHairColorId
	{
		get{ return playerHairColorId; }
		set{ playerHairColorId = value; }
	}

	public string PlayerHairColor
	{
		get{ return playerHairColor; }
		set{ playerHairColor = value; }
	}

	public string PlayerGender
	{
		get{ return playerGender; }
		set{ playerGender = value; }
	}

	public string PlayerName
	{
		get{ return playerName; }
		set{ playerName = value; }
	}

	public string MothersName
	{
		get{ return mothersName; }
		set{ mothersName = value; }
	}

	public string FathersName
	{
		get{ return fathersName; }
		set{ fathersName = value; }
	}


	public int PlayerLevel
	{
		get{ return playerLevel; }
		set{ playerLevel = value; }
	}

	public int PlayerXP
	{
		get{ return playerXP; }
		set{ playerXP = value; }
	}

	public int XPToLevelUp
	{
		get{ return xpToLevelUp; }
		set{ xpToLevelUp = value; }
	}

	public string CharacterRaceName{
		get{ return characterRaceName; }
		set{ characterRaceName = value; }
	}

	public string CharacterRaceDescription{
		get{ return characterRaceDescription; }
		set{ characterRaceDescription = value; }
	}

	public string CharacterRaceSlug{
		get{ return characterRaceSlug; }
		set{ characterRaceSlug = value; }
	}
}
