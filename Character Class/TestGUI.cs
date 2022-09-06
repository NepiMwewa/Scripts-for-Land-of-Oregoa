using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGUI : MonoBehaviour {

	private BaseCharacterRace class1 = new BaseDwarvenRace ();
	private BaseCharacterRace class2 = new BaseGoblinRace();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		GUILayout.Label (class1.CharacterRaceName);	
		GUILayout.Label (class1.CharacterRaceDescription);
		GUILayout.Label (class2.CharacterRaceName);	
		GUILayout.Label (class2.CharacterRaceDescription);
	}
}
