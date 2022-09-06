using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseDwarvenRace : BaseCharacterRace {

	public BaseDwarvenRace(){

		CharacterRaceName = "Dwarven";
		CharacterRaceDescription = "im a dwarf and i dig a hole";
		CharacterRaceSlug = "_dwarven";//no caps and use _ for spaces
		MaxHealth = 10;
		BaseStrength = 5;
		BaseWisdom = 5;
		BaseDexterity = 3;

		MaxStamina = 5;
		MaxFatigue = 10;
		CurrentWeight = 0;
		MaxWeight = 10;
		//combat skill
		CombatSkillExp = 0;
		CombatSkillLvl = 0;
		CombatSkillDmgValue = 0;
		CombatSkillDodgeValue = 0;
		CombatSkillDiceValue = 1;
		//weapon skill
		WeaponSkillExp = 0;
		WeaponSkillLvl = 0;
		WeaponSkillValue = 0;

		MagicSkillsExp = 0;
		MagicSkillValue = 0;
		MagicSkillsLvl = 0;

		BaseSpeed = 2;
		BaseDodge = 10;
		BaseDamage = 1;
	
	}
}
