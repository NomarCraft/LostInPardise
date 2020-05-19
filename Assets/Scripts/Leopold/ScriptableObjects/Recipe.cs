using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{
    public bool unlocked;
    public int id;
    public string itemName;
    public List<Item> ingredients = new List<Item>();
    public Item result;
    public GameObject menuAsset;
    [Tooltip("Use Menu Asset instead of sprite")]
    public Sprite sprite;
    
    [TextArea(1, 20)]
    public string description;
}
