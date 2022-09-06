using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPP {

	public static void LoadAllInformation(){

		//GameInformation.PlayerStats = (BasePlayerStats)PPSerialization.Load ("_player_stats");

		GameInformation.AudioMaster = PlayerPrefs.GetFloat("audio_Master");
		GameInformation.AudioSfx = PlayerPrefs.GetFloat("audio_Sfx");
		GameInformation.AudioMusic = PlayerPrefs.GetFloat("audio_Music");
		GameInformation.AudioVocal = PlayerPrefs.GetFloat("audio_Vocal");

		//if (PlayerPrefs.GetString ("PLAYERRACE") != null) {

		/*GameInformation.Health = (int) PPSerialization.Load ("HEALTH");
		GameInformation.PlayerRace = (BaseCharacterRace)PPSerialization.Load ("PLAYERRACE");

		GameInformation.MothersName = (string)PPSerialization.Load ("MOTHERSNAME");
		GameInformation.FathersName = (string)PPSerialization.Load ("FATHERSNAME");*/

		//Debug.Log ("loaded");

	}

}
