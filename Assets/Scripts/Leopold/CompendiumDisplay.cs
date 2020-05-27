using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompendiumDisplay : MonoBehaviour
{/*
    public Compendium compendium;
    
    [SerializeField] Transform compendiumPage;
    public List<DisplayPack> packs = new List<DisplayPack>(new DisplayPack[3]);

    public int X_START;
    public int Y_START;
    public int Y_SPACE_BETWEEN_ITEMS;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;
    Sprite itemSprite;

    public void Update(){
        UpdateDisplayedItems();
    }

    public void CreateItemDisplay(){
        
        for (int i = 0; i < compendium.unlockedItem.Count; i++)
        {
            var obj = Instantiate(compendium.unlockedItem[i].item.menuAsset, Vector3.zero, Quaternion.identity, packs[0].page);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = compendium.unlockedItem[i].item.itemName.ToString();
        }
    }
    void UpdateDisplayedItems(){
        for (int i = 0; i < compendium.unlockedItem.Count; i++)
        {
            UpdateDisplay(i, 0, compendium.itemDictionnaryInstance);
        }
    }
    void UpdateDisplayedRecipes(){
        for (int i = 0; i < compendium.unlockedRecipe.Count; i++)
        {
            UpdateDisplay(i, 1, compendium.recipeDictionnaryInstance);
        }
    }
    void UpdateDisplayedLogs(){
        for (int i = 0; i < compendium.unlockedLog.Count; i++)
        {
            UpdateDisplay(i, 2, compendium.logDictionnaryInstance);
        }
    }

    public void UpdateDisplay(int i, int nb, List<CustomDictionnary> listC){
        if(packs[nb].objDisplayed.ContainsKey(listC[i].)){
            packs[nb].objDisplayed[packs[nb].inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = packs[nb].inventory.container[i].amount.ToString("n0");
        }else{
            var obj = Instantiate(packs[nb].inventory.container[i].item.menuAsset, Vector3.zero, Quaternion.identity, packs[nb].page);
            RectTransform trans = obj.GetComponent<RectTransform>();
            trans.localPosition = GetPosition(i);
            trans.anchorMax = new Vector2(0, 1);
            trans.anchorMin = new Vector2(0, 1);
            trans.pivot = new Vector2(0, 1);
            packs[nb].objDisplayed.Add(packs[nb].inventory.container[i], obj);
        }
    }

    public void CreateRecipeDisplay(){
        
        for (int i = 0; i < compendium.unlockedRecipe.Count; i++)
        {
            var obj = Instantiate(compendium.unlockedRecipe[i].recipe.menuAsset, Vector3.zero, Quaternion.identity, packs[0].page);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = compendium.unlockedRecipe[i].recipe.itemName.ToString();
        }
    }
    public void CreateLogDisplay(){
        
        for (int i = 0; i < compendium.unlockedLog.Count; i++)
        {
            var obj = Instantiate(compendium.unlockedLog[i].log.menuAsset, Vector3.zero, Quaternion.identity, packs[0].page);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = compendium.unlockedLog[i].log.itemName.ToString();
        }
    }

    public void DisplayData(CompendiumData scriptable){
        itemName.text = scriptable.itemName;
        itemDescription.text = scriptable.description;
    }

    public void RemoveData(CompendiumData scriptable){
        itemName.text = null;
        itemDescription.text = null;
    }

    public Vector3 GetPosition(int i){
        return new Vector3(X_START, Y_START + (-Y_SPACE_BETWEEN_ITEMS * i), 0f);
    }*/
}

[System.Serializable]
public class DisplayPack{
    public Transform page;
    public Dictionary<CompendiumData, GameObject> objDisplayed;
}
