using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCharacterRace {

	private string characterRaceName;
	private string characterRaceDescription;
	private string characterRaceSlug;
	//stats
	//stats editable by the player
	public int BaseStrength{get; set;}
	public int BaseWisdom{get; set;}
	public int BaseDexterity{get; set;}

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
	public int BaseDamage {get; set;}
	public float CurrentWeight{get; set;}
	public float MaxWeight{get; set;}
	public int BaseSpeed{get; set;}
	public int BaseDodge{get; set;}
	public int MaxFatigue{get; set;}
	public int MaxStamina{get; set;}

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
