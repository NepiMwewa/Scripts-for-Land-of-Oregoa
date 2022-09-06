using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppedItemScript: MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	[SerializeField] private bool isItem;
	[SerializeField] private int droppedID;
	[SerializeField] private int goldAmount;
	private InventoryWindow inv;

    private void Awake()
    {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
	}
    // Use this for initialization
    void Start () {
		inv = FindObjectOfType<InventoryWindow> ();
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setupThisItem(bool tempIsItem,int itemId, int amount)
	{
		isItem = tempIsItem;
		if (tempIsItem) {
			droppedID = itemId;

			spriteRenderer.sprite = ReturnItemIcon (GameInformation.ItemDatabase [droppedID]);
			//Debug.Log ("show sprite");
		} else {
			//show gold
			goldAmount = amount;//chance the renderer to show different gold sprites depending on the amount of gold
			spriteRenderer.sprite = ReturnGoldIcon();
		}
	}


	private Sprite ReturnItemIcon(BaseItem item)
	{
		Sprite icon;

		icon = Resources.Load<Sprite> ("ItemIcons/" + item.ItemSlug);

		return icon;
	}
	private Sprite ReturnGoldIcon()
	{
		Sprite icon = null;

		if (goldAmount <= 50) {
			icon = Resources.Load<Sprite> ("ItemIcons/" + "copperCoin");
		} else if (goldAmount <= 250 && goldAmount > 50) {
			icon = Resources.Load<Sprite> ("ItemIcons/" + "silverCoin");
		} else if (goldAmount > 250) {
			icon = Resources.Load<Sprite> ("ItemIcons/" + "goldCoin");
		}

		return icon;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			//pick up item
			if (isItem) {
				inv.AddItemByID (droppedID);
			} else {
				GameInformation.PlayerGoldAmount += goldAmount;
				inv.RefreshPlayerGold ();
			}

			Destroy (gameObject);
		}
	}
}
