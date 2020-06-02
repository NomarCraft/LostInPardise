using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelection : Button
{
    public CompendiumData compendiumData;
    private UIManager _ui;
	public UIManager ui
	{
		get
		{
			if (!_ui)
				_ui = GameManager.Instance.uiManager;

			return _ui;
		}
	}

    public override void OnSelect(BaseEventData eventData){
        base.OnSelect(eventData);
        ui.UpdateCompendiumText(compendiumData);
    }
/*
    public override void OnDeselect(BaseEventData eventData){
        base.OnDeselect(eventData);
        //compendiumDisplay.RemoveData(compendiumData);
    }*/
}
