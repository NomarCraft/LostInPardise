using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other){
        ItemData item = other.GetComponent<ItemObject>().item;
        if(item){
            inventory.AddItem(item.id, 1);
            Destroy(other.gameObject);
            GameManager.Instance.invDis.ChangeDisplay(0);
        }
    }
    /*private void OnApplicationQuit(){
        inventory.container.Clear();
    }*/
}
