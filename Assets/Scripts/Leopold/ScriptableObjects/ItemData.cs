using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemData : CompendiumData
{
    public DataType dataType = DataType.Item;
    public float itemWeight;
}