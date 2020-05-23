﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryObject inventory;

    [SerializeField] Transform itemPage;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
   Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
   List<GameObject> menuItems;

    void Start(){
        CreateDisplay();
    }
    void Update(){
        UpdateDisplay();
    }

    public void CreateDisplay(){
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.menuAsset, Vector3.zero, Quaternion.identity, transform);
            //menuItems.Add(obj);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            Debug.Log(obj.name);
        }
    }

    public void UpdateDisplay(){
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if(itemsDisplayed.ContainsKey(inventory.container[i])){
                itemsDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            }else{
                var obj = Instantiate(inventory.container[i].item.menuAsset, Vector3.zero, Quaternion.identity, itemPage);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.container[i], obj);
            }
        }
    }

    public void ChangeDisplay(){

        inventory.SortInventoryById();

        foreach (var item in itemsDisplayed)
        {
            Destroy(item.Value);
        }
        itemsDisplayed.Clear();

        //CreateDisplay();
    }

    public Vector3 GetPosition(int i){
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN)), 0f);
    }
}
