using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class Log : CompendiumData
{
    DataType dataType = DataType.Log;
    [TextArea(1, 20)]
    public string text;
}
