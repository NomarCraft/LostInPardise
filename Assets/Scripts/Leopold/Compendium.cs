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
	public List<RecipeDictionnary> lockedRecipe = new List<RecipeDictionnary>();
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
			lockedRecipe.Add(recipeDictionnaryInstance[i]);
        }

        for (int i = 0; i < logDictionnary.Count; i++)
        {
            logDictionnaryInstance.Add(new LogDictionnary());
            logDictionnaryInstance[i].log = Instantiate(logDictionnary[i].log);
        }
    }

    public void CheckCompendium(int itemID){

        //Check if we already had the item
        for (int i = 0 ; i < itemDictionnaryInstance.Count ; i++)
        {
            if(itemDictionnaryInstance[i].item.id == itemID){
                if(itemDictionnaryInstance[i].item.unlocked == false){
                    UnlockItem(i);

					List<RecipeDictionnary> lockedRecipeToRemove = new List<RecipeDictionnary>();
					//Check if we can unlock recipes
					for (int j = 0; j < lockedRecipe.Count; j++)
					{
						for (int k = 0; k < lockedRecipe[j].recipe.ingredients.Count; k++)
						{
							for (int l = 0; l < unlockedItem.Count; l++)
							{
								if (lockedRecipe[j].recipe.ingredients[k].ingredient.id == unlockedItem[l].item.id)
								{
									lockedRecipe[j].recipe.ingredients[k].unlocked = true;
								}
							}

							bool checkIngr = true;
							foreach(Ingredient ingr in lockedRecipe[j].recipe.ingredients)
							{
								if (!ingr.unlocked)
								{
									checkIngr = false;
								}
							}

							if (checkIngr)
							{
								UnlockRecipe(lockedRecipe[j].recipe.id);
								lockedRecipeToRemove.Add(lockedRecipe[j]);
							}
						}
					}

					RemoveRecipeFromList(lockedRecipeToRemove);

                    break;
                }
            }
        }
    }

    public void UnlockItem(int id){
        itemDictionnaryInstance[id].item.unlocked = true;
		unlockedItem.Add(itemDictionnaryInstance[id]);
        SortUnlockedItem();
    }

	public void UnlockRecipe(int id)
	{
		if (!recipeDictionnaryInstance[id].recipe.unlocked)
		{
			recipeDictionnaryInstance[id].recipe.unlocked = true;
			unlockedRecipe.Add(recipeDictionnaryInstance[id]);
		}
	}

    public void UnlockLog(int logNumber){
		if (!logDictionnaryInstance[logNumber].log.unlocked)
		{
			logDictionnaryInstance[logNumber].log.unlocked = true;
			unlockedLog.Add(logDictionnaryInstance[logNumber]);
			SortUnlockedLog();
		}
    }

	public void RemoveRecipeFromList(List<RecipeDictionnary> list)
	{
		foreach (RecipeDictionnary recipe in list)
		{
			lockedRecipe.Remove(recipe);
		}
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
        //public CompendiumData data;
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