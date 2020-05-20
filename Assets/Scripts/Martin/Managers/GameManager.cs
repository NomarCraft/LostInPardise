using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private UIManager _uiManager;
	public UIManager uiManager { get { return _uiManager; } }
}
