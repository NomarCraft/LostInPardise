using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class Item : ScriptableObject{

    public int id;
    public GameObject prefab;
    
    [TextArea(1, 20)]
    public string description;
    
}