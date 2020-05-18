using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{
    public List<Item> ingredients = new List<Item>();
    public bool unlocked;
}
