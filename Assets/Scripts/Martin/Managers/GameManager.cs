using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Console;

public class GameManager : Singleton<GameManager>
{
	[Header("GameManagment")]
	public bool _gamePaused = false;
	public Gatherable[] _gatherables;
	[SerializeField] private float _currentTime;
	public float _turnDuration = 60f;

	[Space(10)]
	[Header("GameComponents")]
	public Transform _chara;
	[SerializeField] private UIManager _uiManager;
	public UIManager uiManager { get { return _uiManager; } }
	[SerializeField] private EventSystem _uiEvents;
	public EventSystem uiEvents { get { return _uiEvents; } }
	[SerializeField] private Compendium _comp;
	public Compendium comp { get { return _comp; } }
	[SerializeField] private InventoryObject _inv;
	public InventoryObject inv { get { return _inv; } }
	[SerializeField] private InventoryDisplay _invDis;
	public InventoryDisplay invDis { get { return _invDis; } }
	[SerializeField] private List<InventoryDisplayPack> _packs;
	public List<InventoryDisplayPack> packs { get { return _packs; } }
	[SerializeField] private CompendiumDisplay _compDis;
	public CompendiumDisplay compDis { get { return _compDis; } }
	[SerializeField] private Crafter _craft;
	public Crafter craft { get { return _craft; } }

	private void Start()
	{
		GetGatherables();
	}

	private void Update()
	{
		TimeManagment();
	}

	public void GetGatherables()
	{
		_gatherables = GameObject.FindObjectsOfType<Gatherable>();
	}

	private void TimeManagment()
	{
		_currentTime += Time.deltaTime;

		if (_currentTime >= _turnDuration)
		{
			PassTurn();
			_currentTime = 0;
		}
	}

	private void PassTurn()
	{
		GatherableManagment();
	}

	private void GatherableManagment()
	{
		foreach (Gatherable gatherable in _gatherables)
		{
			if (!gatherable._isActive)
			{
				gatherable.TurnPass();
			}
		}
	}

	/*public void ShowConsole(InputAction.CallbackContext context){
		DeveloperConsole.Instance.ShowConsole();
	}

	public void EnterCommand(InputAction.CallbackContext context){
		DeveloperConsole.Instance.EnterCommand();
	}*/
}
