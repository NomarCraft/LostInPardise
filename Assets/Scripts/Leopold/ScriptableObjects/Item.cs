using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class Item : ScriptableObject{

    public bool unlocked;
    public int id;
    public string itemName;
    public float weight;
    public GameObject menuAsset;
    [Tooltip("Use Menu Asset instead of sprite")]
    public Sprite sprite;
    
    [TextArea(1, 20)]
    public string description;
    
}