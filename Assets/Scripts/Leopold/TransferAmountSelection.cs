using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferAmountSelection : MonoBehaviour
{
    public int amount;

//Need limiter maximum en fonction de quantité disponible

    public void ChangeAmount(int modif){
        if(modif < 0){
            if(amount > 0){
                amount -= modif;
            }
        }else{
            amount += modif;
        }
    }
}
