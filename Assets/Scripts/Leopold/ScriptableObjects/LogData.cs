using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Log", menuName = "Inventory System/Log")]
public class LogData : CompendiumData
{
    DataType dataType = DataType.Log;
    [TextArea(1, 20)]
    public string text;
}
