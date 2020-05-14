﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other){
        var item = other.GetComponent<ItemObject>().item;
        if(item){
            inventory.AddItem(item, 1);
            Destroy(other.gameObject);
        }
    }
    private void OnApplicationQuit(){
        inventory.Container.Clear();
    }
}
