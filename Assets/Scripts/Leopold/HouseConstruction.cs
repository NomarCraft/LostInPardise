using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseConstruction : MonoBehaviour
{
    public List<GameObject> houseLvls;
    int upgradeLvl = 0;

    void Start(){
        GameManager.Instance.house = this;
    }

    public void UpgradeHouse(){
        foreach (var item in houseLvls)
        {
            item.SetActive(false);
        }

        upgradeLvl++;

        houseLvls[upgradeLvl].SetActive(true);
    }
}
