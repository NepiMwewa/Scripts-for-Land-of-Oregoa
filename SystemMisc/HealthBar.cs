using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	[SerializeField] private GameObject healthbar;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateHealthBar(int currentHealth, int maxHealth)
	{
		float tempTotal, tempA, tempB;
		Vector3 tempVector;
		tempA = currentHealth;
		tempB = maxHealth;

		tempTotal = tempA / tempB;

		tempVector = new Vector3 (tempTotal, healthbar.transform.localScale.y, healthbar.transform.localScale.z);

		healthbar.transform.localScale = tempVector;

	}
}
