using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml;
using System.Xml.Serialization;


public class ItemDatabaseScript : MonoBehaviour {

	public List<BaseItem> InventoryItems{ get; set; }

	public void Awake(){
		LoadItemDatabase();
	}

	public void Save(){

		XmlSerializer serializer = new XmlSerializer(typeof(List<BaseItem>));
			string path = Application.dataPath + "/StreamingAssets/GameData/item_Data.xml";
			if(File.Exists(path)){
				File.Delete(path);
			}

			StreamWriter stream = new StreamWriter (path, false);



		serializer.Serialize (stream, GameInformation.ItemDatabase);


			stream.Close ();
			//Debug.Log ("saved");

	}

	public void LoadItemDatabase(){
		string path = Application.dataPath + "/StreamingAssets/GameData/item_Data.xml";
		XmlSerializer serializer = new XmlSerializer(typeof(List<BaseItem>));

		if (File.Exists (path)) {
			StreamReader stream = new StreamReader (path, true);
			GameInformation.ItemDatabase = serializer.Deserialize (stream) as List<BaseItem>;
			stream.Close ();
			//Debug.Log ("loaded");
		} else {
			//Debug.Log ("no save found");
		}
	}
}
