using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : ScriptableObject
{
    public int id;
    public string itemName;
    public GameObject menuAsset;
    
    [TextArea(1, 20)]
    public string text;
}
