using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public bool _gamePaused = false;

	[SerializeField] private UIManager _uiManager;
	public UIManager uiManager { get { return _uiManager; } }
}
