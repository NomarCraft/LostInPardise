using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    [SerializeField] PlayerInventory inventoryPage;
    [SerializeField] InventoryDisplay inventoryDisplay;
    [SerializeField] Compendium compendiumPage;
    [SerializeField] CompendiumDisplay compendiumDisplay;

    bool inventoryShown;

    public void ShowInventory(){
        inventoryShown = true;
        compendiumPage.gameObject.SetActive(false);
        inventoryPage.gameObject.SetActive(true);
        inventoryDisplay.CreateDisplay();
    }

    public void ShowCompendium(){
        inventoryShown = false;
        inventoryPage.gameObject.SetActive(false);
        compendiumPage.gameObject.SetActive(true);
        //compendiumDisplay.CreateItemDisplay();
    }

    public void ChangeCompendiumPage(){
        if(inventoryShown){
            //If LT then left page
            //If RT then right page
        }
    }
}
