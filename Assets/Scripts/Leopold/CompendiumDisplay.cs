using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompendiumDisplay : MonoBehaviour
{
    public Compendium compendium;
    public List<DisplayPack> packs = new List<DisplayPack>(new DisplayPack[3]);

    public int X_START;
    public int Y_START;
    public int Y_SPACE_BETWEEN_ITEMS;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;
    Sprite itemSprite;

    public void UpdateTime(){
        ChangeDisplay();
        UpdateDisplayedItems();
        UpdateDisplayedRecipes();
        UpdateDisplayedLogs();
    }

    void UpdateDisplayedItems(){
        for (int i = 0; i < compendium.unlockedItem.Count; i++)
        {
            UpdateDisplay(i, 0, compendium.unlockedItem[i].item);
        }
    }
    void UpdateDisplayedRecipes(){
        for (int i = 0; i < compendium.unlockedRecipe.Count; i++)
        {
            UpdateDisplay(i, 1, compendium.unlockedRecipe[i].recipe);
        }
    }
    void UpdateDisplayedLogs(){
        for (int i = 0; i < compendium.unlockedLog.Count; i++)
        {
            UpdateDisplay(i, 2, compendium.unlockedLog[i].log);
        }
    }

    public void ChangeDisplay(){
       // packs[nb].inventory.SortInventoryById();
        for (int i = 0; i < packs.Count; i++)
        {
            foreach (var obj in packs[i].objDisplayed)
            {
                Destroy(obj.Value);
            }
            packs[i].objDisplayed.Clear();
        }
    }

    public void UpdateDisplay(int i, int nb, CompendiumData data){
        if(!packs[nb].objDisplayed.ContainsKey(data)){
            var obj = Instantiate(data.menuAsset, Vector3.zero, Quaternion.identity, packs[nb].page);
            obj.GetComponent<ButtonSelection>().compendiumData = data;
            RectTransform trans = obj.GetComponent<RectTransform>();
			trans.localPosition = GetPosition(i);
			trans.anchorMax = new Vector2(0.5f, 1);
            trans.anchorMin = new Vector2(0.5f, 1);
            trans.pivot = new Vector2(0.5f, 1);
			Debug.Log(GetPosition(i));
			packs[nb].objDisplayed.Add(data, obj);
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
    }
}

[System.Serializable]
public class DisplayPack{
    public Transform page;
    public Dictionary<CompendiumData, GameObject> objDisplayed = new Dictionary<CompendiumData, GameObject>();
}
