using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompendiumData : ScriptableObject
{
    public enum DataType{
        Item,
        Recipe,
        Log,
    };
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
