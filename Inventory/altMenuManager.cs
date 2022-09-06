using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class altMenuManager : MonoBehaviour {

	private SelectedItem invItem;
	private EquipmentSelectedItem eqItem;
	private PlayerHealthManager pHealth;
	private InventoryWindow inv;
	public GameObject thisChild;
	private const int mousePositionOffset = 47;
	private bool calledByEq = false, calledByInv = false;

	// Use this for initialization
	void Awake () {
		thisChild = transform.GetChild (0).gameObject;
		pHealth = FindObjectOfType<PlayerHealthManager> ();
		inv = FindObjectOfType<InventoryWindow> ();
	}
	void Start()
	{
		closeAltMenu ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void closeAltMenu()
	{
		thisChild.SetActive (false);
	}

	public void openAltMenuByEQ(EquipmentSelectedItem temp)
	{
		calledByEq = true;
		calledByInv = false;
		eqItem = temp;
		thisChild.SetActive (true);
		Vector3 mousePosition = (Input.mousePosition - GameObject.FindGameObjectWithTag ("Canvas").GetComponent<RectTransform> ().localPosition);
		thisChild.transform.localPosition = new 
			Vector3 (mousePosition.x + mousePositionOffset, mousePosition.y - mousePositionOffset, mousePosition.z);
	}
	public void openAltMenuByINV(SelectedItem temp)
	{
		calledByEq = false;
		calledByInv = true;
		invItem = temp;
		thisChild.SetActive (true);
		Vector3 mousePosition = (Input.mousePosition - GameObject.FindGameObjectWithTag ("Canvas").GetComponent<RectTransform> ().localPosition);
		thisChild.transform.localPosition = new 
			Vector3 (mousePosition.x + mousePositionOffset, mousePosition.y - mousePositionOffset, mousePosition.z);

	}

	public void destoryItem(){
		if (calledByEq && !calledByInv) {
			
				eqItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
				eqItem.transform.GetChild (0).gameObject.SetActive (false);

				eqItem.EquipmentSlotItem = null;

				eqItem.transform.name = "Empty";
		} else if (!calledByEq && calledByInv) {
			
				invItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
				invItem.transform.GetChild (0).gameObject.SetActive (false);

				invItem.ThisItem = null;

				invItem.transform.name = "Empty";

		}
		thisChild.SetActive (false);
	}

	public void useItem(){
		if (calledByEq && !calledByInv) {
			if (eqItem.EquipmentSlotItem.EquipmentType != BaseItem.EquipmentSlotTypes.Consumable ) {
				return;
			}
			pHealth.playerCurrentHealth += eqItem.EquipmentSlotItem.ItemPotionHpAmount;
			GameInformation.PlayerStats.CurrentHealth += eqItem.EquipmentSlotItem.ItemPotionHpAmount;
			GameInformation.PlayerStats.CurrentStamina += eqItem.EquipmentSlotItem.ItemPotionMPAmount;
			if (eqItem.EquipmentSlotItem.ItemCurrentAmount == 1) {
				eqItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
				eqItem.transform.GetChild (0).gameObject.SetActive (false);

				eqItem.EquipmentSlotItem = null;

				eqItem.transform.name = "Empty";
			} else {
				eqItem.EquipmentSlotItem.ItemCurrentAmount -= 1;
				if (eqItem.EquipmentSlotItem.ItemCurrentAmount == 1) {
					eqItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
				} else {
					eqItem.transform.GetChild (0).GetComponentInChildren<Text> ().text = eqItem.EquipmentSlotItem.ItemCurrentAmount.ToString ();
				}
			}
		} else if (!calledByEq && calledByInv) {
			if (invItem.ThisItem.EquipmentType != BaseItem.EquipmentSlotTypes.Consumable ) {
				return;
			}
			pHealth.playerCurrentHealth += invItem.ThisItem.ItemPotionHpAmount;
			GameInformation.PlayerStats.CurrentHealth += invItem.ThisItem.ItemPotionHpAmount;
			GameInformation.PlayerStats.CurrentStamina += invItem.ThisItem.ItemPotionMPAmount;
			if (invItem.ThisItem.ItemCurrentAmount == 1) {
				invItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
				invItem.transform.GetChild (0).gameObject.SetActive (false);

				invItem.ThisItem = null;

				invItem.transform.name = "Empty";
			} else {
				invItem.ThisItem.ItemCurrentAmount -= 1;
				if (invItem.ThisItem.ItemCurrentAmount == 1) {
					invItem.transform.GetChild (0).transform.GetChild(0).gameObject.SetActive (false);
				} else {
					invItem.transform.GetChild (0).GetComponentInChildren<Text> ().text = invItem.ThisItem.ItemCurrentAmount.ToString ();
				}

			}
		}
		if (pHealth.playerCurrentHealth > pHealth.playerMaxHealth) {
			pHealth.playerCurrentHealth = pHealth.playerMaxHealth;
		}
		if (GameInformation.PlayerStats.CurrentHealth > GameInformation.PlayerStats.MaxHealth) {
			GameInformation.PlayerStats.CurrentHealth = GameInformation.PlayerStats.MaxHealth;
		}
		if (GameInformation.PlayerStats.CurrentStamina > GameInformation.PlayerStats.MaxStamina) {
			GameInformation.PlayerStats.CurrentStamina = GameInformation.PlayerStats.MaxStamina;
		}
		thisChild.SetActive (false);

	}
}
