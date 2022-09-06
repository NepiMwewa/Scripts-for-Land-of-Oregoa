using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListSelectedItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

	private BattleManager bManager;
	private Text selectedItemText;
	private GameObject itemText;
	public BaseItem SlotItem{ get; set; }

	private int slotName;

	// Use this for initialization

	void Awake(){
		bManager = FindObjectOfType<BattleManager> ();
		selectedItemText = GameObject.Find ("SelectedItemText").transform.GetChild (0).GetComponent<Text> ();
		itemText = selectedItemText.transform.parent.gameObject;
		itemText.SetActive (false);

	}

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowSelectedItemText()
	{

			selectedItemText.text = SlotItem.ItemName + " " + SlotItem.ItemDescription;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//ShowSelectedItemText ();
		bManager.UseItem (SlotItem);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		itemText.SetActive (true);
		ShowSelectedItemText ();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		itemText.SetActive (false);
	}
}
