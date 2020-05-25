using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory System/Recipe")]
public class RecipeData : CompendiumData
{
    DataType dataType = DataType.Recipe;

    //Maybe change to id
    public List<ItemData> ingredients;
}
