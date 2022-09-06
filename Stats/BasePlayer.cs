using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour {

	//private RPGItemDatabase itemDatabase = new RPGItemDatabase();

	//private InventoryWindow inv = new InventoryWindow();

//	private List<BaseStat> _playerStats = new List<BaseStat>();

	private List<BaseItem> _inventory = new List<BaseItem> ();

	private int _goldAmount;



	// Use this for initialization
	void Start () {
		_goldAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<BaseItem> Inventory
	{
		get{ return _inventory; }
		set{ _inventory = value; }
	}

	public List<BaseItem> ReturnPlayerInventory()
	{
		return _inventory;
	}

	public int GoldAmount
	{
		get{ return _goldAmount; }
		set{ _goldAmount = value; }
	}

	public int ReturnPlayerGoldAmount()
	{
		return _goldAmount;
	}
		
}
