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

    public void UpdateTime(int currentlySelected){
		switch (currentlySelected)
		{
			case 0:
				UpdateDisplayedItems();
				break;
			case 1:
				UpdateDisplayedRecipes();
				break;
			case 2:
				UpdateDisplayedLogs();
				break;
			default:
				break;
		}
    }

	public void DisplayCraftWindow()
	{
		ChangeDisplay();
		for (int i = 0; i < compendium.unlockedRecipe.Count; i++)
		{
			UpdateDisplay(i, 1, compendium.unlockedRecipe[i].recipe, GameManager.Instance.uiManager._craftWindow);
		}
        for (int i = 0; i < compendium.unlockedBuilding.Count; i++)
        {
            UpdateDisplay(i, 1, compendium.unlockedBuilding[i].building, GameManager.Instance.uiManager._craftWindow);
        }
	}

    void UpdateDisplayedItems(){
		ChangeDisplay();
		for (int i = 0; i < compendium.unlockedItem.Count; i++)
        {
            UpdateDisplay(i, 0, compendium.unlockedItem[i].item);
        }
    }
    void UpdateDisplayedRecipes(){
		ChangeDisplay();
		for (int i = 0; i < compendium.unlockedRecipe.Count; i++)
        {
            UpdateDisplay(i, 1, compendium.unlockedRecipe[i].recipe);
        }
    }
    void UpdateDisplayedLogs(){
		ChangeDisplay();
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
			packs[nb].objDisplayed.Add(data, obj);
			if (i == 0)
				GameManager.Instance.uiEvents.SetSelectedGameObject(obj);
		}
    }

	public void UpdateDisplay(int i, int nb, CompendiumData data, RectTransform parent)
	{
		if (!packs[nb].objDisplayed.ContainsKey(data))
		{
			var obj = Instantiate(data.menuAsset, Vector3.zero, Quaternion.identity, parent);
			obj.GetComponent<ButtonSelection>().compendiumData = data;
			RectTransform trans = obj.GetComponent<RectTransform>();
			trans.localPosition = GetPosition(i);
			trans.anchorMax = new Vector2(0.5f, 1);
			trans.anchorMin = new Vector2(0.5f, 1);
			trans.pivot = new Vector2(0.5f, 1);
			packs[nb].objDisplayed.Add(data, obj);
			if (i == 0)
            GameManager.Instance.uiEvents.SetSelectedGameObject(obj);
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
