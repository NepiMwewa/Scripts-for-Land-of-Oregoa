using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

[System.Serializable]
public class BattleData{

	[XmlAttribute("attack")]
	public string AttackSlug{ get; set; }
	public int AttackID { get; set; }
	public string AttackName{ get; set; }
	public string AttackDescription{ get; set; }

	public int AttackDmg{ get; set; }


}
