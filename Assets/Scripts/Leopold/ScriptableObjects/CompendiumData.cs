using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataType{
        Item,
        Recipe,
        Log,
        Building
    };

public class CompendiumData : ScriptableObject
{
    
    [Header("General")]
    public bool unlocked;
    public int id;
    public string itemName;
    public GameObject menuAsset;
    [Tooltip("Use Menu Asset instead of sprite")]
    public Sprite sprite;
    [TextArea(1, 20)]
    public string description;
}
