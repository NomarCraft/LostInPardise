using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compendium : MonoBehaviour
{
    [System.Serializable]
    public class ItemDictionnary{
        public Item item;
    }
    [System.Serializable]
    public class RecipeDictionnary{
        public Recipe recipe;
    }
    [System.Serializable]
    public class LogDictionnary{
        public Log log;
    }

    public List<ItemDictionnary> itemDictionnary;
    [HideInInspector] public List<ItemDictionnary> itemDictionnaryInstance;
    public List<RecipeDictionnary> recipeDictionnary;
    [HideInInspector] public List<RecipeDictionnary> recipeDictionnaryInstance;
    public List<LogDictionnary> logDictionnary;
    [HideInInspector] public List<LogDictionnary> logDictionnaryInstance;

    void Start(){
        for (int i = 0; i < itemDictionnary.Count; i++)
        {
            itemDictionnaryInstance.Add(new ItemDictionnary());
            itemDictionnaryInstance[i].item = Instantiate(itemDictionnary[i].item);
        }

        for (int i = 0; i < recipeDictionnary.Count; i++)
        {
            recipeDictionnaryInstance.Add(new RecipeDictionnary());
            recipeDictionnaryInstance[i].recipe = Instantiate(recipeDictionnary[i].recipe);
        }

        for (int i = 0; i < logDictionnary.Count; i++)
        {
            logDictionnaryInstance.Add(new LogDictionnary());
            logDictionnaryInstance[i].log = Instantiate(logDictionnary[i].log);
        }
    }

    public void CheckCompendium(Item pickedItem){
        if(pickedItem.unlocked == false){
            for (int i = itemDictionnaryInstance.Count - 1; i >= 0 ; i--)
            {
                if(itemDictionnaryInstance[i].item == pickedItem){
                    UnlockItem(i);
                    break;
                }
            }
        }
    }

    public void UnlockItem(int id){
        itemDictionnaryInstance[id].item.unlocked = true;

        for (int i = 0; i < recipeDictionnaryInstance.Count; i++)
        {
            if(!recipeDictionnaryInstance[i].recipe.unlocked){
                recipeDictionnaryInstance[i].recipe.unlocked = true;
                for (int j = 0; j < recipeDictionnaryInstance[i].recipe.ingredients.Count; j++)
                {
                    if(!recipeDictionnaryInstance[i].recipe.ingredients[j].unlocked){
                        recipeDictionnaryInstance[i].recipe.unlocked = false;
                        break;
                    }
                }
            }
        }
    }

    public void UnlockLog(int logNumber){
        
    }
}
