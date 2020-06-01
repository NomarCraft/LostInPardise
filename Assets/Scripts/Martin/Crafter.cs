using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
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

	private UIManager _ui;
	public UIManager ui
	{
		get
		{
			if (!_ui)
				_ui = gm.uiManager;

			return _ui;
		}
	}

	private Compendium _comp;
	public Compendium comp
	{
		get
		{
			if (!_comp)
				_comp = gm.comp;

			return _comp;
		}
	}

	public void Test()
	{
		Craft(gm.comp.recipeDictionnaryInstance[0].recipe.ingredients, gm.invDis.packs, gm.comp.recipeDictionnaryInstance[0].recipe.craftedItemId, 0);
	}

	public void Craft(List<Ingredient> ingredients, List<InventoryDisplayPack> inventories, int craftedItem, int target)
	{
		foreach (Ingredient item in ingredients)
		{
			int currentAmount = 0;

			for (int i = 0; i < inventories.Count; i++)
			{
				if (i == 1)
				{
					//Avoid checking the inventory twice
				}
				else
				{
					int itemAmount = 0;
					if (inventories[i].inventory.CheckItem(item.ingredient.id, out itemAmount))
						currentAmount += itemAmount;

					Debug.Log(item.ingredient.id);
					Debug.Log(inventories[i].inventory.CheckItem(item.ingredient.id, out itemAmount));
				}
			}

			if (currentAmount < item.amount)
			{
				if (ui)
				{
					ui.DisplayElement(ui._displayMessagePanel);
					ui.DisplayTemporaryMessageWithColor(_ui._itemDisplayMessageText, "You can't craft " + comp.GetItemReference(craftedItem).itemName + " because you need more " + item.ingredient.itemName, Color.red);
				}

				return;
			}
		}

		if (inventories[target].inventory.AddItem(craftedItem))
		{
			//Remove Items mais c'est chiant
		}
	}
}
