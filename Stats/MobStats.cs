using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobStats{

	public string Name{ get; set; }
	public int MobTypeID{ get; set; }
	public int Health{ get; set; }
	public Vector3 Position{ get; set; }

}