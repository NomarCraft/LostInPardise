using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryPage;
    [SerializeField] GameObject compendiumPage;

    public void ShowInventory(){
        compendiumPage.SetActive(false);
        inventoryPage.SetActive(true);
    }

    public void ShowCompendium(){
        inventoryPage.SetActive(false);
        compendiumPage.SetActive(true);
    }
}
