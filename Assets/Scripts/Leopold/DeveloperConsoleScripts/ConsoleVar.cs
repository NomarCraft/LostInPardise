using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleVar : MonoBehaviour
{
    [HideInInspector] public Dictionary<string ,Transform> teleportPoints = new Dictionary<string, Transform>();
    public List<Transform> teleportTranforms;

    public void Awake(){
        foreach (var item in teleportTranforms)
        {
            teleportPoints.Add(item.name.ToLower(), item);
        }
    }
}
