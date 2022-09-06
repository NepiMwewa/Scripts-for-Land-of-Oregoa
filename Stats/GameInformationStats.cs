using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameInformationStats{


	//public string FileName{ get; set; }
	public Vector3 CameraPosition{ get; set; }
	public Vector3 PlayerPosition{ get; set; }
	public Vector2 PlayerLastMove { get; set; }
	public string MapSlug{ get; set; }
	public bool HadHelp{ get; set; }
	//players party
	public int PartyNumber{ get; set; }

	/*
	 * Battle stats
	 */
	public string RespawnPoint{ get; set; }
	public string RespawnMapSlug{ get; set; }

	//inventory
	/*
	 * make it so that only the id gets stored, unless we add weapon breaking and such then leave it as is
	 */
	public List<BaseItem> PlayerInventory{ get; set; }
	public int PlayerGoldAmount{ get; set; }
	public BaseItem[] EquipmentItems{ get; set; }

	//chest invs
	public List<int> ChestsID{ get; set;}//set all to true at start.
	//once someone opens a chest it gets a random number of items and then its set to false
	public List<List<int>> ChestItemIDs{ get; set; }// set this to the item ids inside of the chest.
	public List<List<int>> ChestItemAmounts{get; set;}


	//quests
	public bool[] QuestCompleted{ get; set; }
	public bool[] QuestActive{ get; set; }
	public int[] QuestEnemyKillCount{ get; set; }
	public bool[] QuestPlayerHasItem{ get; set; }

}
