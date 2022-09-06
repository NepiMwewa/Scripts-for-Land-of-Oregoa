using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePP {

	public static void SaveAllInformation(){

		//PPSerialization.Save ("_player_stats", GameInformation.PlayerStats); 


		PlayerPrefs.SetFloat ("audio_Master", GameInformation.AudioMaster);
		PlayerPrefs.SetFloat("audio_Sfx", GameInformation.AudioSfx);
		PlayerPrefs.SetFloat("audio_Music", GameInformation.AudioMusic);
		PlayerPrefs.SetFloat("audio_Vocal", GameInformation.AudioVocal);

		//if (GameInformation.PlayerRace != null) {
			
		/*PPSerialization.Save ("HEALTH", GameInformation.Health);
		PPSerialization.Save ("PLAYERRACE", GameInformation.PlayerRace); 
		PPSerialization.Save ("MOTHERSNAME", GameInformation.MothersName);
		PPSerialization.Save ("FATHERSNAME", GameInformation.FathersName);*/

		//

	}

}
