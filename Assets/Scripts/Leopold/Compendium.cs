using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compendium : MonoBehaviour
{

    public List<ItemDictionnary> itemDictionnary;
    [HideInInspector] public List<ItemDictionnary> itemDictionnaryInstance;
    public List<RecipeDictionnary> recipeDictionnary;
    [HideInInspector] public List<RecipeDictionnary> recipeDictionnaryInstance;
    public List<LogDictionnary> logDictionnary;
    [HideInInspector] public List<LogDictionnary> logDictionnaryInstance;
    public List<ItemDictionnary> unlockedItem = new List<ItemDictionnary>();
    public List<RecipeDictionnary> unlockedRecipe = new List<RecipeDictionnary>();
    public List<LogDictionnary> unlockedLog = new List<LogDictionnary>();

//Setting instance of the Scriptable Objects
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

    public void CheckCompendium(int itemID){

        //Check if we already had the item
        for (int i = itemDictionnaryInstance.Count - 1; i >= 0 ; i--)
        {
            if(itemDictionnaryInstance[i].item.id == itemID){
                if(itemDictionnaryInstance[i].item.unlocked == false){
                    UnlockItem(i);

                    //Check if we can unlock recipes
                    for (int k = 0; k < recipeDictionnaryInstance.Count; k++)
                    {
                        if(!recipeDictionnaryInstance[k].recipe.unlocked){
                            recipeDictionnaryInstance[k].recipe.unlocked = true;
                            for (int j = 0; j < recipeDictionnaryInstance[k].recipe.ingredients.Count; j++)
                            {
                                int id = new int();

                                for (int l = 0; l < itemDictionnaryInstance.Count; l++)
                                {
                                    if(itemDictionnaryInstance[l].item.id == recipeDictionnaryInstance[k].recipe.ingredients[j].id){
                                        id = l;
                                        break;
                                    }
                                }

                                if(!itemDictionnaryInstance[id].item.unlocked){
                                    recipeDictionnaryInstance[k].recipe.unlocked = false;
                                    break;
                                }
                            }
                            
                            SortUnlockedRecipe();
                        }
                    }

                    break;
                }
            }
        }
    }

    public void UnlockItem(int id){
        itemDictionnaryInstance[id].item.unlocked = true;
        SortUnlockedItem();
    }

    public void UnlockLog(int logNumber){
        logDictionnaryInstance[logNumber].log.unlocked = true;
        unlockedLog.Add(logDictionnaryInstance[logNumber]);
        SortUnlockedLog();
    }

    public void SortUnlockedItem(){

        unlockedItem.Sort(delegate(ItemDictionnary x, ItemDictionnary y)
        {
            if (x.item.id == 0 && y.item.id == 0) return 0;
            else if (x.item.id == 0) return -1;
            else if (y.item.id == 0) return 1;
            else return x.item.id.CompareTo(y.item.id);
        });
    }
    public void SortUnlockedRecipe(){

        unlockedRecipe.Sort(delegate(RecipeDictionnary x, RecipeDictionnary y)
        {
            if (x.recipe.id == 0 && y.recipe.id == 0) return 0;
            else if (x.recipe.id == 0) return -1;
            else if (y.recipe.id == 0) return 1;
            else return x.recipe.id.CompareTo(y.recipe.id);
        });
    }
    public void SortUnlockedLog(){

        unlockedLog.Sort(delegate(LogDictionnary x, LogDictionnary y)
        {
            if (x.log.id == 0 && y.log.id == 0) return 0;
            else if (x.log.id == 0) return -1;
            else if (y.log.id == 0) return 1;
            else return x.log.id.CompareTo(y.log.id);
        });
    }

    public ItemData GetItemReference(int itemId){
        ItemData returnedItem = null; 
        for (int i = 0; i < itemDictionnaryInstance.Count; i++)
        {
            if(itemDictionnaryInstance[i].item.id == itemId){
                returnedItem = itemDictionnaryInstance[i].item;
            }
            //break;
        }
        return returnedItem;
    }
}

    public class CustomDictionnary{

    }
    [System.Serializable]
    public class ItemDictionnary : CustomDictionnary{
        public ItemData item;
    }
    [System.Serializable]
    public class RecipeDictionnary : CustomDictionnary{
        public RecipeData recipe;
    }
    [System.Serializable]
    public class LogDictionnary : CustomDictionnary{
        public LogData log;
    }