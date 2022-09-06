using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XMLManager : MonoBehaviour {

	public static XMLManager ins;

	void Awake(){
		ins = this;
	}

	public void SavePlayer(){

		XmlSerializer serializer = new XmlSerializer(typeof(BasePlayerStats));
		string path = Application.dataPath + "/StreamingAssets/DataFiles/player_Data.xml";

		StreamWriter stream = new StreamWriter (path, false);

		serializer.Serialize (stream, GameInformation.PlayerStats);


		stream.Close ();
		//Debug.Log ("saved");

	}

	public void LoadPlayer(){

		string path = Application.dataPath + "/StreamingAssets/DataFiles/player_Data.xml";
		XmlSerializer serializer = new XmlSerializer(typeof(BasePlayerStats));

		if (File.Exists (path)) {
			StreamReader stream = new StreamReader (path, true);
			GameInformation.PlayerStats = serializer.Deserialize (stream) as BasePlayerStats;
			stream.Close ();
			//Debug.Log ("loaded");
		} else {
			//Debug.Log ("no save found");
		}
	}

	public void SaveGame(){

		XmlSerializer serializer = new XmlSerializer(typeof(GameInformationStats));
		string path = Application.dataPath + "/StreamingAssets/DataFiles/game_Data.xml";

		StreamWriter stream = new StreamWriter (path, false);

		serializer.Serialize (stream, GameInformation.GameStats);


		stream.Close ();
		//Debug.Log ("saved");

	}

	public void LoadGame(){

		string path = Application.dataPath + "/StreamingAssets/DataFiles/game_Data.xml";
		XmlSerializer serializer = new XmlSerializer(typeof(GameInformationStats));

		if (File.Exists (path)) {
			StreamReader stream = new StreamReader (path, true);
			GameInformation.GameStats = serializer.Deserialize (stream) as GameInformationStats;
			stream.Close ();
			//Debug.Log ("loaded");
			GameInformation.saveFound = true;
		} else {
			//Debug.Log ("no save found");
			GameInformation.saveFound = false;
		}
	}

	public void SaveNpc(){

		XmlSerializer serializer = new XmlSerializer(typeof(List<NpcStats>));
		string path = Application.dataPath + "/StreamingAssets/DataFiles/npc_Data.xml";
		if(File.Exists(path)){
			File.Delete(path);
		}

		StreamWriter stream = new StreamWriter (path, false);



		serializer.Serialize (stream, GameInformation.NpcStatsList);


		stream.Close ();
		//Debug.Log ("saved");

	}

	public void LoadNpc(){


		string path = Application.dataPath + "/StreamingAssets/DataFiles/npc_Data.xml";
		XmlSerializer serializer = new XmlSerializer(typeof(List<NpcStats>));

		if (File.Exists (path)) {
			StreamReader stream = new StreamReader (path, true);
			GameInformation.NpcStatsList = serializer.Deserialize (stream) as List<NpcStats>;
			stream.Close ();
			//Debug.Log ("loaded");
		} else {
			//Debug.Log ("no save found");
		}
	}

	public void SaveMob(){

		XmlSerializer serializer = new XmlSerializer(typeof(List<MobStats>));
		string path = Application.dataPath + "/StreamingAssets/DataFiles/mob_Data.xml";
		if(File.Exists(path)){
			File.Delete(path);
		}

		StreamWriter stream = new StreamWriter (path, false);



		serializer.Serialize (stream, GameInformation.MobStatsList);


		stream.Close ();
		//Debug.Log ("saved");

	}

	public void LoadMob(){


		string path = Application.dataPath + "/StreamingAssets/DataFiles/mob_Data.xml";
		XmlSerializer serializer = new XmlSerializer(typeof(List<MobStats>));

		if (File.Exists (path)) {
			StreamReader stream = new StreamReader (path, true);
			GameInformation.MobStatsList = serializer.Deserialize (stream) as List<MobStats>;
			stream.Close ();
			//Debug.Log ("loaded");
		} else {
			//Debug.Log ("no save found");
		}
	}

	public void SaveBattleData(){

		XmlSerializer serializer = new XmlSerializer(typeof(List<BattleData>));
		string path = Application.dataPath + "/StreamingAssets/GameData/battle_Data.xml";
		if(File.Exists(path)){
			File.Delete(path);
		}

		StreamWriter stream = new StreamWriter (path, false);



		serializer.Serialize (stream, GameInformation.BattleDataList);


		stream.Close ();
		//Debug.Log ("saved");

	}

	public void LoadBattleData(){

		string path = Application.dataPath + "/StreamingAssets/GameData/battle_Data.xml";
		XmlSerializer serializer = new XmlSerializer(typeof(List<BattleData>));

		if (File.Exists (path)) {
			StreamReader stream = new StreamReader (path, true);
			GameInformation.BattleDataList = serializer.Deserialize (stream) as List<BattleData>;
			stream.Close ();
			//Debug.Log ("loaded");
		} else {
			//Debug.Log ("no save found");
		}
	}


}

[System.Serializable]
public class PlayerCharacter{
	public string name;
	public Race race;
	public int health;
}

[System.Serializable]
public class playerDatabase{

	[XmlArrayItem("player_races")]
	public List<PlayerCharacter> list = new List<PlayerCharacter>();


}

public enum Race{
	Dwarven,
	Elven,
	Goblin,
	Human,
	Merman,
	Orcish,
	DarkElven
}

