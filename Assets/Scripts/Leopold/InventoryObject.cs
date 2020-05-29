using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 //[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Inventory")]
public class InventoryObject : MonoBehaviour
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public float maximumWeight = Mathf.Infinity;
    public float actualWeight;

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

	private UIManager _ui;
	public UIManager ui
	{
		get
		{
			if (!_ui)
				_ui = GameManager.Instance.uiManager;

			return _ui;
		}
	}

    public void AddItem(int id, int amount){

        bool hasItem = false;
        ItemData item;

        item = compendium.GetItemReference(id);

        if(actualWeight + item.itemWeight <= maximumWeight){

            for(int i = 0; i < container.Count; i++){
                if(container[i].item.id == id){
                    item = container[i].item;
                    container[i].AddAmount(amount);
                    hasItem = true;
                    break;
                }
            }
            if(!hasItem){
                container.Add(new InventorySlot(item, amount));
                //compendium.CheckCompendium(item);
            }

			if (ui != null)
			{
				ui.DisplayElement(ui._displayMessagePanel);
				ui.DisplayTemporaryMessageWithColor(ui._itemDisplayMessageText, "You acquired " + amount.ToString() + " " + item.itemName, Color.green);
			}

			actualWeight += item.itemWeight;

        }else{

            if (ui != null)
			{
				ui.DisplayElement(ui._displayMessagePanel);
				ui.DisplayTemporaryMessageWithColor(ui._itemDisplayMessageText, "You can't get " + amount.ToString() + " " + item.itemName + " because your inventory is full", Color.red);
			}

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

	public bool CheckItem(int id)
	{
		for (int i = 0; i < container.Count; i++)
		{
			if (container[i].item.id == id)
				return true;
		}

		return false;
	}

	public bool CheckItem(int id, out int amount)
	{
		for (int i = 0; i < container.Count; i++)
		{
			if (container[i].item.id == id)
			{
				amount = container[i].amount;
				return true;
			}
		}

		amount = 0;
		return false;
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