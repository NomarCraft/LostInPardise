using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory System/Recipe")]
public class RecipeData : CompendiumData
{
    DataType dataType = DataType.Recipe;

	//Maybe change to id
	public int craftedItemId;
    public List<Ingredient> ingredients;
}

[System.Serializable]
public class Ingredient
{
	public bool unlocked;
	public ItemData ingredient;
	public int amount;
}
