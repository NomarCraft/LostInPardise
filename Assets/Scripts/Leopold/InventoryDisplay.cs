using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public List<InventoryDisplayPack> packs;
    /*public List<InventoryObject> inventories;

    [SerializeField] Transform itemPage;*/

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
/*
    void Start(){
        CreateDisplay();
    }*/
    void Update(){
        //Remove Line when trying opening and closing the inventory
		ChangeDisplay(0);
    }
/*
    public void CreateDisplay(){
        
        foreach (var item in inventories)
        {
            for (int i = 0; i < item.container.Count; i++)
            {
                var obj = Instantiate(item.container[i].item.menuAsset, Vector3.zero, Quaternion.identity, transform);
                NUMBER_OF_COLUMN = Mathf.RoundToInt(itemPage.GetComponent<RectTransform>().sizeDelta.x / X_SPACE_BETWEEN_ITEM);
                RectTransform trans = obj.GetComponent<RectTransform>();
                trans.localPosition = GetPosition(i);
                trans.anchorMax = new Vector2(0, 1);
                trans.anchorMin = new Vector2(0, 1);
                trans.pivot = new Vector2(0, 1);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = item.container[i].amount.ToString("n0");
            }
        }
    }*/

    void UpdateDisplay(int nb){
        for (int i = 0; i < packs[nb].inventory.container.Count; i++)
        {
            if(packs[nb].itemsDisplayed.ContainsKey(packs[nb].inventory.container[i])){
                packs[nb].itemsDisplayed[packs[nb].inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = packs[nb].inventory.container[i].amount.ToString("n0");
            }else{
                NUMBER_OF_COLUMN = Mathf.RoundToInt(packs[nb].invetoryPage.GetComponent<RectTransform>().sizeDelta.x / X_SPACE_BETWEEN_ITEM);
                var obj = Instantiate(packs[nb].inventory.container[i].item.menuAsset, Vector3.zero, Quaternion.identity, packs[nb].invetoryPage);
                RectTransform trans = obj.GetComponent<RectTransform>();
                trans.localPosition = GetPosition(i);
                trans.anchorMax = new Vector2(0, 1);
                trans.anchorMin = new Vector2(0, 1);
                trans.pivot = new Vector2(0, 1);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = packs[nb].inventory.container[i].amount.ToString("n0");
                packs[nb].itemsDisplayed.Add(packs[nb].inventory.container[i], obj);
            }
        }
    }

    public void ChangeDisplay(int nb){
        packs[nb].inventory.SortInventoryById();

        foreach (var obj in packs[nb].itemsDisplayed)
        {
            Destroy(obj.Value);
        }
        packs[nb].itemsDisplayed.Clear();

        UpdateDisplay(nb);
    }

    public Vector3 GetPosition(int i){
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN)), 0f);
    }
}

[System.Serializable]
public class InventoryDisplayPack{
    public InventoryObject inventory;
    public Transform invetoryPage;
    public Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
}