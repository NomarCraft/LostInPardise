using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
	private GameManager _gm;
	public GameManager gm
	{
		get
		{
			if (!_gm)
				_gm = GameManager.Instance;

			return _gm;
		}
	}

    [SerializeField] public List<InventoryDisplayPack> packs;
    /*public List<InventoryObject> inventories;

    [SerializeField] Transform itemPage;*/

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    void UpdateDisplay(int nb){

		packs = gm.packs;
        NUMBER_OF_COLUMN = Mathf.RoundToInt(packs[nb].invetoryPage.GetComponent<RectTransform>().sizeDelta.x / X_SPACE_BETWEEN_ITEM);
        
        for (int i = 0; i < packs[nb].inventory.container.Count; i++)
        {
            if(packs[nb].itemsDisplayed.ContainsKey(packs[nb].inventory.container[i])){
                packs[nb].itemsDisplayed[packs[nb].inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = packs[nb].inventory.container[i].amount.ToString("n0");
            }else{
                var obj = Instantiate(packs[nb].inventory.container[i].item.menuAsset, Vector3.zero, Quaternion.identity, packs[nb].invetoryPage);
                var butt = obj.GetComponent<ButtonSelection>();
				butt.compendiumData = packs[nb].inventory.container[i].item;
				butt.inventoryPack = nb;
                RectTransform trans = obj.GetComponent<RectTransform>();
                trans.localPosition = GetPosition(i);
                trans.anchorMax = new Vector2(0, 1);
                trans.anchorMin = new Vector2(0, 1);
                trans.pivot = new Vector2(0, 1);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = packs[nb].inventory.container[i].amount.ToString("n0");
                packs[nb].itemsDisplayed.Add(packs[nb].inventory.container[i], butt);
				if (i == 0)
					gm.uiEvents.SetSelectedGameObject(obj);
			}
        }
    }

    public void ChangeDisplay(int nb){
		packs = gm.packs;
		packs[nb].inventory.SortInventoryById();

        foreach (var obj in packs[nb].itemsDisplayed)
        {
            Destroy(obj.Value.gameObject);
        }
        packs[nb].itemsDisplayed.Clear();

        UpdateDisplay(nb);
    }

    public void ChangeDisplayAll(){
        for (int i = 0; i < packs.Count; i++)
        {
            ChangeDisplay(i);
        }
    }

    public Vector3 GetPosition(int i){
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN)), 0f);
    }

    public void TransferItems(ButtonSelection buttonSelection, int quantity){

        bool added = false;

        for (int i = 0; i < packs.Count; i++)
        {
            if((i != buttonSelection.inventoryPack) && i != 0){
                if(packs[i].inventory.AddItem(buttonSelection.compendiumData.id)){
                    packs[i].inventory.AddItem(buttonSelection.compendiumData.id, quantity - 1);
                    added = true;
                }
            }
        }
        
        if(added){
            packs[buttonSelection.inventoryPack].inventory.RemoveItem(buttonSelection.compendiumData as ItemData, quantity);
            ChangeDisplayAll();
        }
    }
}


[System.Serializable]
public class InventoryDisplayPack{
    public InventoryObject inventory;
    public Transform invetoryPage;
    //public Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public Dictionary<InventorySlot, ButtonSelection> itemsDisplayed = new Dictionary<InventorySlot, ButtonSelection>();
}