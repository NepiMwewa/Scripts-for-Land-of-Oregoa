using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class PPSerialization{

	public static BinaryFormatter bf = new BinaryFormatter();

	protected string saveLocation = Application.persistentDataPath + "/gamePrefs.gPrefs";

	public static void saveObj(string saveTag, object saveObj)
	{
		//FileStream
	}

	public static object loadObj(string saveTag){
		return null;
	}

	public static void Save (string saveTag, object saveObj)
	{
		MemoryStream memorysteam = new MemoryStream ();
		bf.Serialize (memorysteam, saveObj);
		string temp = System.Convert.ToBase64String (memorysteam.ToArray ());
		PlayerPrefs.SetString (saveTag, temp);
	}

	public static object Load(string saveTag){
		string temp = PlayerPrefs.GetString (saveTag);
		if (temp == string.Empty) {
			return null;
		}
			
		MemoryStream memoryStream = new MemoryStream (System.Convert.FromBase64String(temp));
		return bf.Deserialize (memoryStream);
	}
 
}
