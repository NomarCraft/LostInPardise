using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
	public bool _gamePaused = false;

	[SerializeField] private UIManager _uiManager;
	public UIManager uiManager { get { return _uiManager; } }
	[SerializeField] private Compendium _comp;
	public Compendium comp { get { return _comp; } }
	[SerializeField] private InventoryObject _inv;
	public InventoryObject inv { get { return _inv; } }
	[SerializeField] private InventoryDisplay _invDis;
	public InventoryDisplay invDis { get { return _invDis; } }
}
