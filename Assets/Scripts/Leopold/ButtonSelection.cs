using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelection : Button
{

    public override void OnSelect(BaseEventData eventData){
        base.OnSelect(eventData);
        //UIManager.Instance.compendiumDisplay
    }

    public override void OnDeselect(BaseEventData eventData){
        base.OnDeselect(eventData);
        //compendiumDisplay.RemoveData(compendiumData);
    }
}
