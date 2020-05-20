using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory System/Recipe")]
public class Recipe : CompendiumData
{
    DataType dataType = DataType.Recipe;
    public List<Item> ingredients;
}
