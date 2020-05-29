using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
{
	public void Craft(List<Ingredient> ingredients, List<InventoryDisplayPack> inventories, int target)
	{
		
		foreach (Ingredient item in ingredients)
		{
			for (int i = 0; i < inventories.Count; i++)
			{
				if (i == 1)
				{
					//Avoid checking the inventory twice
				}
				else
				{
					///////inventories[i];
				}
			}
		}
	}
}
