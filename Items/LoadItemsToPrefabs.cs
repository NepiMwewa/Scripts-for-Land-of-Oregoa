using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadItemsToPrefabs : MonoBehaviour
{
	/*
	private GameObject tempBaseGameObject, outputGameObject;
	private quickDragItems tempQuickItem, outputQuickItem;
	[SerializeField] private bool runLoadCode, runCreateCode;
	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (runLoadCode)
        {
			LoadItemsToPrefab();
			runLoadCode = false;
        }
		if (runCreateCode)
		{
			CreateNewPrefabs();
			runCreateCode = false;
		}
	}
	private void CreateNewPrefabs()
    {
		const string assetPath = "Assets/Prefabs/Items/baseItemPrefab.prefab";

		for (int i = 0; i < GameInformation.ItemDatabase.Count; i++)
		{
			tempBaseGameObject = null;
			tempQuickItem = null;
			if (AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Items/" + GameInformation.ItemDatabase[i].ItemSlug + ".prefab",
					 typeof(GameObject)) as GameObject == null) {
				if (!AssetDatabase.CopyAsset(assetPath, "Assets/Prefabs/Items/" + GameInformation.ItemDatabase[i].ItemSlug + ".prefab"))
				{
					Debug.Log("Item: " + i + " failed to be copied");
				}

				tempBaseGameObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Items/" + GameInformation.ItemDatabase[i].ItemSlug + ".prefab",
					 typeof(GameObject)) as GameObject;
				tempQuickItem = tempBaseGameObject.GetComponent<quickDragItems>();

				tempQuickItem = LoadBaseItemsToItems(tempQuickItem, i);

				Debug.Log("The item is: " + GameInformation.ItemDatabase[i].ItemName.ToString());
                Debug.Log("The item is: " + tempQuickItem.ItemName.ToString());
				tempBaseGameObject.GetComponent<SpriteRenderer>().sprite = ReturnItemIcon(tempQuickItem.ItemSlug);

				EditorUtility.SetDirty(tempBaseGameObject);
			}
		}
		AssetDatabase.SaveAssets();
	}
	private void LoadItemsToPrefab()
	{
		//Update asset
		for (int i = 0; i < GameInformation.ItemDatabase.Count; i++)
		{
			tempBaseGameObject = null;
			tempQuickItem = null;
			if (AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Items/" + GameInformation.ItemDatabase[i].ItemSlug + ".prefab",
					 typeof(GameObject)) as GameObject != null)
			{
				tempBaseGameObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Items/" + GameInformation.ItemDatabase[i].ItemSlug.ToString() + ".prefab",
				 typeof(GameObject)) as GameObject;

				//tempBaseGameObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Items/baseItemPrefab.prefab",
				//		 typeof(GameObject)) as GameObject;
				tempQuickItem = tempBaseGameObject.GetComponent<quickDragItems>();

				tempQuickItem = LoadBaseItemsToItems(tempQuickItem, i);
				//tempQuickItem.ItemName = GameInformation.ItemDatabase[i].ItemName;
				//tempQuickItem.ItemID = tempQuickItem.Item.ItemID;

				Debug.Log("The item is: " + GameInformation.ItemDatabase[i].ItemName.ToString());
				Debug.Log("The item is: " + tempQuickItem.ItemName.ToString());
				tempBaseGameObject.GetComponent<SpriteRenderer>().sprite = ReturnItemIcon(tempQuickItem.ItemSlug);


				EditorUtility.SetDirty(tempBaseGameObject);
				//AssetDatabase.SaveAssetIfDirty(tempBaseGameObject);
			}
            else
            {
				Debug.Log("Couldn't find item at position: " + i);
            }
		}
		AssetDatabase.SaveAssets();
	}

	private quickDragItems LoadBaseItemsToItems(quickDragItems tempQDrag, int x)
    {
		tempQDrag.ItemName = GameInformation.ItemDatabase[x].ItemName;
		tempQDrag.ItemID = GameInformation.ItemDatabase[x].ItemID;
		tempQDrag.ItemDescription = GameInformation.ItemDatabase[x].ItemDescription;
		tempQDrag.ItemSlug = GameInformation.ItemDatabase[x].ItemSlug;
		tempQDrag.ItemValue = GameInformation.ItemDatabase[x].ItemValue;

		tempQDrag.ItemType = GameInformation.ItemDatabase[x].ItemType;
		tempQDrag.EquipmentType = GameInformation.ItemDatabase[x].EquipmentType;
		tempQDrag.EquipmentID = GameInformation.ItemDatabase[x].EquipmentID;
		tempQDrag.ItemWeaponDamage = GameInformation.ItemDatabase[x].ItemWeaponDamage;
		tempQDrag.ItemWeaponPen = GameInformation.ItemDatabase[x].ItemWeaponPen;
		tempQDrag.ItemPotionHpAmount = GameInformation.ItemDatabase[x].ItemPotionHpAmount;

		tempQDrag.ItemPotionMPAmount = GameInformation.ItemDatabase[x].ItemPotionMPAmount;
		tempQDrag.ItemArmor = GameInformation.ItemDatabase[x].ItemArmor;
		tempQDrag.ItemCurrentDura = GameInformation.ItemDatabase[x].ItemCurrentDura;
		tempQDrag.ItemMaxDura = GameInformation.ItemDatabase[x].ItemMaxDura;
		tempQDrag.ItemStackable = GameInformation.ItemDatabase[x].ItemStackable;
		tempQDrag.ItemCurrentAmount = GameInformation.ItemDatabase[x].ItemCurrentAmount;
		tempQDrag.ItemMaxAmount = GameInformation.ItemDatabase[x].ItemMaxAmount;
		return tempQDrag;
    }
	private Sprite ReturnItemIcon(string itemSlug)
	{
		Sprite icon;

		icon = Resources.Load<Sprite>("ItemIcons/" + itemSlug);

		return icon;
	}

	*/
}
