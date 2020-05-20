using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelection : Button
{
    public CompendiumDisplay compendiumDisplay;

    public override void OnSelect(BaseEventData eventData){
        base.OnSelect(eventData);
        Debug.Log("Selected");
    }

    public override void OnDeselect(BaseEventData eventData){
        base.OnDeselect(eventData);
        Debug.Log("Deselected");
    }
}
