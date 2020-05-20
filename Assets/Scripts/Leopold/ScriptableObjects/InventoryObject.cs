using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 //[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Inventory")]
public class InventoryObject : MonoBehaviour
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public Compendium compendium;

    public void AddItem(Item item, int amount){

        bool hasItem = false;

        for(int i = 0; i < container.Count; i++){
            if(container[i].item == item){
                container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem){
            container.Add(new InventorySlot(item, amount));
            compendium.CheckCompendium(item);
        }
    }

    public void SortInventoryById(){

        container.Sort(delegate(InventorySlot x, InventorySlot y)
        {
            if (x.item.id == 0 && y.item.id == 0) return 0;
            else if (x.item.id == 0) return -1;
            else if (y.item.id == 0) return 1;
            else return x.item.id.CompareTo(y.item.id);
        });
    }

    public void RemoveItem(Item item){

        for (int i = 0; i < container.Count; i++)
        {
            if(container[i].item == item){
                container.Remove(container[i]);
                break;
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
    public InventorySlot(Item _item, int _amount){
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value){
        amount += value;
    }
}