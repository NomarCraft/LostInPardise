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
                compendium.CheckCompendium(item.id);
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

	public bool AddItem(int id)
	{
		bool hasItem = false;
		ItemData item;

		item = compendium.GetItemReference(id);

		if (actualWeight + item.itemWeight <= maximumWeight)
		{

			for (int i = 0; i < container.Count; i++)
			{
				if (container[i].item.id == id)
				{
					item = container[i].item;
					container[i].AddAmount(1);
					hasItem = true;
					break;
				}
			}
			if (!hasItem)
			{
				container.Add(new InventorySlot(item, 1));
				compendium.CheckCompendium(item.id);
			}

			if (ui != null)
			{
				ui.DisplayElement(ui._displayMessagePanel);
				ui.DisplayTemporaryMessageWithColor(ui._itemDisplayMessageText, "You acquired " + 1.ToString() + " " + item.itemName, Color.green);
			}

			actualWeight += item.itemWeight;
			return true;
		}
		else
		{

			if (ui != null)
			{
				ui.DisplayElement(ui._displayMessagePanel);
				ui.DisplayTemporaryMessageWithColor(ui._itemDisplayMessageText, "You can't get " + 1.ToString() + " " + item.itemName + " because your inventory is full", Color.red);
			}
			return false;
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

    public void RemoveItem(ItemData item, int amount){

        for (int i = 0; i < container.Count; i++)
        {
			Debug.Log(amount);
            if(container[i].item.id == item.id){
				container[i].amount -= amount;
				if (container[i].amount <= 0)
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
			Debug.Log(container[i].item.id);
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