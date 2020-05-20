using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompendiumDisplay : MonoBehaviour
{
    public Compendium compendium;
    
    [SerializeField] Transform compendiumPage;

    public int X_START;
    public int Y_START;
    public int Y_SPACE_BETWEEN_ITEMS;
    void Start(){
        CreateItemDisplay();
    }

    public void CreateItemDisplay(){
        
        for (int i = 0; i < compendium.unlockedItem.Count; i++)
        {
            var obj = Instantiate(compendium.unlockedItem[i].item.menuAsset, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = compendium.unlockedItem[i].item.itemName.ToString();
        }
    }
    public void CreateRecipeDisplay(){
        
        for (int i = 0; i < compendium.unlockedRecipe.Count; i++)
        {
            var obj = Instantiate(compendium.unlockedRecipe[i].recipe.menuAsset, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = compendium.unlockedRecipe[i].recipe.itemName.ToString();
        }
    }
    public void CreateLogDisplay(){
        
        for (int i = 0; i < compendium.unlockedLog.Count; i++)
        {
            var obj = Instantiate(compendium.unlockedLog[i].log.menuAsset, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = compendium.unlockedLog[i].log.itemName.ToString();
        }
    }

    public void DisplayData(CompendiumData scriptable){
        
    }

    public Vector3 GetPosition(int i){
        return new Vector3(X_START, Y_START + (-Y_SPACE_BETWEEN_ITEMS * i), 0f);
    }
}
