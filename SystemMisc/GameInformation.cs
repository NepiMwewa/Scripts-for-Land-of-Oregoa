using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour {


	//[Range(1,5)] use that to make an int not beable to go above or below a certain amount

	public static bool loadSave, startOfScene, saveFound, fromBattle, lostBattle, shopOpen;

	public static string mobSlugInBattle;
	public static int numberOfMobs;

	public static int PlayerGoldAmount;

	public static List<BaseItem> ItemDatabase{ get; set; }

	public static BasePlayerStats PlayerStats{ get; set; }//stores player stat values and character apperence

	public static List<BasePlayerStats> PartyStatsList{ get; set; }

	public static GameInformationStats GameStats{ get; set; }// player location values, quests, etc ~along with player inventory.~

	public static List<NpcStats> NpcStatsList{ get; set;}//stores npc location and health values. 

	public static List<MobStats> MobStatsList{ get; set; }//stores mob location,health values. 

	public static List<BattleData> BattleDataList{ get; set; }

	public static float AudioMaster{ get; set; }
	public static float AudioSfx{ get; set; }
	public static float AudioMusic{ get; set; }
	public static float AudioVocal{ get; set; }

}
