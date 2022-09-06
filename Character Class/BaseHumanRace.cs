using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseHumanRace : BaseCharacterRace {

	public BaseHumanRace(){

		CharacterRaceName = "Human";
		CharacterRaceDescription = "";
		CharacterRaceSlug = "_human";
		MaxHealth = 10;
		BaseStrength = 5;
		BaseWisdom = 0;
		BaseDexterity = 0;

		MaxStamina = 0;
		MaxFatigue = 0;
		CurrentWeight = 0;
		MaxWeight = 0;
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

		BaseSpeed = 0;
		BaseDodge = 0;
		BaseDamage = 0;

	}
}
