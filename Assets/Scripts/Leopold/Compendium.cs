using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compendium : MonoBehaviour
{
    public Dictionary<int, bool> itemDictionnary = new Dictionary<int, bool>();
    public Dictionary<int, bool> recipeDictionnary = new Dictionary<int, bool>();
    public Dictionary<int, bool> logDictionnary = new Dictionary<int, bool>();


    public void CheckCompendium(Item item){
        if(itemDictionnary.ContainsKey(item.id)){
            itemDictionnary[item.id] = true;

            /*for (int i = 0; i < length; i++)
            {
                
            }*/
        }
    }

    public void UnlockLog(int logNumber){
        logDictionnary[logNumber] = true;
    }
}
