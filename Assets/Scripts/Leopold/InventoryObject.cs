using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 //[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Inventory")]
public class InventoryObject : MonoBehaviour
{
    public List<InventorySlot> container = new List<InventorySlot>();

	private Compendium _compendium;
    public Compendium compendium
	{
		get
		{
			if (!_compendium)
				_compendium = GameManager.Instance.comp;

			return _compendium;
		}
	}

    public void AddItem(int id, int amount){

        bool hasItem = false;
        ItemData item;

        for(int i = 0; i < container.Count; i++){
            if(container[i].item.id == id){
                container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem){
            item = compendium.GetItemReference(id);
            container.Add(new InventorySlot(item, amount));
            //compendium.CheckCompendium(item);
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

    public void RemoveItem(ItemData item){

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
    public ItemData item;
    public int amount;
    public InventorySlot(ItemData _item, int _amount){
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value){
        amount += value;
    }
}