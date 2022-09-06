using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class NpcStats{

	[XmlAttribute("NPC")]
	public string NpcName{ get; set; }
	public Vector3 Position{ get; set; }


}