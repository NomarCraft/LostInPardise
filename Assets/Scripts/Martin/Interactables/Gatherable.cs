using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : Interactable
{
	[Space(10)]
	[Header("Gather")]
	public int _gatheredItemId;
	public float _gatheredItemAmount;
	public int _turnsToRespawn = 1;
	private int _currentTurnUntilRespawn;
	public GameObject _compToDesactivate;

	private Vector3 _startPos;
	private Quaternion _startRot;

	[Header("Tool")]
	public int _toolRequiredId;

	private void Start()
	{
		_startPos = transform.position;
		_startRot = transform.rotation;
	}

	private void Desactivate()
	{
		if (_compToDesactivate != null)
		{
			_isActive = false;
			_compToDesactivate.SetActive(false);
			_currentTurnUntilRespawn = 0;
			return;
		}
		else
		{
			_isActive = false;
			this.gameObject.SetActive(false);
			_currentTurnUntilRespawn = 0;
		}
	}

	public void TurnPass()
	{
		_currentTurnUntilRespawn++;

		if (_currentTurnUntilRespawn >= _turnsToRespawn)
			Activate();
	}

	public void Activate()
	{
		/*if (_compToDesactivate != null)
		{
			_compToDesactivate.SetActive(true);
			transform.SetPositionAndRotation(_startPos, _startRot);
			_isActive = true;
		}
		else
		{
			this.gameObject.SetActive(true);
			_isActive = true;
		}
		*/
	}

	public void Interaction(out ItemData item)
	{
		if (GameManager.Instance.inv.AddItem(_gatheredItemId, _gatheredItemAmount))
		{
			Desactivate();
			item = GameManager.Instance.comp.GetItemReference(_gatheredItemId);
		}
		else
		{
			item = GameManager.Instance.comp.GetItemReference(_gatheredItemId);
		}

	}
}
