using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelected : MonoBehaviour{
	private BattleManager bManager;

	// Use this for initialization
	void Start () {
		bManager = FindObjectOfType<BattleManager>();
	}


	public void OnButtonClick()
	{
		////Debug.Log ("clicked");
		bManager.EnemySelected (this.gameObject.GetComponentInParent<EnemyStats>());
	}
	void OnMouseDown()
	{
		bManager.EnemySelected (this.gameObject.GetComponentInParent<EnemyStats>());
	}


}
