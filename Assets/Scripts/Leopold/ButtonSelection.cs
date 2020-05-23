using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelection : Button
{
    public CompendiumDisplay compendiumDisplay;
    public CompendiumData compendiumData;

    public override void OnSelect(BaseEventData eventData){
        base.OnSelect(eventData);
        //compendiumDisplay.DisplayData(compendiumData);
    }

    public override void OnDeselect(BaseEventData eventData){
        base.OnDeselect(eventData);
        //compendiumDisplay.RemoveData(compendiumData);
    }
}
