using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySellButtonScript : MonoBehaviour {

	private bool selling, buying;
	private BaseItem tempItem;
	private SelectedItem tempSelected;
	private ShopInventory sWindow;
	private InventoryWindow pWindow;
	[SerializeField] private Text displayText;
	[SerializeField] private GameObject yesNoButtons, nextButton;
	private GameObject childPanel;

	private string sellStringStart = "Do you want to sell this item for ", sellStringEnd = " gold", buyStringStart = "Do you want to buy this item for ";
	private string notEnoughGoldString = "Not enough gold to buy the item", notEnoughRoom = "Not enough room in Inventory";

	// Use this for initialization
	void Start () {
		sWindow = FindObjectOfType<ShopInventory> ();
		pWindow = FindObjectOfType<InventoryWindow> ();
		childPanel = gameObject.transform.GetChild (0).gameObject;
		childPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setupBItem(BaseItem item)
	{
		if (!selling && !buying) {
			childPanel.SetActive (true);
			yesNoButtons.SetActive (true);
			nextButton.SetActive (false);
			displayText.text = buyStringStart + item.ItemValue + sellStringEnd;
			tempItem = item;
			buying = true;
		}
	}
	public void setupSItem(SelectedItem tempS)
	{
		if (!buying && !selling) {
			childPanel.SetActive (true);
			yesNoButtons.SetActive (true);
			nextButton.SetActive (false);
			displayText.text = sellStringStart + tempS.ThisItem.ItemValue + sellStringEnd;
			tempSelected = tempS;
			selling = true;
		}
	}

	public void NotEnoughGold()
	{
		//////Debug.Log ("ran not enough gold");
		childPanel.SetActive (true);
		yesNoButtons.SetActive (false);
		nextButton.SetActive (true);
		displayText.text = notEnoughGoldString;
	}

	public void NotEnoughSpace()
	{
		childPanel.SetActive (true);
		yesNoButtons.SetActive (false);
		nextButton.SetActive (true);
		displayText.text = notEnoughRoom;
	}

	public void yes()
	{
		childPanel.SetActive (false);
		if (selling) {
			pWindow.PlayerWantsToSell (tempSelected);
		}
		if (buying) {
			sWindow.PlayerWantsToBuy (tempItem);
		}
		buying = false;
		selling = false;
		tempItem = new BaseItem ();
		tempSelected = null;
	}
	public void no()
	{
		buying = false;
		selling = false;
		tempItem = new BaseItem ();
		tempSelected = null;
		childPanel.SetActive (false);
	}

	public void CloseThis()
	{
		childPanel = gameObject.transform.GetChild (0).gameObject;
		buying = false;
		selling = false;
		tempItem = new BaseItem ();
		tempSelected = null;
		childPanel.SetActive (false);
	}
}
